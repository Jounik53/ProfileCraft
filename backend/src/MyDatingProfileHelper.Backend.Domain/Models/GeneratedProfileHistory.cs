csharp
using System;

namespace MyDatingProfileHelper.Backend.Domain.Models
{
    /// <summary>
    /// Модель данных для истории сгенерированных анкет.
    /// Связана с профилем пользователя.
    /// </summary>
    public class GeneratedProfileHistory
    {
        /// <summary>
        /// Уникальный идентификатор записи истории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор профиля пользователя, к которому относится запись.
        /// Внешний ключ к таблице UserProfiles.
        /// </summary>
        public int UserProfileId { get; set; }

        /// <summary>
        /// Текст сгенерированной анкеты.
        /// </summary>
        public string GeneratedText { get; set; }

        /// <summary>
        /// Время, когда была сгенерирована анкета.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Параметры, использованные для генерации анкеты (например, в формате JSON).
        /// </summary>
        public string InputParameters { get; set; } // Можно использовать JSON строку для хранения параметров

        /// <summary>
        /// Навигационное свойство к профилю пользователя.
        /// </summary>
        public UserProfile UserProfile { get; set; }
    }
}