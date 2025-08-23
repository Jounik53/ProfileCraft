csharp
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Api.Middleware
{
    /// <summary>
    /// Middleware для логирования входящих HTTP запросов.
    /// Перехватывает запросы и записывает информацию о них в лог.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        /// <summary>
        /// Конструктор middleware.
        /// </summary>
        /// <param name="next">Следующий делегат в конвейере запросов.</param>
        /// <param name="logger">Объект логгера.</param>
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Асинхронный метод обработки входящего запроса.
        /// </summary>
        /// <param name="context">Контекст HTTP запроса.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            // Логирование информации о входящем запросе перед его обработкой
            _logger.LogInformation($"[Request] {context.Request.Method} {context.Request.Path} - Начало обработки в {DateTime.UtcNow}");

            try
            {
                // Передача запроса следующему делегату в конвейере
                await _next(context);

                // Логирование информации об исходящем ответе после обработки запроса
                _logger.LogInformation($"[Request] {context.Request.Method} {context.Request.Path} - Обработка завершена со статусом {context.Response.StatusCode} в {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                // Логирование ошибок, возникших при обработке запроса
                _logger.LogError(ex, $"[Request] {context.Request.Method} {context.Request.Path} - Ошибка при обработке запроса.");
                throw; // Перебрасываем исключение для дальнейшей обработки в конвейере (например, middleware обработки исключений)
            }
        }
    }
}