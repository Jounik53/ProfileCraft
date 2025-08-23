csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models; // Убедитесь, что путь к модели Transaction правильный
using System.Collections.Generic;

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос на получение истории транзакций пользователя.
    /// </summary>
    public class GetUserTransactionsQuery : IRequest<IEnumerable<Transaction>>
    {
        /// <summary>
        /// Идентификатор пользователя, историю транзакций которого нужно получить.
        /// </summary>
        public int UserId { get; set; }
    }
}