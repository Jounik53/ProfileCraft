csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models;
using System.Collections.Generic;

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос для получения истории транзакций пользователя.
    /// </summary>
    public class GetTransactionHistoryQuery : IRequest<List<Transaction>>
    {
        /// <summary>
        /// Идентификатор пользователя, для которого запрашивается история транзакций.
        /// </summary>
        public int UserId { get; set; }
    }
}