csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса для получения последних новостей.
    /// </summary>
    public class GetRecentNewsQueryHandler : IRequestHandler<GetRecentNewsQuery, IEnumerable<News>>
    {
        private readonly INewsRepository _newsRepository;

        /// <summary>
        /// Конструктор обработчика.
        /// </summary>
        /// <param name="newsRepository">Репозиторий новостей.</param>
        public GetRecentNewsQueryHandler(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение последних новостей.
        /// </summary>
        /// <param name="request">Запрос на получение последних новостей.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Коллекция последних новостей.</returns>
        public async Task<IEnumerable<News>> Handle(GetRecentNewsQuery request, CancellationToken cancellationToken)
        {
            // Вызываем метод репозитория для получения новостей начиная с указанной даты и для указанного языка
            return await _newsRepository.GetRecentNewsAsync(request.FromDate, request.LanguageCode);
        }
    }
}