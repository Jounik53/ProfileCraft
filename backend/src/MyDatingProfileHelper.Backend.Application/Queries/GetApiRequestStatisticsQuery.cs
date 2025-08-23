csharp
using MediatR;
using System;
using MyDatingProfileHelper.Backend.Application.DTOs; // Убедитесь, что этот using соответствует расположению вашего DTO ApiRequestStatistics

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос для получения статистики запросов к API.
    /// </summary>
    public class GetApiRequestStatisticsQuery : IRequest<ApiRequestStatistics>
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
}
