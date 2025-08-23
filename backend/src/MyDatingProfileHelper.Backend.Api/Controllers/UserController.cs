csharp
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDatingProfileHelper.Backend.Application.Queries;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с данными пользователя, не связанными с профилем.
    /// </summary>
    [Authorize] // Этот контроллер требует аутентификации
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор контроллера пользователя.
        /// </summary>
        /// <param name="mediator">Медиатор для отправки команд и запросов.</param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получает текущий баланс кристаллов авторизованного пользователя.
        /// </summary>
        /// <returns>Текущий баланс кристаллов.</returns>
        [HttpGet("balance")]
        public async Task<ActionResult<int>> GetCrystalBalance()
        {
            // Получаем ID пользователя из контекста аутентификации.
            // В реальном приложении нужно убедиться, что ID пользователя
            // корректно добавляется в клеймы JWT при авторизации.
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                // Если ID пользователя не найден в клеймах, возвращаем ошибку авторизации.
                return Unauthorized("Не удалось определить пользователя.");
            }

            // Создаем и отправляем запрос на получение баланса кристаллов.
            var query = new GetUserCrystalBalanceQuery { UserId = userId };
            var balance = await _mediator.Send(query);

            // Возвращаем баланс кристаллов.
            return Ok(balance);
        }

        /// <summary>
        /// Получает текущий баланс кристаллов пользователя по его Telegram ID или Username.
        /// Доступен без авторизации для бота.
        /// </summary>
        /// <param name="telegramIdentifier">Telegram User ID или Username пользователя.</param>
        /// <returns>Текущий баланс кристаллов.</returns>
        [AllowAnonymous] // Этот endpoint доступен без авторизации (для бота)
        [HttpGet("balance/telegram/{telegramIdentifier}")]
        public async Task<ActionResult<int>> GetCrystalBalanceByTelegramIdentifier(string telegramIdentifier)
        {
            // Создаем и отправляем запрос на получение баланса кристаллов по Telegram идентификатору.
            var query = new GetUserCrystalBalanceByTelegramIdentifierQuery { TelegramIdentifier = telegramIdentifier };
            var balance = await _mediator.Send(query);

            // TODO: Добавить обработку случая, когда пользователь не найден (например, вернуть 404 Not Found).
            // В текущей реализации обработчик запроса может вернуть 0 или выбросить исключение.
            return Ok(balance);
        }
    }
}