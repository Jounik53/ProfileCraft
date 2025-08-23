csharp
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Repositories
{
    /// <summary>
    /// Репозиторий для работы с новостями, использующий Entity Framework Core.
    /// </summary>
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Конструктор репозитория новостей.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public NewsRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает новость по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор новости.</param>
        /// <returns>Объект новости или null, если новость не найдена.</returns>
        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        /// <summary>
        /// Получает все новости.
        /// </summary>
        /// <returns>Коллекция всех новостей.</returns>
        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News.ToListAsync();
        }

        /// <summary>
        /// Добавляет новую новость.
        /// </summary>
        /// <param name="news">Объект новости для добавления.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task AddAsync(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync(); // Сохраняем изменения
        }

        /// <summary>
        /// Обновляет существующую новость.
        /// </summary>
        /// <param name="news">Объект новости с обновленными данными.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task UpdateAsync(News news)
        {
            _context.Entry(news).State = EntityState.Modified;
            await _context.SaveChangesAsync(); // Сохраняем изменения
        }

        /// <summary>
        /// Удаляет новость по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор новости для удаления.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task DeleteAsync(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync(); // Сохраняем изменения
            }
        }

        /// <summary>
        /// Получает последние новости, опубликованные начиная с указанной даты.
        /// </summary>
        /// <param name="fromDate">Дата, начиная с которой необходимо получить новости (включительно).</param>
        /// <param name="languageCode">Код языка для фильтрации новостей.</param>
        /// <returns>Коллекция новостей на указанном языке, опубликованных начиная с fromDate, отсортированных по дате публикации.</returns>
        public async Task<IEnumerable<News>> GetRecentNewsAsync(DateTime fromDate, string languageCode)
        {
            return await _context.News
                                 .Where(n => n.PublishDate >= fromDate)
                                 .Where(n => n.LanguageCode == languageCode) // Фильтруем по коду языка
                                 .OrderByDescending(n => n.PublishDate) // Сортируем по убыванию даты для получения последних новостей первыми
                                 .ToListAsync();
        }
    }
}