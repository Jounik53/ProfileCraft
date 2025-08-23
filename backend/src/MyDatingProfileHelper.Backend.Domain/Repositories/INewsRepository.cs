csharp
using MyDatingProfileHelper.Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Domain.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для работы с сущностью "Новость".
    /// Определяет операции доступа к данным новостей.
    /// </summary>
    public interface INewsRepository
    {
        /// <summary>
        /// Асинхронно получает новость по ее уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор новости.</param>
        /// <returns>Новость с указанным идентификатором или null, если не найдена.</returns>
        Task<News> GetByIdAsync(int id);

        /// <summary>
        /// Асинхронно получает все новости.
        /// </summary>
        /// <returns>Коллекция всех новостей.</returns>
        Task<IEnumerable<News>> GetAllAsync();

        /// <summary>
        /// Асинхронно добавляет новую новость в хранилище.
        /// </summary>
        /// <param name="news">Объект новости для добавления.</param>
        Task AddAsync(News news);

        /// <summary>
        /// Асинхронно обновляет существующую новость в хранилище.
        /// </summary>
        /// <param name="news">Объект новости для обновления.</param>
        Task UpdateAsync(News news);

        /// <summary>
        /// Асинхронно удаляет новость из хранилища по ее уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор новости для удаления.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Асинхронно получает новости, опубликованные начиная с указанной даты.
        /// </summary>
        /// <param name="fromDate">Дата, с которой начинать поиск новостей (включительно).
        /// <param name="languageCode">Код языка для фильтрации новостей.</param>
        /// <returns>Коллекция новостей, опубликованных начиная с указанной даты.</returns>
        Task<IEnumerable<News>> GetRecentNewsAsync(DateTime fromDate, string languageCode);
    }
}