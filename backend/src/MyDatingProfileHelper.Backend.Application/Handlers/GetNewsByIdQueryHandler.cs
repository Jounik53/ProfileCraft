csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса для получения новости по ее идентификатору.
    /// </summary>
    public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, News?>
    {
        private readonly INewsRepository _newsRepository;

        /// <summary>
        /// Конструктор обработчика запроса.
        /// </summary>
        /// <param name="newsRepository">Репозиторий новостей.</param>
        public GetNewsByIdQueryHandler(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение новости по идентификатору.
        /// </summary>
        /// <param name="request">Запрос GetNewsByIdQuery.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Новость или null, если не найдена.</returns>
        public async Task<News?> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            // Вызываем метод репозитория для получения новости по идентификатору
            return await _newsRepository.GetByIdAsync(request.NewsId);
        }
    }
}