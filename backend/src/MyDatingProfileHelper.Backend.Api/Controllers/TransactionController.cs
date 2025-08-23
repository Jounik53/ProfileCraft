csharp
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Api.DTOs;
using MyDatingProfileHelper.Backend.Api.Mappers;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Application.Queries; // Добавляем импорт для GetUserByTelegramIdentifierQuery
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims; // Для получения UserId из контекста авторизации
using System.Linq; // Для Select

namespace MyDatingProfileHelper.Backend.Api.Controllers
{
    /// <summary>
    /// Контроллер для управления транзакциями пользователя.
    /// </summary>
    [Authorize] // Указывает, что для доступа к этому контроллеру требуется авторизация
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор контроллера транзакций.
        /// </summary>
        /// <param name="mediator">Экземпляр медиатора для отправки команд и запросов.</param>
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получает историю транзакций текущего авторизованного пользователя.
        /// </summary>
        /// <returns>Список DTO транзакций.</returns>
        [HttpGet("history")]
        public async Task<ActionResult<List<TransactionDto>>> GetTransactionHistory()
        {
            // Получаем ID пользователя из контекста авторизации.
            // Предполагается, что при генерации JWT в нем был сохранен ClaimTypes.NameIdentifier с UserId.
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                // Если UserId не найден или недействителен в токене
                return Unauthorized("Неверный токен авторизации.");
            }

            // Создаем запрос для получения истории транзакций
            var query = new GetUserTransactionsQuery { UserId = userId };

            // Отправляем запрос через медиатор в слой Application
            var transactions = await _mediator.Send(query);

            // Преобразуем доменные модели транзакций в DTO для API
            var transactionDtos = transactions.Select(TransactionMapper.ToTransactionDto).ToList();

            return Ok(transactionDtos);
        }

        /// <summary>
        /// Извлекает ID пользователя из Claims принципа авторизованного пользователя.
        /// </summary>
        /// <returns>ID пользователя.</returns>
        /// <exception cref="UnauthorizedAccessException">Выбрасывается, если ID пользователя не найден в Claims.</exception>
        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim?.Value ?? throw new UnauthorizedAccessException("User ID not found in claims."));
        }
        
        /// <summary>
        /// Принимает информацию об оплате из Telegram бота и обновляет баланс пользователя.
        /// </summary>
        /// <param name="request">DTO с информацией об оплате.</param>
        /// <returns>Статус операции.</returns>
        // Добавляем политику авторизации для бота (нужно будет определить эту политику)
        // [Authorize(Policy = "BotPolicy")] 
        [HttpPost("credit/telegram")]
        public async Task<ActionResult> CreditCrystalsForTelegramUser([FromBody] CreditCrystalsTelegramRequest request)
        {
            // Check for the API key in the request header
            if (!Request.Headers.TryGetValue("X-API-Key", out var apiKeyHeader) || apiKeyHeader.FirstOrDefault() != "YOUR_BACKEND_API_KEY_HERE" || apiKeyHeader.Count != 1)
            {
                return Unauthorized("Invalid API Key.");
            }

            // Находим пользователя по TelegramIdentifier, чтобы получить UserId для пополнения и записи транзакции
            var userQuery = new GetUserByTelegramIdentifierQuery { TelegramIdentifier = request.TelegramIdentifier };
            var user = await _mediator.Send(userQuery);

            // Если пользователь не найден, возвращаем ошибку
            if (user == null)
            {
                // В зависимости от требований, можно вернуть NotFound или другой статус
                return NotFound($"Пользователь с Telegram Identifier {request.TelegramIdentifier} не найден.");
            }

            // Создаем команду для пополнения баланса через Telegram
            var command = new CreditCrystalsByTelegramCommand
            {
                UserId = user.Id, // Используем полученный UserId
                Amount = request.Amount
            };
            await _mediator.Send(command);


            if (user != null)
            {
                // Создаем команду для записи транзакции
                var transactionCommand = new CreateTransactionCommand { UserId = user.Id, Amount = request.Amount, Type = "Credit", Details = "Пополнение через Telegram" };
                await _mediator.Send(transactionCommand);
            }
 // Or a more detailed response
            return Ok("Баланс пользователя успешно обновлен и транзакция записана.");
        }
    }
}