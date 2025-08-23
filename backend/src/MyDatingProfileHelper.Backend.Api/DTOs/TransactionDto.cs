// Пространство имен для DTO API уровня
namespace MyDatingProfileHelper.Backend.Api.DTOs
{
    /// <summary>
    /// DTO для представления информации о транзакции на уровне API.
    /// </summary>
    public class TransactionDto
    {
        /// <summary>
        /// Уникальный идентификатор транзакции.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Количество кристаллов в транзакции (положительное для начисления, отрицательное для списания).
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Тип транзакции (например, "Credit", "Debit", "NeuralNetworkGeneration").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Отметка времени совершения транзакции.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Дополнительные детали о транзакции (например, ID нейросети, информация об оплате).
        /// </summary>
        public string? Details { get; set; }
    }
}
csharp
// Пространство имен для DTO API уровня
namespace MyDatingProfileHelper.Backend.Api.DTOs
{
    /// <summary>
    /// DTO для представления информации о транзакции на уровне API.
    /// </summary>
    public class TransactionDto
    {
        /// <summary>
        /// Количество кристаллов в транзакции (положительное для начисления, отрицательное для списания).
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Тип транзакции (например, "пополнение", "списание за нейросеть").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Отметка времени совершения транзакции.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Дополнительные детали о транзакции (например, ID нейросети, информация об оплате).
        /// </summary>
        public string? Details { get; set; }
    }
}