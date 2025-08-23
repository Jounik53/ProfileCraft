csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик команды для создания новой транзакции.
    /// </summary>
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Transaction>
    {
        private readonly ITransactionRepository _transactionRepository;

        /// <summary>
        /// Конструктор обработчика команды.
        /// </summary>
        /// <param name="transactionRepository">Репозиторий транзакций.</param>
        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Обрабатывает команду создания транзакции.
        /// </summary>
        /// <param name="request">Команда для создания транзакции.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Созданный объект транзакции.</returns>
        public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            // Создаем новый объект транзакции из данных команды
            var transaction = new Transaction
            {
                UserId = request.UserId,
                Amount = request.Amount,
                Type = request.Type,
                Details = request.Details,
                Timestamp = DateTime.UtcNow // Устанавливаем текущее время транзакции
            };

            // Добавляем транзакцию в репозиторий
            await _transactionRepository.AddAsync(transaction);

            // Возвращаем созданную транзакцию
            return transaction;
        }
    }
}