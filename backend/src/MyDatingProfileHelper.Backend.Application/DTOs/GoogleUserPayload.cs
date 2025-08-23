csharp
// Пространство имен для DTO уровня приложения
namespace MyDatingProfileHelper.Backend.Application.DTOs
{
    /// <summary>
    /// Базовый класс для хранения проверенной информации о пользователе Google.
    /// </summary>
    public class GoogleUserPayload
    {
        /// <summary>
        /// Уникальный идентификатор пользователя Google.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Email пользователя Google.
        /// </summary>
        public string? Email { get; set; }
    }
}