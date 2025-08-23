csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса для получения всей истории сгенерированных анкет пользователя.
    /// </summary>
    public class GetAllGeneratedProfileHistoryQueryHandler : IRequestHandler<GetAllGeneratedProfileHistoryQuery, List<GeneratedProfileHistory>>
    {
        private readonly IGeneratedProfileHistoryRepository _generatedProfileHistoryRepository;

        /// <summary>
        /// Конструктор обработчика запроса получения всей истории.
        /// </summary>
        /// <param name="generatedProfileHistoryRepository">Репозиторий истории сгенерированных анкет.</param>
        public GetAllGeneratedProfileHistoryQueryHandler(IGeneratedProfileHistoryRepository generatedProfileHistoryRepository)
        {
            _generatedProfileHistoryRepository = generatedProfileHistoryRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение всей истории сгенерированных анкет пользователя.
        /// </summary>
        /// <param name="request">Запрос, содержащий UserProfileId.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список объектов GeneratedProfileHistory.</returns>
        public async Task<List<GeneratedProfileHistory>> Handle(GetAllGeneratedProfileHistoryQuery request, CancellationToken cancellationToken)
        {
            // Получаем всю историю сгенерированных анкет для указанного профиля пользователя из репозитория.
            var history = await _generatedProfileHistoryRepository.GetAllAsync(request.UserProfileId);

            // Возвращаем список записей истории.
            return history;
        }
    }
}