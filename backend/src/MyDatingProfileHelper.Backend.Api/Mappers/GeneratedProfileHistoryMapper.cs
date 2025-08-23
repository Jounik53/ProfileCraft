using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Api.DTOs;

namespace MyDatingProfileHelper.Backend.Api.Mappers
{
 /// <summary>
 /// Статический класс для преобразования между моделями истории генерации профилей и DTO уровня API.
 /// </summary>
 public static class GeneratedProfileHistoryMapper
 {
 /// <summary>
 /// Преобразует доменную модель GeneratedProfileHistory в DTO для API.
 /// </summary>
 /// <param name="history">Доменная модель GeneratedProfileHistory.</param>
 /// <returns>DTO GeneratedProfileHistoryDto.</returns>
 public static GeneratedProfileHistoryDto ToGeneratedProfileHistoryDto(GeneratedProfileHistory history)
 {
 return new GeneratedProfileHistoryDto
 {
 Id = history.Id,
 GeneratedText = history.GeneratedText,
 Timestamp = history.Timestamp
 };
 }

 /// <summary>
 /// Преобразует DTO запроса на сохранение истории и Id профиля пользователя в команду для слоя Application.
 /// </summary>
 /// <param name="request">DTO запроса на сохранение.</param>
 /// <param name="userProfileId">Id профиля пользователя.</param>
 /// <returns>Команда SaveGeneratedProfileHistoryCommand.</returns>
 public static SaveGeneratedProfileHistoryCommand ToSaveGeneratedProfileHistoryCommand(SaveGeneratedProfileHistoryRequest request, int userProfileId)
 {
 // Убедитесь, что тип данных InputParameters соответствует
 return new SaveGeneratedProfileHistoryCommand(userProfileId, request.GeneratedText, request.InputParameters);
 }
 }
}
csharp
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Api.DTOs;

namespace MyDatingProfileHelper.Backend.Api.Mappers
{
    /// <summary>
    /// Статический класс для преобразования между моделями истории генерации профилей и DTO уровня API.
    /// </summary>
    public static class GeneratedProfileHistoryMapper
    {
        /// <summary>
        /// Преобразует доменную модель GeneratedProfileHistory в DTO для API.
        /// </summary>
        /// <param name="history">Доменная модель GeneratedProfileHistory.</param>
        /// <returns>DTO GeneratedProfileHistoryDto.</returns>
        public static GeneratedProfileHistoryDto ToGeneratedProfileHistoryDto(GeneratedProfileHistory history)
        {
            return new GeneratedProfileHistoryDto
            {
                Id = history.Id,
                GeneratedText = history.GeneratedText,
                Timestamp = history.Timestamp
            };
        }

        /// <summary>
        /// Преобразует DTO запроса на сохранение истории и Id профиля пользователя в команду для слоя Application.
        /// </summary>
        /// <param name="request">DTO запроса на сохранение.</param>
        /// <param name="userProfileId">Id профиля пользователя.</param>
        /// <returns>Команда SaveGeneratedProfileHistoryCommand.</returns>
        public static SaveGeneratedProfileHistoryCommand ToSaveGeneratedProfileHistoryCommand(SaveGeneratedProfileHistoryRequest request, int userProfileId)
        {
            return new SaveGeneratedProfileHistoryCommand
            {
                UserProfileId = userProfileId,
                GeneratedText = request.GeneratedText,
                InputParameters = request.InputParameters // Убедитесь, что тип данных соответствует
            };
        }
    }
}