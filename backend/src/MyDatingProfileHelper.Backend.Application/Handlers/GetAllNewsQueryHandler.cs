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
    /// Обработчик запроса для получения всех новостей.
    /// </summary>
    public class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, IEnumerable<News>>
    {
        private readonly INewsRepository _newsRepository;

        /// <summary>
        /// Конструктор обработчика запроса.
        /// </summary>
        /// <param name="newsRepository">Репозиторий новостей.</param>
        public GetAllNewsQueryHandler(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение всех новостей.
        /// </summary>
        /// <param name="request">Запрос на получение всех новостей.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Коллекция всех новостей.</returns>
        public async Task<IEnumerable<News>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
        {
            // Получаем все новости из репозитория
            var news = await _newsRepository.GetAllAsync();
            return news;
        }
    }
}