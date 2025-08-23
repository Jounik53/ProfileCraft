csharp
using MediatR;
using System;

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос на получение статистики по кристаллам (куплено/потрачено).
    /// </summary>
    public class GetCrystalStatisticsQuery : IRequest<CrystalStatistics>
    {
        /// <summary>
        /// Начальная дата/время для фильтрации статистики.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Конечная дата/время для фильтрации статистики.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// Класс для представления результатов статистики по кристаллам.
    /// </summary>
    public class CrystalStatistics
    {
        /// <summary>
        /// Общее количество купленных кристаллов.
        /// </summary>
        public int TotalCrystalsBought { get; set; }

        /// <summary>
        /// Общее количество потраченных кристаллов.
        /// </summary>
        public int TotalCrystalsSpent { get; set; }
    }
}