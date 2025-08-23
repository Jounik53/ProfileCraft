csharp
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Api.DTOs;
using MyDatingProfileHelper.Backend.Api.Mappers;
using MyDatingProfileHelper.Backend.Domain.Models; // Необходимо для CRUD операций с моделью News
using MyDatingProfileHelper.Backend.Application.Commands; // Необходимо для команд CRUD
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MyDatingProfileHelper.Backend.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с новостями.
    /// </summary>
    [Authorize] // Требует авторизации для доступа ко всему контроллеру
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор контроллера новостей.
        /// </summary>
        /// <param name="mediator">Экземпляр медиатора для отправки команд и запросов.</param>
        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получает последние новости за указанное количество дней (по умолчанию сегодня и вчера).
        /// </summary>
        /// <param name="days">Количество дней назад от сегодняшней даты для включения новостей.</param>
        /// <returns>Список DTO последних новостей.</returns>
        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetRecent([FromQuery] int days = 2)
        {
            // Получаем код языка из заголовка Accept-Language. Если заголовок отсутствует, используем "ru" по умолчанию.
 var languageCode = Request.Headers["Accept-Language"].FirstOrDefault()?.Split(',').FirstOrDefault()?.Trim() ?? "ru";

            // Вычисляем дату, начиная с которой нужно получать новости (сегодня минус (дни - 1))
            // Если days = 2, то today - (2-1) = today - 1, т.е. вчера и сегодня.
            // Если days = 3, то today - (3-1) = today - 2, т.е. позавчера, вчера и сегодня.
            var fromDate = DateTime.UtcNow.Date.AddDays(-(days - 1));
            
            // Создаем запрос для получения последних новостей с указанием кода языка
            var query = new GetRecentNewsQuery { FromDate = fromDate, LanguageCode = languageCode };

            // Отправляем запрос через медиатор в слой Application
            var news = await _mediator.Send(query);

            // Преобразуем доменные модели новостей в DTO для API
            var newsDtos = news.Select(NewsMapper.ToNewsDto);

            return Ok(newsDtos);
        }

        // --- CRUD Эндпоинты для админ панели (примерная реализация) ---
        // Для использования этих эндпоинтов могут потребоваться дополнительные политики авторизации
        // (например, [Authorize(Roles = "Admin")]) и соответствующие команды/обработчики в Application слое.

        /// <summary>
        /// Получает новость по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор новости.</param>
        /// <returns>DTO новости или NotFound.</returns>
        [HttpGet("{id}")]
 [Authorize(Roles = "Admin")] // Только администраторы могут получать новость по ID
        public async Task<ActionResult<NewsDto>> GetById(int id)
        {
            // Создаем запрос для получения новости по ID
            var query = new GetNewsByIdQuery { NewsId = id };
            // Отправляем запрос через медиатор
            var news = await _mediator.Send(query);

            if (news == null)
            {
                return NotFound();
            }
            return Ok(NewsMapper.ToNewsDto(news)); // Преобразуем и возвращаем DTO новости
        }

        /// <summary>
        /// Создает новую новость.
        /// </summary>
        /// <param name="command">Команда для создания новости.</param>
        /// <returns>Результат создания новости (например, CreatedAtAction).</returns>
        [HttpPost]
 [Authorize(Roles = "Admin")] // Только администраторы могут создавать новости
        public async Task<ActionResult<NewsDto>> Create([FromBody] CreateNewsCommand command)
        {
            // Отправляем команду на создание новости через медиатор
            var createdNews = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = createdNews.Id }, NewsMapper.ToNewsDto(createdNews)); // Возвращаем статус 201 Created и созданный объект
        }

        /// <summary>
        /// Обновляет существующую новость.
        /// </summary>
        /// <param name="id">Идентификатор новости для обновления.</param>
        /// <param name="command">Команда для обновления новости.</param>
        /// <returns>Результат обновления.</returns>
        [HttpPut("{id}")]
 [Authorize(Roles = "Admin")] // Только администраторы могут обновлять новости
 public async Task<ActionResult> Update(int id, [FromBody] UpdateNewsCommand command)
        {   // Устанавливаем ID новости из маршрута в команду
            command.Id = id;
            // Отправляем команду на обновление новости через медиатор
            var updatedNews = await _mediator.Send(command); // Обработчик должен вернуть null, если новость не найдена

            if (updatedNews == null) return NotFound(); // Если новость не найдена, возвращаем 404
            return NoContent(); // Возвращаем статус 204 No Content при успешном обновлении
        }

        /// <summary>
        /// Удаляет новость по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор новости для удаления.</param>
        /// <returns>Результат удаления.</returns>
        [HttpDelete("{id}")]
 [Authorize(Roles = "Admin")] // Только администраторы могут удалять новости
        public async Task<ActionResult> Delete(int id)
        {   // Создаем команду на удаление новости по ID
            var command = new DeleteNewsCommand { NewsId = id };
            await _mediator.Send(command); // Отправляем команду через медиатор
            return NoContent(); // Возвращаем статус 204 No Content при успешном удалении (предполагаем успешное удаление, обработка ошибок в обработчике)
        }
    }
}