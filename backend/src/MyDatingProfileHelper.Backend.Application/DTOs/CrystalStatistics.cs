csharp
// Пространство имен для DTO уровня приложения
namespace MyDatingProfileHelper.Backend.Application.DTOs
{
    /// <summary>
    /// Класс для представления статистики по кристаллам.
    /// Используется в запросах и ответах уровня приложения.
    /// </summary>
    public class CrystalStatistics
    {
        /// <summary>
        /// Общее количество купленных или начисленных кристаллов.
        /// </summary>
        public int TotalCredited { get; set; }

        /// <summary>
        /// Общее количество потраченных или списанных кристаллов.
        /// </summary>
        public int TotalDebited { get; set; }
    }
}