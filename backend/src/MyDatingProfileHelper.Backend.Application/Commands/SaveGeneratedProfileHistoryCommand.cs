csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models; // Убедитесь, что путь к модели GeneratedProfileHistory правильный

namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для сохранения записи истории сгенерированных анкет.
    /// </summary>
    public class SaveGeneratedProfileHistoryCommand : IRequest<GeneratedProfileHistory>
    {
        /// <summary>
        /// Идентификатор профиля пользователя, связанный с записью истории.
        /// </summary>
        public int UserProfileId { get; set; }

        /// <summary>
        /// Сгенерированный текст анкеты.
        /// </summary>
        public string GeneratedText { get; set; }

        /// <summary>
        /// Входные параметры, использованные для генерации (например, в формате JSON).
        /// </summary>
        public string? InputParameters { get; set; } // Может быть необязательным
    }
}