csharp
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Api.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;
using MyDatingProfileHelper.Backend.Api.Mappers; // Предполагается, что здесь находится маппер

namespace MyDatingProfileHelper.Backend.Api.Controllers
{
    /// <summary>
    /// Контроллер для управления профилями пользователей.
    /// </summary>
    [Authorize] // Защищаем все эндпоинты этого контроллера авторизацией JWT
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор контроллера профиля пользователя.
        /// </summary>
        /// <param name="mediator">Экземпляр MediatR для отправки команд и запросов.</param>
        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получает профиль текущего авторизованного пользователя.
        /// </summary>
        /// <returns>Профиль пользователя.</returns>
        [HttpGet]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile()
        {
            // Получаем UserId из контекста аутентифицированного пользователя.
            // Важно: корректное извлечение User ID зависит от того, как вы настроили генерацию и парсинг JWT.
            // Обычно User ID хранится в одном из клеймов JWT.
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // Пример: если User ID в клейме NameIdentifier
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                // Если UserId не найден или некорректен в клейме, возвращаем ошибку.
                return Unauthorized("Не удалось получить идентификатор пользователя из токена.");
            }

            // Создаем запрос к слою Application через MediatR.
            var query = new GetUserProfileQuery { UserId = userId };
            var userProfile = await _mediator.Send(query);

            if (userProfile == null)
            {
                // Если профиль не найден (например, пользователь новый), возвращаем 404 Not Found.
                return NotFound("Профиль пользователя не найден.");
            }

            // Преобразуем модель домена/приложения в DTO уровня API.
            var userProfileDto = UserProfileMapper.ToUserProfileDto(userProfile);

            // Возвращаем DTO.
            return Ok(userProfileDto);
        }

        /// <summary>
        /// Сохраняет или обновляет профиль текущего авторизованного пользователя.
        /// </summary>
        /// <param name="profileDto">Данные профиля для сохранения/обновления.</param>
        /// <returns>Статус операции.</returns>
        [HttpPost]
        public async Task<ActionResult> SaveUserProfile([FromBody] SaveUserProfileRequest profileDto) // Предполагается DTO для входных данных
        {
             // Получаем UserId из контекста аутентифицированного пользователя.
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("Не удалось получить идентификатор пользователя из токена.");
            }

            // Создаем команду для слоя Application через MediatR.
            // Преобразуем данные из входного DTO в формат, подходящий для команды (если требуется).
            var command = new SaveUserProfileCommand
            {
                UserId = userId,
                Name = profileDto.Name,
                Photos = profileDto.Photos,
                Interests = profileDto.Interests,
                DatingGoals = profileDto.DatingGoals,
                Description = profileDto.Description
                // Возможно, здесь потребуется дополнительное маппирование для списков
            };

            // Отправляем команду. MediatR вызовет соответствующий обработчик.
            var updatedProfile = await _mediator.Send(command);

            // В зависимости от логики обработки команды, можно вернуть updatedProfile или просто статус.
            // Например, можно вернуть 200 OK или 201 Created, если это первое сохранение.
             return Ok("Профиль успешно сохранен/обновлен.");
            // Если команда возвращает обновленный профиль, можно его вернуть в DTO:
            // var updatedProfileDto = UserProfileMapper.ToUserProfileDto(updatedProfile);
            // return Ok(updatedProfileDto);
        }

        // Здесь будут добавлены другие эндпоинты, например, для работы с фотографиями, интересами и т.д.
    }
}