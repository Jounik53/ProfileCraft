csharp
// Пространство имен для DTO API слоя
namespace MyDatingProfileHelper.Backend.Api.DTOs
{
    /// <summary>
    /// DTO для запроса сохранения истории сгенерированной анкеты.
    /// Используется для получения данных от клиента.
    /// </summary>
    public class SaveGeneratedProfileHistoryRequest
    {
        /// <summary>
        /// Сгенерированный текст анкеты.
        /// </summary>
        public string GeneratedText { get; set; }

        /// <summary>
        /// Входные параметры, использованные для генерации (в формате JSON или строки).
        /// </summary>
        public string? InputParameters { get; set; }
    }
}