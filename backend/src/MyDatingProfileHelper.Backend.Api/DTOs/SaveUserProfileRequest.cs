csharp
namespace MyDatingProfileHelper.Backend.Api.DTOs
{
    /// <summary>
    /// DTO для запроса на сохранение или обновление профиля пользователя через API.
    /// Используется для приема данных от клиента.
    /// </summary>
    public class SaveUserProfileRequest
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Список ссылок на фотографии пользователя.
        /// </summary>
        public List<string>? Photos { get; set; }

        /// <summary>
        /// Список интересов пользователя.
        /// </summary>
        public List<string>? Interests { get; set; }

        /// <summary>
        /// Список целей знакомства пользователя.
        /// </summary>
        public List<string>? DatingGoals { get; set; }

        /// <summary>
        /// Описание профиля пользователя.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Пол пользователя.
        /// </summary>
        public string? Gender { get; set; }
    }
}