csharp
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер.
builder.Services.AddControllersWithViews(); // Добавляем поддержку контроллеров и представлений MVC

// Настройка аутентификации Cookie Authentication для администратора
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Путь к странице входа
        options.LogoutPath = "/Account/Logout"; // Путь для выхода
        options.AccessDeniedPath = "/Account/AccessDenied"; // Путь для отказа в доступе
    });

// Настройка авторизации
builder.Services.AddAuthorization(options =>
{
    // Пример политики для администратора
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireAuthenticatedUser(); // Требует аутентифицированного пользователя
        // Здесь можно добавить дополнительные требования, например, проверку роли
        // policy.RequireRole("Administrator");
    });
});

// Добавление MediatR для обработки команд и запросов из слоя Application
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyDatingProfileHelper.Backend.Application.Commands.CreateUserCommand).Assembly));


builder.Services.AddControllersWithViews(options =>
{
    // Добавление глобального фильтра авторизации, требующего аутентификации по умолчанию
    // Это гарантирует, что все контроллеры или действия, не имеющие атрибута [AllowAnonymous],
    // будут требовать аутентификации. Для административной панели это часто полезно.
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));

});


var app = builder.Build();

// Настройка конвейера обработки HTTP-запросов.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Обработка ошибок в продакшене
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // Перенаправление на HTTPS
app.UseStaticFiles(); // Обслуживание статических файлов (CSS, JS, изображения)

app.UseRouting(); // Включение маршрутизации

app.UseAuthentication(); // Использование аутентификации
app.UseAuthorization(); // Использование авторизации

// Настройка маршрута по умолчанию
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
