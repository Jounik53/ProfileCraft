csharp
using MyDatingProfileHelper.Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Domain.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для управления историей сгенерированных анкет.
    /// Определяет методы для добавления, получения последней записи и получения списка записей истории.
    /// </summary>
    public interface IGeneratedProfileHistoryRepository
    {
        /// <summary>
        /// Асинхронно добавляет новую запись истории генерации анкеты.
        /// </summary>
        /// <param name="historyEntry">Объект записи истории для добавления.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task AddHistoryEntryAsync(GeneratedProfileHistory historyEntry);

        /// <summary>
        /// Асинхронно получает последнюю запись истории генерации анкеты для указанного профиля пользователя.
        /// </summary>
        /// <param name="userProfileId">Идентификатор профиля пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию, возвращающую последнюю запись истории или null, если записей нет.</returns>
        Task<GeneratedProfileHistory?> GetLatestHistoryEntryAsync(int userProfileId);

        /// <summary>
        /// Асинхронно получает список всех записей истории генерации анкет для указанного профиля пользователя.
        /// </summary>
        /// <param name="userProfileId">Идентификатор профиля пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию, возвращающую список записей истории.</returns>
        Task<List<GeneratedProfileHistory>> GetHistoryForUserProfileAsync(int userProfileId);

        /// <summary>
        /// Асинхронно получает список всех записей истории генерации анкет.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию, возвращающую список всех записей истории.</returns>
        Task<IEnumerable<GeneratedProfileHistory>> GetAllAsync();
    }
}