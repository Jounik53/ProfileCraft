csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса для получения баланса кристаллов пользователя.
    /// </summary>
    public class GetUserCrystalBalanceQueryHandler : IRequestHandler<GetUserCrystalBalanceQuery, int>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор обработчика запроса.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        public GetUserCrystalBalanceQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение баланса кристаллов пользователя.
        /// </summary>
        /// <param name="request">Запрос на получение баланса кристаллов.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Баланс кристаллов пользователя.</returns>
        public async Task<int> Handle(GetUserCrystalBalanceQuery request, CancellationToken cancellationToken)
        {
            // Получаем пользователя из репозитория по его идентификатору.
            // В реальной реализации нужно добавить проверку на null и обработку ошибок.
            var user = await _userRepository.GetUserByIdAsync(request.UserId);

            // Возвращаем баланс кристаллов пользователя.
            return user?.CrystalBalance ?? 0; // Возвращаем 0, если пользователь не найден (простейшая обработка)
        }
    }
}