csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDatingProfileHelper.Backend.Domain.Models;

namespace MyDatingProfileHelper.Backend.Domain.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для управления транзакциями.
    /// Определяет методы для добавления и получения транзакций.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Асинхронно добавляет новую транзакцию в репозиторий.
        /// </summary>
        /// <param name="transaction">Объект транзакции для добавления.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task AddAsync(Transaction transaction);

        /// <summary>
        /// Асинхронно получает список транзакций для указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию, возвращающую список транзакций пользователя.</returns>
        Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId);

        /// <summary>
        /// Асинхронно получает транзакцию по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор транзакции.</param>
        /// <returns>Задача, представляющая асинхронную операцию, возвращающую транзакцию или null, если не найдена.</returns>
        Task<Transaction?> GetByIdAsync(int id);

        /// <summary>
        /// Асинхронно получает список транзакций для указанного пользователя с возможностью фильтрации по типу.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="transactionType">Тип транзакции для фильтрации (необязательно).</param>
        /// <returns>Задача, представляющая асинхронную операцию, возвращающую список отфильтрованных транзакций пользователя.</returns>
        Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId, string? transactionType);
    }
}