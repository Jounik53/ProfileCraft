csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса на получение случайных примеров сгенерированных описаний профилей.
    /// </summary>
    public class GetRandomProfileDescriptionsQueryHandler : IRequestHandler<GetRandomProfileDescriptionsQuery, IEnumerable<GeneratedProfileHistory>>
    {
        private readonly IGeneratedProfileHistoryRepository _generatedProfileHistoryRepository;

        /// <summary>
        /// Конструктор обработчика.
        /// </summary>
        /// <param name="generatedProfileHistoryRepository">Репозиторий истории генерации профилей.</param>
        public GetRandomProfileDescriptionsQueryHandler(IGeneratedProfileHistoryRepository generatedProfileHistoryRepository)
        {
            _generatedProfileHistoryRepository = generatedProfileHistoryRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение случайных примеров описаний.
        /// </summary>
        /// <param name="request">Запрос на получение случайных описаний.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список случайных сгенерированных описаний профилей.</returns>
        public async Task<IEnumerable<GeneratedProfileHistory>> Handle(GetRandomProfileDescriptionsQuery request, CancellationToken cancellationToken)
        {
            // Получаем все сгенерированные истории (или достаточно большой набор, если их очень много)
            // В реальном приложении может потребоваться оптимизация, например, выборка случайных ID
            // или использование особенностей конкретной СУБД для случайной выборки.
            var allHistories = await _generatedProfileHistoryRepository.GetAllAsync(); // Предполагаем, что этот метод существует или его нужно добавить

            if (allHistories == null || !allHistories.Any())
            {
                return Enumerable.Empty<GeneratedProfileHistory>();
            }

            var random = new Random();

            // Выбираем случайные описания
            var randomDescriptions = allHistories.OrderBy(x => random.Next())
                                                 .Take(request.Count)
                                                 .ToList();

            return randomDescriptions;
        }
    }
}