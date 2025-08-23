csharp
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Api.DTOs;
using MyDatingProfileHelper.Backend.Application.Commands; // Добавляем using для команды сохранения профиля
using System.Linq; // Потребуется для работы с коллекциями, если Photos, Interests, DatingGoals будут List<string>

namespace MyDatingProfileHelper.Backend.Api.Mappers
{
    /// <summary>
    /// Статический класс для преобразования между моделями профиля пользователя уровня домена и DTO уровня API.
    /// Это помогает изолировать доменные модели от деталей представления API.
    /// </summary>
    public static class UserProfileMapper
    {
        /// <summary>
        /// Преобразует объект UserProfile из доменного слоя в UserProfileDto для использования в API.
        /// </summary>
        /// <param name="userProfile">Объект профиля пользователя из домена.</param>
        /// <returns>DTO профиля пользователя для API.</returns>
        public static UserProfileDto ToUserProfileDto(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                return null;
            }

            // Здесь выполняется преобразование свойств.
            // Если Photos, Interests, DatingGoals хранятся как JSON строки в доменной модели,
            // здесь потребуется десериализация в List<string> для DTO.
            // Для простоты пока предполагаем прямое копирование или базовое преобразование.
            // В реальной реализации потребуется более сложная логика для работы с JSON строками или другими форматами.

            return new UserProfileDto
            {
                // Пример простого маппинга свойств
                Name = userProfile.Name,
                Description = userProfile.Description,
                // Пример, если Photos хранится как строка и нужно преобразовать в List<string> (потребуется логика парсинга JSON)
                // Photos = string.IsNullOrEmpty(userProfile.Photos) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(userProfile.Photos),
                // Пока просто копируем строку
                Photos = userProfile.Photos, // Возможно, потребуется изменение типа в UserProfileDto на string
                Interests = userProfile.Interests, // Аналогично, возможно потребуется изменение типа
                DatingGoals = userProfile.DatingGoals // Аналогично, возможно потребуется изменение типа
                Gender = userProfile.Gender // Маппинг для свойства Gender
                                            // Кристаллы и другие чувствительные или внутренние данные не включаются в этот DTO, если они не нужны клиенту напрямую через этот эндпоинт
            };
        }

        /// <summary>
        /// Преобразует объект SaveUserProfileRequest из уровня API во входные данные для команды SaveUserProfileCommand уровня приложения.
        /// </summary>
        /// <param name="request">Запрос на сохранение профиля пользователя из API.</param>
        /// <param name="userId">Идентификатор пользователя, для которого сохраняется профиль.</param>
        /// <returns>Команда для сохранения профиля пользователя.</returns>
        public static SaveUserProfileCommand ToSaveUserProfileCommand(SaveUserProfileRequest request, int userId)
        {
            if (request == null)
            {
                return null; // Или выбросить исключение, в зависимости от логики
            }

            return new SaveUserProfileCommand
            {
                UserId = userId,
                Name = request.Name,
                Photos = request.Photos, // Предполагаем, что DTO API и команда принимают одинаковый формат (например, List<string>)
                Interests = request.Interests, // Аналогично
                DatingGoals = request.DatingGoals, // Аналогично
                Description = request.Description,
                Gender = request.Gender // Маппинг для свойства Gender
            };
        }
    }
}