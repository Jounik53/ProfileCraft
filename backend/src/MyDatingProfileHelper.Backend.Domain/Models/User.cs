csharp
// Пространство имен для моделей данных
namespace MyDatingProfileHelper.Backend.Domain.Models
{
    /// <summary>
    /// Класс, представляющий пользователя в системе.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя (первичный ключ).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя из Google Authentication.
        /// </summary>
        public string GoogleId { get; set; }

        /// <summary>
        /// Адрес электронной почты пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Дата и время регистрации пользователя.
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Дата и время последнего входа пользователя.
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Баланс кристаллов пользователя.
        /// </summary>
        public int CrystalBalance { get; set; }
        
        /// <summary>
        /// Имя пользователя в Telegram.
        /// </summary>
        public string? TelegramUsername { get; set; }
        
        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        public string? PhoneNumber { get; set; }

        // Навигационные свойства для связей с другими моделями
        // Например, связь один-к-одному с UserProfile
        public UserProfile UserProfile { get; set; }

        // Связь один-ко-многим с транзакциями
        public ICollection<Transaction> Transactions { get; set; }
    }
}