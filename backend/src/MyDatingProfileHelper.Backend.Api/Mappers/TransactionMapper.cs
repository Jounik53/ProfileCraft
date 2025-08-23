csharp
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Api.DTOs;

namespace MyDatingProfileHelper.Backend.Api.Mappers
{
    /// <summary>
    /// Статический класс для преобразования моделей транзакций в DTO для API.
    /// </summary>
    public static class TransactionMapper
    {
        /// <summary>
        /// Преобразует объект Transaction доменного уровня в TransactionDto для API.
        /// </summary>
        /// <param name="transaction">Объект транзакции из домена.</param>
        /// <returns>Объект TransactionDto для API.</returns>
        public static TransactionDto ToTransactionDto(Transaction transaction)
        {
            // Простая реализация преобразования. В реальном приложении может потребоваться более сложное маппирование.
            return new TransactionDto
            {
                Id = transaction.Id, // Добавляем маппинг Id
                Amount = transaction.Amount,
                Type = transaction.Type,
                Timestamp = transaction.Timestamp,
                Details = transaction.Details
            };
        }

        /// <summary>
        /// Преобразует коллекцию объектов Transaction доменного уровня в коллекцию TransactionDto для API.
        /// </summary>
        /// <param name="transactions">Коллекция объектов транзакций из домена.</param>
        /// <returns>Коллекция объектов TransactionDto для API.</returns>
        public static IEnumerable<TransactionDto> ToTransactionDto(IEnumerable<Transaction> transactions)
        {
            return transactions.Select(t => ToTransactionDto(t));
        }
    }
}