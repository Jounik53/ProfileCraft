csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик команды для удаления новости.
    /// </summary>
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, Unit>
    {
        private readonly INewsRepository _newsRepository;

        /// <summary>
        /// Конструктор обработчика команды.
        /// </summary>
        /// <param name="newsRepository">Репозиторий новостей.</param>
        public DeleteNewsCommandHandler(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Обрабатывает команду удаления новости.
        /// </summary>
        /// <param name="request">Команда для удаления новости.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, представляющая асинхронную операцию с возвращаемым типом Unit.</returns>
        public async Task<Unit> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            // Вызываем метод репозитория для удаления новости по ее ID
            await _newsRepository.DeleteAsync(request.NewsId);

            // Возвращаем Unit.Value для команд, которые не возвращают результат
            return Unit.Value;
        }
    }
}