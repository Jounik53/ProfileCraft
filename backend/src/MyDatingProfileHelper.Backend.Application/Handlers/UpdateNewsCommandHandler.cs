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
    /// Обработчик команды для обновления существующей новости.
    /// </summary>
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, News>
    {
        private readonly INewsRepository _newsRepository;

        /// <summary>
        /// Конструктор обработчика команды.
        /// </summary>
        /// <param name="newsRepository">Репозиторий новостей.</param>
        public UpdateNewsCommandHandler(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Обрабатывает команду обновления новости.
        /// </summary>
        /// <param name="request">Команда для обновления новости.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Обновленный объект новости или null, если новость не найдена.</returns>
        /// <exception cref="Exception">Исключение, если новость с указанным ID не найдена.</exception>
        public async Task<News> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            // Получаем существующую новость по ID
            var news = await _newsRepository.GetByIdAsync(request.Id);

            // Если новость не найдена, выбрасываем исключение
            if (news == null)
            {
                throw new Exception($"Новость с ID {request.Id} не найдена.");
            }

            // Обновляем свойства новости из команды
            news.Title = request.Title;
            news.Content = request.Content;
            news.LanguageCode = request.LanguageCode;
            // PublishDate обычно не обновляется при редактировании, но можно добавить эту логику, если требуется

            // Обновляем новость в репозитории
            await _newsRepository.UpdateAsync(news);

            // Возвращаем обновленную новость
            return news;
        }
    }
}