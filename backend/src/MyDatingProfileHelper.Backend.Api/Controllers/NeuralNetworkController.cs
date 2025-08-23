csharp
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Application.Queries;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // Для авторизации
using System.Security.Claims; // Для получения UserId из Claims
using System.Collections.Generic; // Для списка
using MyDatingProfileHelper.Backend.Api.Mappers; // Для маппера

namespace MyDatingProfileHelper.Backend.Api.Controllers
{
    [Authorize] // Защита контроллера - требует авторизации пользователя
    [ApiController]
    [Route("api/[controller]")] // Базовый маршрут для контроллера
    public class NeuralNetworkController : ControllerBase
    {
        private readonly IMediator _mediator; // Медиатор для отправки команд и запросов

        /// <summary>
        /// Конструктор контроллера NeuralNetworkController
        /// </summary>
        /// <param name="mediator">Экземпляр IMediator для взаимодействия с уровнем Application</param>
        public NeuralNetworkController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint для генерации описания профиля с помощью нейросети.
        /// Требует авторизации пользователя.
        /// </summary>
        /// <returns>Сгенерированный текст описания профиля</returns>
        [HttpPost("generate")] // HTTP POST запрос на маршрут /api/NeuralNetwork/generate
        public async Task<IActionResult> GenerateProfileDescription()
        {
            // Получаем ID пользователя из контекста авторизации.
            // Предполагается, что ID пользователя (из нашей БД) сохранен в клеймах JWT.
            // Необходимо реализовать логику получения UserId из ClaimsPrincipal.
            // Например: var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Для простоты, пока используем заглушку или предполагаем, что UserId будет доступен.
            // В реальной реализации здесь нужно будет получить ID текущего пользователя.
            // var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            // if (userIdClaim == null)
            // {
            //     return Unauthorized("User ID not found in token claims.");
            // }
            // if (!int.TryParse(userIdClaim.Value, out var userId))
            // {
            //     return Unauthorized("Invalid User ID format in token claims.");
            // }

            // Заглушка для UserId - замените на получение реального ID пользователя из авторизации
            int userId = 1; // Пример: Получение ID пользователя из контекста авторизации

            // Создаем команду для генерации описания профиля
            var command = new GenerateProfileDescriptionCommand { UserId = userId };

            // Отправляем команду в MediatR для выполнения в уровне Application
            var generatedDescription = await _mediator.Send(command);

            // Возвращаем сгенерированный текст в ответе
            return Ok(generatedDescription);
        }

        /// <summary>
        /// Endpoint для получения случайных примеров сгенерированных описаний профилей.
        /// Требует авторизации пользователя.
        /// </summary>
        /// <param name="count">Количество случайных примеров для получения (по умолчанию 5).</param>
        /// <returns>Список DTO случайных примеров описаний.</returns>
        [HttpGet("random-examples")] // HTTP GET запрос на маршрут /api/NeuralNetwork/random-examples
        public async Task<ActionResult<List<DTOs.ProfileDescriptionDto>>> GetRandomExamples([FromQuery] int count = 5)
        {
            // Создаем запрос для получения случайных примеров описаний
            var query = new GetRandomProfileDescriptionsQuery { Count = count };

            // Отправляем запрос в MediatR для выполнения в уровне Application
            var randomDescriptions = await _mediator.Send(query);

            // Преобразуем доменные модели в DTO и возвращаем их
            return Ok(randomDescriptions.Select(ProfileDescriptionMapper.ToProfileDescriptionDto).ToList());
        }
    }
}