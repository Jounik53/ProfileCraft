csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса для получения количества зарегистрированных пользователей.
    /// </summary>
    public class GetRegisteredUserCountQueryHandler : IRequestHandler<GetRegisteredUserCountQuery, int>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор обработчика запроса.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        public GetRegisteredUserCountQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение количества зарегистрированных пользователей.
        /// </summary>
        /// <param name="request">Запрос GetRegisteredUserCountQuery.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Количество зарегистрированных пользователей.</returns>
        public async Task<int> Handle(GetRegisteredUserCountQuery request, CancellationToken cancellationToken)
        {
            // TODO: Реализовать метод GetUserCount в UserRepository
            // Например:
            // return await _userRepository.GetUserCountAsync();

            // Пока возвращаем заглушку
            return await Task.FromResult(100); // Пример: 100 зарегистрированных пользователей
        }
    }
}