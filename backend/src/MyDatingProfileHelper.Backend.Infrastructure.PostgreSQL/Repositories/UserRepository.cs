csharp
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Repositories
{
    /// <summary>
    /// Реализация репозитория пользователей с использованием Entity Framework Core.
    /// Взаимодействует с базой данных PostgreSQL.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Конструктор репозитория пользователей.
        /// </summary>
        /// <param name="context">Контекст базы данных Entity Framework Core.</param>
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет нового пользователя в базу данных.
        /// </summary>
        /// <param name="user">Объект пользователя для добавления.</param>
        /// <returns>Асинхронная операция.</returns>
        public async Task AddUserAsync(User user)
        {
            // Добавление пользователя в DbSet
            await _context.Users.AddAsync(user);
            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Находит пользователя по его Google ID.
        /// </summary>
        /// <param name="googleId">Google ID пользователя.</param>
        /// <returns>Найденный объект пользователя или null, если пользователь не найден.</returns>
        public async Task<User?> FindByGoogleIdAsync(string googleId)
        {
            // Поиск пользователя в DbSet по GoogleId
            return await _context.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId);
        }

        /// <summary>
        /// Обновляет баланс кристаллов пользователя в базе данных.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="newBalance">Новый баланс кристаллов.</param>
        /// <returns>Асинхронная операция.</returns>
        public async Task UpdateCrystalBalanceAsync(int userId, int newBalance)
        {
            // Находим пользователя по ID
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                // Обновляем баланс кристаллов
                user.CrystalBalance = newBalance;
                // Сохраняем изменения
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Списывает указанное количество кристаллов с баланса пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="amount">Количество кристаллов для списания.</param>
        /// <returns>True, если кристаллы успешно списаны; False, если у пользователя недостаточно кристаллов или пользователь не найден.</returns>
        public async Task<bool> DebitCrystalsAsync(int userId, int amount)
        {
            // Находим пользователя по ID
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false; // Пользователь не найден
            }

            if (user.CrystalBalance < amount)
            {
                return false; // Недостаточно кристаллов
            }

            user.CrystalBalance -= amount; // Уменьшаем баланс
            await _context.SaveChangesAsync(); // Сохраняем изменения
            return true; // Списание успешно
        }

        /// <summary>
        /// Находит пользователя по его Telegram Identifier.
        /// </summary>
        /// <param name="telegramIdentifier">Telegram Identifier пользователя (Username или User ID).</param>
        /// <returns>Найденный объект пользователя или null, если пользователь не найден.</returns>
        public async Task<User?> FindByTelegramIdentifierAsync(string telegramIdentifier)
        {
            // Поиск пользователя в DbSet по TelegramUsername или TelegramUserId
            return await _context.Users.FirstOrDefaultAsync(u => u.TelegramUsername == telegramIdentifier || u.TelegramUserId == telegramIdentifier);
        }

        // Здесь могут быть реализованы другие методы из IUserRepository, если они добавлены
    }
}