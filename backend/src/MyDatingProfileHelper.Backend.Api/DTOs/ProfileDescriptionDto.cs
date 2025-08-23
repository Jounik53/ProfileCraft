csharp
// Пространство имен для DTO уровня API
namespace MyDatingProfileHelper.Backend.Api.DTOs
{
    /// <summary>
    /// DTO для представления описания профиля в API ответах.
    /// </summary>
    public class ProfileDescriptionDto
    {
        /// <summary>
        /// Идентификатор описания профиля.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст описания профиля.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Категория описания профиля (например, "юмористическое", "серьезное").
        /// </summary>
        public string Category { get; set; }
    }
}