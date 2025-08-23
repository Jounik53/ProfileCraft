csharp
namespace MyDatingProfileHelper.Backend.Api.DTOs
{
    /// <summary>
    /// DTO для представления истории сгенерированной анкеты в API.
    /// </summary>
    public class GeneratedProfileHistoryDto
    {
        /// <summary>
        /// Идентификатор записи истории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сгенерированный текст анкеты.
        /// </summary>
        public string GeneratedText { get; set; }

        /// <summary>
        /// Время генерации.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}