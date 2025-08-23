csharp
namespace MyDatingProfileHelper.Backend.AdminWebApp.Models
{
    /// <summary>
    /// Модель представления для страницы панели администратора.
    /// Содержит данные статистики для отображения.
    /// </summary>
    public class DashboardViewModel
    {
        /// <summary>
        /// Количество зарегистрированных пользователей.
        /// </summary>
        public int RegisteredUserCount { get; set; }

        /// <summary>
        /// Статистика по кристаллам (куплено/потрачено).
        /// </summary>
        // Предполагается, что CrystalStatistics - это класс или DTO,
        // определенный в слое Application.
        public Application.DTOs.CrystalStatistics CrystalStatistics { get; set; }

        /// <summary>
        /// Статистика запросов к API из мобильного приложения.
        /// </summary>
        // Предполагается, что ApiRequestStatistics - это класс или DTO,
        // определенный в слое Application.
        public Application.DTOs.ApiRequestStatistics ApiRequestStatistics { get; set; }

        // Здесь можно добавить другие свойства для графиков, фильтров по дате и т.д.
    }
}