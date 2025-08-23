csharp
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDatingProfileHelper.Backend.Application.Queries; // Предполагается, что запросы для статистики находятся здесь
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Api.Controllers
{
    /// <summary>
    /// Контроллер для системной статистики и административных функций.
    /// Требует авторизации для администраторов.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Ограничиваем доступ только для пользователей с ролью "Admin"
    public class SystemController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор контроллера системной статистики.
        /// </summary>
        /// <param name="mediator">Медиатор для отправки команд и запросов.</param>
        public SystemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получает общее количество зарегистрированных пользователей.
        /// </summary>
        /// <returns>Количество зарегистрированных пользователей.</returns>
        [HttpGet("registered-user-count")]
        public async Task<IActionResult> GetRegisteredUserCount()
        {
            // Отправляем запрос в слой Application для получения количества пользователей
            var userCount = await _mediator.Send(new GetRegisteredUserCountQuery());
            return Ok(userCount);
        }

        /// <summary>
        /// Получает статистику по кристаллам (куплено/потрачено).
        /// В дальнейшем можно добавить параметры для фильтрации по времени.
        /// </summary>
        /// <returns>Общая статистика по кристаллам.</returns>
        [HttpGet("crystal-statistics")]
        public async Task<IActionResult> GetCrystalStatistics()
        {
            // Отправляем запрос в слой Application для получения статистики кристаллов
            var crystalStats = await _mediator.Send(new GetCrystalStatisticsQuery());
            return Ok(crystalStats); // crystalStats должна быть DTO или простым объектом со статистикой
        }

        /// <summary>
        /// Получает статистику запросов к API из мобильного приложения.
        /// Требует реализации логирования запросов.
        /// </summary>
        /// <returns>Статистика запросов к API.</returns>
        [HttpGet("api-request-statistics")]
        public async Task<IActionResult> GetApiRequestStatistics()
        {
            // Отправляем запрос в слой Application для получения статистики запросов к API
            // (Необходимо реализовать соответствующий запрос и обработчик в слое Application)
            // var requestStats = await _mediator.Send(new GetApiRequestStatisticsQuery());
            // return Ok(requestStats); // requestStats должна быть DTO или простым объектом со статистикой

            // Пока возвращаем заглушку
            return Ok(new { Message = "Статистика запросов к API пока не реализована." });
        }

        // В дальнейшем здесь можно добавить методы для других административных функций,
        // таких как управление пользователями, просмотр логов, загрузка БД и т.д.
    }
}