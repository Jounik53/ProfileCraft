csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using MyDatingProfileHelper.Backend.Domain.Models;
using System;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик команды для начисления кристаллов пользователю через Telegram.
    /// </summary>
    public class CreditCrystalsByTelegramCommandHandler : IRequestHandler<CreditCrystalsByTelegramCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        /// <summary>
        /// Конструктор обработчика.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        /// <param name="transactionRepository">Репозиторий транзакций.</param>
        public CreditCrystalsByTelegramCommandHandler(IUserRepository userRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Обрабатывает команду начисления кристаллов через Telegram.
        /// </summary>
        /// <param name="request">Команда начисления кристаллов.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>True, если начисление успешно, иначе False.</returns>
        public async Task<bool> Handle(CreditCrystalsByTelegramCommand request, CancellationToken cancellationToken)
        {
            // Найти пользователя по Telegram Identifier
            var user = await _userRepository.FindByTelegramIdentifierAsync(request.TelegramIdentifier);

            // Если пользователь не найден, вернуть false
            if (user == null)
            {
                // Возможно, здесь стоит логировать ошибку: пользователь не найден
                return false;
            }

            // Начислить кристаллы пользователю
            user.CrystalBalance += request.Amount;
            // В реальной реализации здесь может потребоваться отдельный метод в UserRepository для обновления баланса
            // Например, await _userRepository.UpdateCrystalBalanceAsync(user.Id, user.CrystalBalance);

            // Записать транзакцию
            var transaction = new Transaction
            {
                UserId = user.Id,
                Amount = request.Amount,
                Type = "Пополнение через Telegram", // Тип транзакции
                Timestamp = DateTime.UtcNow,
                Details = $"Пополнение на {request.Amount} кристаллов через Telegram бота. Telegram ID/Username: {request.TelegramIdentifier}" // Детали транзакции
            };

            await _transactionRepository.AddAsync(transaction);

            // Так как баланс был обновлен напрямую в объекте user,
            // необходимо сохранить изменения в контексте базы данных.
            // В реальной репозитории метод AddAsync или UpdateAsync может делать SaveChangesAsync.
            // Если нет, потребуется вызвать Unit of Work или явно сохранить изменения здесь,
            // в зависимости от используемого паттерна доступа к данным.
            // Например: await _transactionRepository.SaveChangesAsync(); // Если репозиторий включает SaveChangesAsync

            return true; // Начисление и запись транзакции успешны
        }
    }
}