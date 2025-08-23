csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса для получения статистики кристаллов.
    /// </summary>
    public class GetCrystalStatisticsQueryHandler : IRequestHandler<GetCrystalStatisticsQuery, CrystalStatistics>
    {
        private readonly ITransactionRepository _transactionRepository;

        /// <summary>
        /// Конструктор обработчика запроса GetCrystalStatisticsQueryHandler.
        /// </summary>
        /// <param name="transactionRepository">Репозиторий транзакций.</param>
        public GetCrystalStatisticsQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение статистики кристаллов.
        /// </summary>
        /// <param name="request">Запрос GetCrystalStatisticsQuery, содержащий диапазон дат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Объект CrystalStatistics с агрегированными данными.</returns>
        public async Task<CrystalStatistics> Handle(GetCrystalStatisticsQuery request, CancellationToken cancellationToken)
        {
            // Получаем все транзакции из репозитория.
            // В реальной реализации, возможно, потребуется метод в репозитории
            // для получения транзакций с фильтрацией по дате для эффективности.
            var allTransactions = await _transactionRepository.GetTransactionsAsync(request.UserId); 

            // Фильтруем транзакции по заданному диапазону дат.
            var filteredTransactions = allTransactions
                .Where(t => t.Timestamp >= request.StartDate && t.Timestamp <= request.EndDate)
                .ToList();

            // Подсчитываем общее количество начисленных кристаллов.
            int totalCredited = filteredTransactions
                .Where(t => t.Type == "пополнение" || t.Amount > 0) // Предполагаем, что положительная сумма или тип "пополнение" означает начисление
                .Sum(t => t.Amount);

            // Подсчитываем общее количество списанных кристаллов.
            int totalDebited = filteredTransactions
                .Where(t => t.Type == "списание" || t.Amount < 0) // Предполагаем, что отрицательная сумма или тип "списание" означает списание
                .Sum(t => -t.Amount); // Берем абсолютное значение для списаний

            // Возвращаем агрегированные данные в объекте CrystalStatistics.
            return new CrystalStatistics
            {
                TotalCredited = totalCredited,
                TotalDebited = totalDebited
            };
        }
    }
}