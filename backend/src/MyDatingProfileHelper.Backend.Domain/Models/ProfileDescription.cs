csharp
#nullable enable
namespace MyDatingProfileHelper.Backend.Domain.Models
{
    // <summary>
    /// Представляет пример описания профиля.
    /// </summary>
    public class ProfileDescription
    {
        /// <summary>
        /// Gets or sets the unique identifier for the profile description.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the text of the profile description.
        /// Получает или задает текст описания профиля.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the category of the profile description (e.g., humorous, serious).
        /// Получает или задает категорию описания профиля (например, юмористическое, серьезное и т.д.).
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the date and time the profile description was created.
        /// Получает или задает дату и время создания описания профиля.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Получает или задает значение, указывающее, было ли одобрено описание профиля.
        /// </summary>
        public bool IsApproved { get; set; }
    }
}