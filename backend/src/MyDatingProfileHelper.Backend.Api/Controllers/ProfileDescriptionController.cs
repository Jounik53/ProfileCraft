csharp
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDatingProfileHelper.Backend.Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDatingProfileHelper.Backend.Api.DTOs; // Потребуется создать ProfileDescriptionDto
using MyDatingProfileHelper.Backend.Api.Mappers; // Потребуется создать или обновить маппер

namespace MyDatingProfileHelper.Backend.Api.Controllers
{
    /// <summary>
    /// Контроллер для управления примерами описаний профилей.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileDescriptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="mediator">Медиатор для отправки запросов и команд.</param>
        public ProfileDescriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получает список примеров описаний профилей.
        /// </summary>
        /// <returns>Список примеров описаний профилей.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDescriptionDto>>> GetProfileDescriptions()
        {
            // Создаем запрос для получения списка описаний из слоя Application
            var query = new GetProfileDescriptionsQuery();

            // Отправляем запрос через MediatR и ожидаем результат
            var descriptions = await _mediator.Send(query);

            // Преобразуем модели домена/приложения в DTO для уровня API
            // Потребуется создать маппер для ProfileDescription -> ProfileDescriptionDto
            var descriptionDtos = ProfileDescriptionMapper.ToProfileDescriptionDtoList(descriptions);

            // Возвращаем DTO список в ответе на HTTP запрос
            return Ok(descriptionDtos);
        }
    }
}