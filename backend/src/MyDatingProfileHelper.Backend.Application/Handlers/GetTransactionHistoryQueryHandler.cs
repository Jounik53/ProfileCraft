csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса на получение истории транзакций пользователя.
    /// </summary>
    public class GetTransactionHistoryQueryHandler : IRequestHandler<GetTransactionHistoryQuery, List<Transaction>>
    {
        private readonly ITransactionRepository _transactionRepository;

        /// <summary>
        /// Конструктор обработчика запроса истории транзакций.
        /// </summary>
        /// <param name="transactionRepository">Репозиторий транзакций.</param>
        public GetTransactionHistoryQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение истории транзакций.
        /// </summary>
        /// <param name="request">Запрос на получение истории транзакций.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список транзакций пользователя.</returns>
        public async Task<List<Transaction>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            // Использование репозитория транзакций для получения списка транзакций для указанного пользователя.
            // В реальной реализации может потребоваться добавить фильтрацию по типу транзакции, пагинацию и т.д.
            return await _transactionRepository.GetTransactionsByUserIdAsync(request.UserId);
        }
    }
}
