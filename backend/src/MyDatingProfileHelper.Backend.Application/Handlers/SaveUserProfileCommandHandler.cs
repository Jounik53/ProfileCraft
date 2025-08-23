csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик команды для сохранения или обновления профиля пользователя.
    /// </summary>
    public class SaveUserProfileCommandHandler : IRequestHandler<SaveUserProfileCommand, UserProfile>
    {
        private readonly IUserProfileRepository _userProfileRepository;

        /// <summary>
        /// Конструктор обработчика команды.
        /// </summary>
        /// <param name="userProfileRepository">Репозиторий для работы с профилями пользователей.</param>
        public SaveUserProfileCommandHandler(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// Обрабатывает команду сохранения или обновления профиля пользователя.
        /// </summary>
        /// <param name="request">Команда сохранения или обновления профиля.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Объект UserProfile после сохранения или обновления.</returns>
        public async Task<UserProfile> Handle(SaveUserProfileCommand request, CancellationToken cancellationToken)
        {
            // Здесь должна быть логика получения существующего профиля по UserId из request
            // Если профиль существует, обновляем его данные из request
            // Если профиль не существует, создаем новый объект UserProfile на основе данных из request

            // Placeholder: Создаем или обновляем профиль (в реальной реализации нужно получать из репозитория)
            var userProfile = new UserProfile
            {
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
                // Маппинг списков (фото, интересы, цели) потребуется реализовать
                // Photos = ...
                // Interests = ...
                // DatingGoals = ...
            };

            // Вызов репозитория для добавления или обновления профиля.
            // Метод в репозитории должен уметь определять, нужно ли добавлять или обновлять.
            var savedProfile = await _userProfileRepository.SaveOrUpdateProfileAsync(userProfile); // Предполагаем наличие такого метода в репозитории

            // В реальной реализации здесь может быть логика валидации или дополнительные действия.

            return savedProfile;
        }
    }
}