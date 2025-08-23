csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса для получения последней записи истории сгенерированных профилей.
    /// </summary>
    public class GetLatestGeneratedProfileHistoryQueryHandler : IRequestHandler<Queries.GetLatestGeneratedProfileHistoryQuery, GeneratedProfileHistory?>
    {
        private readonly IGeneratedProfileHistoryRepository _historyRepository;

        /// <summary>
        /// Конструктор обработчика запроса.
        /// </summary>
        /// <param name="historyRepository">Репозиторий истории сгенерированных профилей.</param>
        public GetLatestGeneratedProfileHistoryQueryHandler(IGeneratedProfileHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение последней записи истории сгенерированных профилей.
        /// </summary>
        /// <param name="request">Запрос с идентификатором профиля пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Последняя запись истории сгенерированного профиля или null, если записей нет.</returns>
        public async Task<GeneratedProfileHistory?> Handle(Queries.GetLatestGeneratedProfileHistoryQuery request, CancellationToken cancellationToken)
        {
            // Используем репозиторий для получения последней записи истории по идентификатору профиля пользователя.
            // Реализация в репозитории должна сортировать по дате/времени и брать первую/последнюю запись.
            return await _historyRepository.GetLatestHistoryEntryForUserProfileAsync(request.UserProfileId);
        }
    }
}