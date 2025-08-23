csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса для получения баланса кристаллов пользователя по его Telegram идентификатору.
    /// </summary>
    public class GetUserCrystalBalanceByTelegramIdentifierQueryHandler : IRequestHandler<GetUserCrystalBalanceByTelegramIdentifierQuery, int?>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор обработчика.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей для доступа к данным.</param>
        public GetUserCrystalBalanceByTelegramIdentifierQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Обрабатывает запрос получения баланса кристаллов по Telegram идентификатору.
        /// </summary>
        /// <param name="request">Запрос с Telegram идентификатором.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Баланс кристаллов пользователя или null, если пользователь не найден.</returns>
        public async Task<int?> Handle(GetUserCrystalBalanceByTelegramIdentifierQuery request, CancellationToken cancellationToken)
        {
            // Находим пользователя в репозитории по его Telegram идентификатору.
            // Предполагается, что в IUserRepository есть метод FindByTelegramIdentifierAsync.
            // Если такого метода нет, его нужно будет добавить в интерфейс и его реализацию.
            var user = await _userRepository.FindByTelegramIdentifierAsync(request.TelegramIdentifier);

            // Если пользователь найден, возвращаем его баланс кристаллов.
            // Если пользователь не найден, возвращаем null.
            return user?.CrystalBalance;
        }
    }
}