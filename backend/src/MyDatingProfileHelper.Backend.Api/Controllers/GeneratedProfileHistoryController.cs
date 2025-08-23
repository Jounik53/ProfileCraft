csharp
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDatingProfileHelper.Backend.Api.DTOs;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Api.Mappers; // Добавлено использование маппера
using MyDatingProfileHelper.Backend.Application.Queries;
using System.Security.Claims;
using MyDatingProfileHelper.Backend.Domain.Repositories; // Добавлен импорт репозитория профилей
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Api.Controllers
{
    /// <summary>
    /// Контроллер для управления историей сгенерированных анкет.
    /// </summary>
    [Authorize] // Защита контроллера, требует авторизации
    [ApiController]
    [Route("api/[controller]")]
    public class GeneratedProfileHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserProfileRepository _userProfileRepository; // Репозиторий профилей пользователей

        /// <summary>
        /// Конструктор контроллера истории сгенерированных анкет.
        /// </summary>
        /// <param name="mediator">Экземпляр MediatR для отправки команд и запросов.</param>
        /// <param name="userProfileRepository">Репозиторий профилей пользователей.</param>
        public GeneratedProfileHistoryController(IMediator mediator, IUserProfileRepository userProfileRepository)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получает ID пользователя из контекста авторизации.
        /// </summary>
        private int GetUserId()
        {
            // В реальном приложении нужно убедиться, что ClaimTypes.NameIdentifier содержит корректный ID пользователя из БД
            // После успешной авторизации JWT должен содержать этот клейм
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            // В случае ошибки или отсутствия клейма, возможно, стоит выбросить исключение или вернуть ошибку
            throw new UnauthorizedAccessException("User ID not found in claims.");
        }

        /// <summary>
        /// Сохраняет новую запись истории сгенерированной анкеты.
        /// </summary>
        /// <param name="request">Данные для сохранения истории.</param>
        /// <returns>Информация о сохраненной записи истории.</returns>
        [HttpPost]
        public async Task<ActionResult<GeneratedProfileHistoryDto>> SaveHistory([FromBody] SaveGeneratedProfileHistoryRequest request)
        {
            var userId = GetUserId();
            
            // Получаем профиль пользователя по User ID, чтобы получить UserProfileId
            var userProfile = await _userProfileRepository.GetByUserIdAsync(userId);
            if (userProfile == null)
            {
                return NotFound("Профиль пользователя не найден."); // Возвращаем 404 если профиль не найден
            }
            var userProfileId = userProfile.Id; // Получаем фактический UserProfileId

            var command = new SaveGeneratedProfileHistoryCommand
            {
                UserProfileId = userProfileId,
                GeneratedText = request.GeneratedText,
                InputParameters = request.InputParameters // Если InputParameters передаются
            };

            var result = await _mediator.Send(command);

            // Преобразование результата в DTO для ответа API
            return Ok(GeneratedProfileHistoryMapper.ToGeneratedProfileHistoryDto(result));
        }

        /// <summary>
        /// Получает последнюю запись истории сгенерированной анкеты для текущего пользователя.
        /// </summary>
        /// <returns>Последняя запись истории или null, если нет записей.</returns>
        [HttpGet("latest")]
        public async Task<ActionResult<GeneratedProfileHistoryDto?>> GetLatestHistory()
        {
            var userId = GetUserId();
            
            // Получаем профиль пользователя по User ID
            var userProfile = await _userProfileRepository.GetByUserIdAsync(userId);
            if (userProfile == null)
            {
                return NotFound("Профиль пользователя не найден.");
            }
            var userProfileId = userProfile.Id;

            var query = new GetLatestGeneratedProfileHistoryQuery { UserProfileId = userProfileId };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            // Преобразование результата в DTO
            return Ok(GeneratedProfileHistoryMapper.ToGeneratedProfileHistoryDto(result));
        }

        /// <summary>
        /// Получает весь список истории сгенерированных анкет для текущего пользователя.
        /// </summary>
        /// <returns>Список записей истории.</returns>
        [HttpGet]
        public async Task<ActionResult<List<GeneratedProfileHistoryDto>>> GetAllHistory()
        {
            var userId = GetUserId();
            
            // Получаем профиль пользователя по User ID
            var userProfile = await _userProfileRepository.GetByUserIdAsync(userId);
            if (userProfile == null)
            {
                return NotFound("Профиль пользователя не найден.");
            }
            var userProfileId = userProfile.Id;

            var query = new GetAllGeneratedProfileHistoryQuery { UserProfileId = userProfileId };
            var result = await _mediator.Send(query);

            // Преобразование списка результатов в список DTO
            return Ok(result.Select(GeneratedProfileHistoryMapper.ToGeneratedProfileHistoryDto).ToList());
        }
    }
}