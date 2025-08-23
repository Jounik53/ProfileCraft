csharp
// Пространство имен для базовых функций ASP.NET Core
using Microsoft.AspNetCore.Builder;
// Пространство имен для настройки аутентификации
using Microsoft.AspNetCore.Authentication.JwtBearer;
// Пространство имен для настройки авторизации
using Microsoft.AspNetCore.Authorization;
// Пространство имен для настройки сервисов
using Microsoft.Extensions.DependencyInjection;
// Пространство имен для настройки хостинга
using Microsoft.EntityFrameworkCore; // Пространство имен для работы с Entity Framework Core
using Microsoft.Extensions.Logging; // Пространство имен для логирования
using MyDatingProfileHelper.Backend.Data; // Пространство имен для контекста базы данных

using Microsoft.Extensions.Hosting;
// Пространства имен для настройки Swagger
using Microsoft.OpenApi.Models; // Пространство имен для моделей OpenAPI

// Пространство имен для настройки IdentityModel (для JWT)
using Microsoft.IdentityModel.Tokens;
// Пространство имен для работы с текстом и кодировками
using System.Text;

// Создаем построитель веб-приложения
var builder = WebApplication.CreateBuilder(args);

// --- Добавление сервисов в контейнер зависимостей ---

// Добавляем поддержку контроллеров API
builder.Services.AddControllers();

// Настраиваем контекст базы данных с использованием PostgreSQL
// Получаем строку подключения из конфигурации (appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddControllers();

// Добавляем сервисы для генерации документации Swagger/OpenAPI
// Swagger предоставляет интерактивную документацию для API
builder.Services.AddEndpointsApiExplorer();
// Настраиваем генерацию Swagger документации
builder.Services.AddSwaggerGen(options =>
{
    // Добавляем определение схемы безопасности для JWT Bearer
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        // Описание схемы
        Description = "Авторизация с использованием JWT Bearer токена. Введите токен в поле Value в формате 'Bearer [токен]'",
        // Название схемы (стандартное для JWT Bearer)
        Name = "Authorization",
        // Где ожидать токен (в заголовке запроса)
        In = ParameterLocation.Header,
        // Тип схемы
        Type = SecuritySchemeType.ApiKey,
        // Схема токена
        Scheme = "Bearer"
    });

    // Добавляем требование безопасности для использования схемы Bearer
    options.AddSecurityRequirement(new OpenApiSecurityRequirement());
});

// Добавляем сервисы для JWT Bearer аутентификации
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Настраиваем параметры валидации токена
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Указываем, нужно ли валидировать издателя токена
            ValidateIssuer = true,
            // Указываем, нужно ли валидировать аудиторию токена
            ValidateAudience = true,
            // Указываем, нужно ли валидировать время жизни токена
            ValidateLifetime = true,
            // Указываем, нужно ли валидировать ключ подписи токена
            ValidateIssuerSigningKey = true,
            // Устанавливаем ожидаемого издателя (получаем из конфигурации)
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Получаем издателя из конфигурации
            // Устанавливаем ожидаемую аудиторию (для кого предназначен токен)
            ValidAudience = builder.Configuration["Jwt:Audience"],
            // Устанавливаем ключ подписи, используемый для верификации
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Добавляем сервисы для авторизации (контроль доступа к ресурсам на основе аутентификации)
builder.Services.AddAuthorization();

// --- Конфигурация конвейера обработки HTTP-запросов ---

// Строим веб-приложение
var app = builder.Build();

// Настраиваем конвейер запросов HTTP.

// В режиме разработки включаем Swagger UI для интерактивной документации
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Swagger UI предоставляет веб-интерфейс для взаимодействия с API
    app.UseSwaggerUI(options =>
    {
        // Настраиваем Swagger UI для включения кнопки "Authorize"
        options.EnableDependencyValidation(); // Включаем валидацию зависимостей
    });
}

// Перенаправляет HTTP запросы на HTTPS
app.UseHttpsRedirection();

// Включает аутентификацию (кто вы?)
app.UseAuthentication();

// Включает авторизацию (что вам разрешено делать?)
app.UseAuthorization();

// Отображает запросы на действия контроллеров
app.MapControllers();

// Запускает приложение
app.Run();