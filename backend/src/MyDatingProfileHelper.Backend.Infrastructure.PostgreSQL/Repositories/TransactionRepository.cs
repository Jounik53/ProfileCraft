csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Data;

namespace MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Repositories
{
    /// <summary>
    /// Репозиторий для работы с транзакциями в базе данных PostgreSQL с использованием Entity Framework Core.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Конструктор с внедрением зависимости AppDbContext.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет новую транзакцию в базу данных.
        /// </summary>
        /// <param name="transaction">Объект транзакции.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Возвращает добавленный объект транзакции.</returns>
        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            // Добавляем транзакцию в контекст
            await _context.Transactions.AddAsync(transaction); // Используем AddAsync для асинхронной операции
                                                               // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();
            // Возвращаем добавленную транзакцию
            return transaction;
        }

        /// <summary>
        /// Получает список транзакций для определенного пользователя, с возможностью фильтрации по типу.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="type">Необязательный параметр для фильтрации по типу транзакции.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Возвращает список транзакций.</returns>
        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId)
        {
            // Создаем запрос к таблице транзакций
            var query = _context.Transactions
                                .Where(t => t.UserId == userId);

            // Применяем фильтр по типу, если он указан
            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(t => t.Type == type);
            }

            // Выполняем запрос и возвращаем результат в виде списка
            return await query.ToListAsync();
        }

        public Task<Transaction> GetByIdAsync(int id)
        {
            // Реализация получения транзакции по ID, если необходимо
            throw new NotImplementedException(); // Или реализуйте логику получения
        }
        // Здесь могут быть добавлены другие методы для работы с транзакциями, если потребуется.
    }
}