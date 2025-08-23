csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Data;

namespace MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Repositories
{
    /// <summary>
    /// Репозиторий для управления историей сгенерированных профилей с использованием Entity Framework Core.
    /// </summary>
    public class GeneratedProfileHistoryRepository : IGeneratedProfileHistoryRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Конструктор с внедрением зависимости AppDbContext.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public GeneratedProfileHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Асинхронно добавляет новую запись истории генерации анкеты.
        /// Переименовано с AddAsync для соответствия интерфейсу.
        /// </summary>
        /// <param name="historyEntry">Запись истории для добавления.</param>
        /// <returns>Добавленная запись истории.</returns>
        // Renamed from AddAsync to match interface
        public async Task AddHistoryEntryAsync(GeneratedProfileHistory historyEntry)
        {
            _context.GeneratedProfileHistory.Add(historyEntry);
            await _context.SaveChangesAsync();
            return historyEntry;
        }

        /// <summary>
        /// Асинхронно получает последнюю запись истории генерации анкеты для указанного профиля пользователя.
        /// Переименовано с GetLatestAsync для соответствия интерфейсу.
        /// </summary>
        /// <param name="userProfileId">Идентификатор профиля пользователя.</param>
        /// <returns>Последняя запись истории или null, если история отсутствует.</returns>
        // Renamed from GetLatestAsync to match interface
        public async Task<GeneratedProfileHistory?> GetLatestHistoryEntryAsync(int userProfileId)
        {
            // Находим последнюю запись истории по времени генерации для данного профиля пользователя.
            return await _context.GeneratedProfileHistory
                .Where(h => h.UserProfileId == userProfileId)
                .OrderByDescending(h => h.Timestamp)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Асинхронно получает список всех записей истории генерации анкет для указанного профиля пользователя.
        /// Переименовано с GetListAsync для соответствия интерфейсу.
        /// </summary>
        /// <param name="userProfileId">Идентификатор профиля пользователя.</param>
        /// <returns>Список записей истории сгенерированных профилей.</returns>
        // Renamed from GetListAsync to match interface
        public async Task<List<GeneratedProfileHistory>> GetHistoryForUserProfileAsync(int userProfileId)
        {
            // Получаем все записи истории для данного профиля пользователя, отсортированные по времени генерации.
            return await _context.GeneratedProfileHistory
                .Where(h => h.UserProfileId == userProfileId)
                .OrderByDescending(h => h.Timestamp)
                .ToListAsync();
        }

        /// <summary>
        /// Асинхронно получает все записи истории генерации анкет.
        /// Реализация нового метода из интерфейса.
        /// </summary>
        /// <returns>Коллекция всех записей истории.</returns>
        public async Task<IEnumerable<GeneratedProfileHistory>> GetAllAsync()
        {
            return await _context.GeneratedProfileHistory.ToListAsync();
        }
    }
}