csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.AdminWebApp.Models;

namespace MyDatingProfileHelper.Backend.AdminWebApp.Controllers
{
    /// <summary>
    /// Контроллер для панели управления администратора.
    /// Требует авторизации для доступа.
    /// </summary>
    [Authorize] // Требует авторизации для доступа ко всем действиям в этом контроллере
    public class DashboardController : Controller
    {
        // Примечание: В реальной реализации здесь нужно будет внедрить сервис
        // или MediatR для получения статистических данных с бэкенда.
        // В данной базовой версии это просто заглушка.

        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор контроллера панели управления.
        /// </summary>
        /// <param name="mediator">Экземпляр IMediator для отправки команд и запросов.</param>
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Отображает главную страницу панели управления администратора.
        /// </summary>
        /// <returns>Представление главной страницы панели управления.</returns>
        /// <param name="startDate">Дата начала промежутка для статистики кристаллов.</param>
        /// <param name="endDate">Дата конца промежутка для статистики кристаллов.</param>
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            // Установка промежутка времени по умолчанию, если не указан (например, последний месяц)
            startDate ??= DateTime.UtcNow.AddMonths(-1);
            endDate ??= DateTime.UtcNow;

            // Получение количества зарегистрированных пользователей через MediatR
            var userCount = await _mediator.Send(new GetRegisteredUserCountQuery());


            // Получение статистики запросов к API через MediatR (пока без фильтра по датам)
            var apiRequestStats = await _mediator.Send(new GetApiRequestStatisticsQuery());

            // Создание модели представления для передачи данных в View
            var dashboardModel = new DashboardViewModel
            {
                RegisteredUsers = userCount,
                TotalCrystalsCredited = crystalStats.TotalCredited,
                // Получение статистики кристаллов через MediatR с фильтром по датам
                var crystalStats = await _mediator.Send(new GetCrystalStatisticsQuery
                {
                    StartDate = startDate.Value,
                    EndDate = endDate.Value
                });

                TotalApiRequests = apiRequestStats.TotalRequests,
                // Добавьте другие поля статистики при необходимости
            };
            return View(dashboardModel); // Отображает представление Index.cshtml, передавая модель данных
        }

        // Примечание: Для функционала графиков с промежутками времени и загрузки бэкапа БД
        // потребуется добавление соответствующих методов в контроллер, команд/запросов в Application
        // и реализаций в Infrastructure.

        // Пример действия для статистики зарегистрированных пользователей по промежуткам (требует реализации логики):
        // [HttpGet]
        // public async Task<IActionResult> RegisteredUsersChart(string interval)
        // {
        //     // Логика получения данных пользователей за указанный промежуток времени
        //     // и подготовка данных для графика через MediatR или другой сервис.
        //     // Пример: var userData = await _mediator.Send(new GetUserRegistrationStatisticsQuery { Interval = interval });
        //     return Json(new { /* данные для графика */ });
        // }

        // Пример действия для скачивания бэкапа БД (требует реализации логики):
        // [HttpGet]
        // public async Task<IActionResult> DownloadDatabaseBackup()
        // {
        //     // Логика создания бэкапа БД и возврата файла для скачивания.
        //     // Требует соответствующих прав и реализации на уровне Infrastructure.
        //     // Пример: var backupStream = await _mediator.Send(new CreateDatabaseBackupCommand());
        //     // return File(backupStream, "application/octet-stream", "database_backup.sql");
        //     return File(new byte[0], "application/octet-stream", "database_backup.sql"); // Заглушка
        // }
    }
}

        // Пример действия для скачивания бэкапа БД:
        // [HttpGet]
        // public IActionResult DownloadDatabaseBackup()
        // {
        //     // Логика создания бэкапа БД и возврата файла для скачивания.
        //     // Требует соответствующих прав и реализации на уровне Infrastructure.
        //     return File(new byte[0], "application/octet-stream", "database_backup.sql");
        // }
    }
}