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
    /// Обработчик команды для создания нового пользователя.
    /// Реализует интерфейс IRequestHandler для команды CreateUserCommand и возвращает объект User.
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор обработчика команды.
        /// Внедряет зависимость от репозитория пользователей.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей для взаимодействия с данными пользователей.</param>
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Обрабатывает команду CreateUserCommand.
        /// Создает новый объект пользователя и добавляет его в базу данных через репозиторий.
        /// </summary>
        /// <param name="request">Команда на создание пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Созданный объект пользователя.</returns>
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Создаем новый объект пользователя из данных команды.
            var user = new User
            {
                GoogleId = request.GoogleId,
                Email = request.Email,
                RegistrationDate = System.DateTime.UtcNow, // Устанавливаем дату регистрации
                LastLoginDate = System.DateTime.UtcNow,    // Устанавливаем дату последнего входа
                CrystalBalance = 0                         // Начальный баланс кристаллов
            };

            // Добавляем пользователя в базу данных с помощью репозитория.
            await _userRepository.AddUserAsync(user);

            // Возвращаем созданного пользователя.
            return user;
        }
    }
}