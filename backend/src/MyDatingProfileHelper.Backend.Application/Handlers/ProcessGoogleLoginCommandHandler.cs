csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Application.Services; // Предполагается, что у вас будет интерфейс сервиса для Google Token верификации
using MyDatingProfileHelper.Backend.Infrastructure.Services; // Предполагается, что у вас будет сервис для генерации JWT
using System.Threading;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик команды для обработки Google авторизации.
    /// </summary>
    public class ProcessGoogleLoginCommandHandler : IRequestHandler<ProcessGoogleLoginCommand, string> // Или другой тип для результата, если возвращается не только JWT
    {
        private readonly IUserRepository _userRepository;
        private readonly IGoogleTokenVerifier _googleTokenVerifier; // Интерфейс для сервиса верификации Google Token
        private readonly IJwtGenerator _jwtGenerator; // Интерфейс для сервиса генерации JWT

        /// <summary>
        /// Конструктор обработчика команды ProcessGoogleLoginCommand.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="googleTokenVerifier">Сервис для верификации Google Token.</param>
        /// <param name="jwtGenerator">Сервис для генерации JWT.</param>
        public ProcessGoogleLoginCommandHandler(IUserRepository userRepository, IGoogleTokenVerifier googleTokenVerifier, IJwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _googleTokenVerifier = googleTokenVerifier;
            _jwtGenerator = jwtGenerator;
        }

        /// <summary>
        /// Обрабатывает команду ProcessGoogleLoginCommand.
        /// </summary>
        /// <param name="request">Команда для обработки.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>JWT при успешной авторизации.</returns>
        public async Task<string> Handle(ProcessGoogleLoginCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement Google Token verification logic
            // Например: var googleUser = await _googleTokenVerifier.VerifyTokenAsync(request.GoogleToken);
            var googleUser = await _googleTokenVerifier.VerifyTokenAsync(request.GoogleToken);

            // Если токен недействителен, возвращаем null или выбрасываем исключение
            if (googleUser == null)
            {
                // В реальном приложении здесь может быть логирование и более специфичное исключение
                // Например: _logger.LogWarning("Неверный или недействительный Google Token.");
                return null; // Или throw new InvalidGoogleTokenException();
            }

            // Получаем Google User ID и Email из верифицированного payload
            string googleUserId = googleUser.Id;
            string email = googleUser.Email;

            // Ищем пользователя по Google ID
            var user = await _userRepository.FindByGoogleIdAsync(googleUserId); // Предполагается, что FindByGoogleIdAsync существует в IUserRepository

            if (user == null)
            {
                // Если пользователь не найден, создаем нового
                var newUser = new Domain.Models.User // Используем модель домена
                {
                    GoogleId = googleUserId,
                    Email = email,
                    RegistrationDate = System.DateTime.UtcNow,
                    LastLoginDate = System.DateTime.UtcNow,
                    CrystalBalance = 0 // Начальный баланс кристаллов
                };
                await _userRepository.AddUserAsync(newUser); // Предполагается, что AddUserAsync существует в IUserRepository
                user = newUser; // Для генерации токена
            }
            else
            {
                // Если пользователь найден, обновляем дату последнего входа
                user.LastLoginDate = System.DateTime.UtcNow;
                // Обновляем пользователя через репозиторий (если у репозитория есть метод обновления)
                await _userRepository.UpdateUserAsync(user); // Предполагается, что UpdateUserAsync существует в IUserRepository
            }

            // Используем данные пользователя из базы данных для генерации JWT
            var jwt = _jwtGenerator.GenerateToken(user); // Предполагается, что GenerateToken принимает объект User

            return jwt; // Возвращаем сгенерированный JWT
        }
    }
}