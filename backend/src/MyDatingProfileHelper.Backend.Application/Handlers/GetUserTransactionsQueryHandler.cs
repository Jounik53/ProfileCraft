csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса на получение истории транзакций пользователя.
    /// </summary>
    public class GetUserTransactionsQueryHandler : IRequestHandler<GetUserTransactionsQuery, IEnumerable<Transaction>>
    {
        private readonly ITransactionRepository _transactionRepository;

        /// <summary>
        /// Конструктор обработчика запроса.
        /// </summary>
        /// <param name="transactionRepository">Репозиторий транзакций.</param>
        public GetUserTransactionsQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение истории транзакций пользователя.
        /// </summary>
        /// <param name="request">Запрос на получение истории транзакций.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Коллекция транзакций пользователя.</returns>
        public async Task<IEnumerable<Transaction>> Handle(GetUserTransactionsQuery request, CancellationToken cancellationToken)
        {
            // Вызываем метод репозитория для получения транзакций пользователя по его ID
            return await _transactionRepository.GetByUserIdAsync(request.UserId);
        }
    }
}