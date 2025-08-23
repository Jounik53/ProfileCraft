csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDatingProfileHelper.Backend.Domain.Models; // Убедитесь, что это пространство имен моделей корректно

namespace MyDatingProfileHelper.Backend.Domain.Repositories
{
    /// <summary>
    /// Определяет контракт для репозитория описаний профилей.
    /// Репозитории в слое Domain являются портами, через которые Application слой взаимодействует с данными.
    /// </summary>
    public interface IProfileDescriptionRepository
    {
        /// <summary>
        /// Асинхронно получает список всех доступных описаний профилей.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. Результатом задачи является список объектов ProfileDescription.</returns>
        Task<List<ProfileDescription>> GetAllProfileDescriptionsAsync();

        // Здесь можно добавить другие методы, например:
        // Task<ProfileDescription> GetProfileDescriptionByIdAsync(int id);
        // Task AddProfileDescriptionAsync(ProfileDescription description);
        // Task UpdateProfileDescriptionAsync(ProfileDescription description);
        // Task DeleteProfileDescriptionAsync(int id);
    }
}