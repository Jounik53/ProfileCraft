csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик команды для создания новой новости.
    /// </summary>
    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, News>
    {
        private readonly INewsRepository _newsRepository;

        /// <summary>
        /// Конструктор обработчика команды.
        /// </summary>
        /// <param name="newsRepository">Репозиторий новостей.</param>
        public CreateNewsCommandHandler(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Обрабатывает команду создания новости.
        /// </summary>
        /// <param name="request">Команда для создания новости.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Созданный объект новости.</returns>
        public async Task<News> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            // Создаем новый объект новости из данных команды
            var news = new News
            {
                Title = request.Title,
                Content = request.Content,
                LanguageCode = request.LanguageCode,
                PublishDate = DateTime.UtcNow // Устанавливаем текущее время публикации
            };

            // Добавляем новость в репозиторий
            await _newsRepository.AddAsync(news);

            // Возвращаем созданную новость
            return news;
        }
    }
}