csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models;

namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для создания новой транзакции.
    /// </summary>
    public class CreateTransactionCommand : IRequest<Transaction>
    {
        /// <summary>
        /// Идентификатор пользователя, совершившего транзакцию.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Сумма транзакции (положительная для зачисления, отрицательная для списания).
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Тип транзакции (например, "Credit", "Debit", "NeuralNetworkGeneration").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Дополнительные детали транзакции (необязательно).
        /// </summary>
        public string? Details { get; set; }
    }
}