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
    /// Обработчик команды для сохранения истории сгенерированных анкет.
    /// </summary>
    public class SaveGeneratedProfileHistoryCommandHandler : IRequestHandler<SaveGeneratedProfileHistoryCommand, GeneratedProfileHistory>
    {
        private readonly IGeneratedProfileHistoryRepository _generatedProfileHistoryRepository;

        /// <summary>
        /// Конструктор обработчика команды.
        /// </summary>
        /// <param name="generatedProfileHistoryRepository">Репозиторий истории сгенерированных анкет.</param>
        public SaveGeneratedProfileHistoryCommandHandler(IGeneratedProfileHistoryRepository generatedProfileHistoryRepository)
        {
            _generatedProfileHistoryRepository = generatedProfileHistoryRepository;
        }

        /// <summary>
        /// Обрабатывает команду сохранения истории сгенерированной анкеты.
        /// </summary>
        /// <param name="request">Команда для обработки.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Сохраненный объект истории сгенерированной анкеты.</returns>
        public async Task<GeneratedProfileHistory> Handle(SaveGeneratedProfileHistoryCommand request, CancellationToken cancellationToken)
        {
            // Создаем объект истории из данных команды
            var historyEntry = new GeneratedProfileHistory
            {
                UserProfileId = request.UserProfileId,
                GeneratedText = request.GeneratedText,
                Timestamp = DateTime.UtcNow, // Устанавливаем текущее время
                InputParameters = request.InputParameters // Сохраняем входные параметры, если есть
            };

            // Добавляем запись в репозиторий
            await _generatedProfileHistoryRepository.AddAsync(historyEntry);

            // Возвращаем сохраненную запись
            return historyEntry;
        }
    }
}