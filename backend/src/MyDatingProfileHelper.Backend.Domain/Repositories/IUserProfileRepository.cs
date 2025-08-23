csharp
using MyDatingProfileHelper.Backend.Domain.Models;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Domain.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для управления профилями пользователей.
    /// Определяет методы для получения, добавления и обновления профилей.
    /// </summary>
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Асинхронно получает профиль пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Профиль пользователя, если найден, иначе null.</returns>
        Task<UserProfile?> GetByUserIdAsync(int userId);

        /// <summary>
        /// Асинхронно добавляет новый профиль пользователя.
        /// </summary>
        /// <param name="userProfile">Объект профиля пользователя для добавления.</param>
        /// <returns>Добавленный объект профиля пользователя.</returns>
        Task<UserProfile> AddAsync(UserProfile userProfile);

        /// <summary>
        /// Асинхронно обновляет существующий профиль пользователя.
        /// </summary>
        /// <param name="userProfile">Объект профиля пользователя для обновления.</param>
        /// <returns>Обновленный объект профиля пользователя.</returns>
        Task<UserProfile> UpdateAsync(UserProfile userProfile);
    }
}