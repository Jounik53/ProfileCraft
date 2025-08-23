csharp
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Data;

namespace MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Repositories
{
    /// <summary>
    /// Репозиторий для управления профилями пользователей, использующий Entity Framework Core.
    /// Реализует интерфейс IUserProfileRepository из слоя Domain.
    /// </summary>
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Конструктор репозитория профилей пользователей.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public UserProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет новый профиль пользователя в базу данных.
        /// </summary>
        /// <param name="profile">Объект UserProfile для добавления.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Возвращает добавленный объект UserProfile.</returns>
        public async Task<UserProfile> AddProfileAsync(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        /// <summary>
        /// Получает профиль пользователя по его идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Возвращает объект UserProfile или null, если профиль не найден.</returns>
        public async Task<UserProfile?> GetProfileByUserIdAsync(int userId)
        {
            // Ищем профиль по UserId. Возможно, потребуется также включать связанные данные, например, фото.
            return await _context.UserProfiles
                                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        /// <summary>
        /// Обновляет существующий профиль пользователя в базе данных.
        /// </summary>
        /// <param name="profile">Объект UserProfile с обновленными данными.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task UpdateProfileAsync(UserProfile profile)
        {
            // Entity Framework Core отслеживает изменения в объектах, полученных из контекста.
            // Если объект был изменен вне контекста, возможно, потребуется явно указать его состояние:
            // _context.Entry(profile).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}