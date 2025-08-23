csharp
using System.Threading.Tasks;
using MyDatingProfileHelper.Backend.Domain.Models;

namespace MyDatingProfileHelper.Backend.Domain.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для работы с сущностью User.
    /// Определяет операции доступа к данным пользователей.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Находит пользователя по его идентификатору из Google.
        /// </summary>
        /// <param name="googleId">Идентификатор пользователя из Google.</param>
        /// <returns>Объект User, если найден, иначе null.</returns>
        Task<User?> FindByGoogleIdAsync(string googleId);

        /// <summary>
        /// Добавляет нового пользователя в репозиторий.
        /// </summary>
        /// <param name="user">Объект пользователя для добавления.</param>
        /// <returns>Созданный объект пользователя.</returns>
        Task<User> AddAsync(User user);

        /// <summary>
        /// Обновляет баланс кристаллов пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="newBalance">Новый баланс кристаллов.</param>
        /// <returns>True, если обновление прошло успешно, иначе false.</returns>
        Task<bool> UpdateCrystalBalanceAsync(int userId, int newBalance);
    }

 /// <summary>
 /// Списывает кристаллы с баланса пользователя.
 /// </summary>
 /// <param name="userId">Идентификатор пользователя.</param>
 /// <param name="amount">Количество кристаллов для списания.</param>
 /// <returns>True, если списание прошло успешно, иначе false (например, недостаточно средств).</returns>
 Task<bool> DebitCrystalsAsync(int userId, int amount);
}

 /// <summary>
 /// Находит пользователя по его идентификатору из Telegram.
 /// </summary>
 /// <param name="telegramIdentifier">Идентификатор пользователя из Telegram (ID или Username).</param>
 /// <returns>Объект User, если найден, иначе null.</returns>
 Task<User?> FindByTelegramIdentifierAsync(string telegramIdentifier);
 }