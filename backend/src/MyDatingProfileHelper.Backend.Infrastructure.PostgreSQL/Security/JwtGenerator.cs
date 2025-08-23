csharp
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyDatingProfileHelper.Backend.Application.Services;
using MyDatingProfileHelper.Backend.Domain.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Security
{
    /// <summary>
    /// Реализация генератора JWT токенов.
    /// </summary>
    public class JwtGenerator : IJwtGenerator
    {
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;
        private readonly string _audience;

        /// <summary>
        /// Конструктор генератора JWT.
        /// </summary>
        /// <param name="config">Конфигурация приложения для получения JWT настроек.</param>
        public JwtGenerator(IConfiguration config)
        {
            // Получаем секретный ключ для подписи токена из конфигурации
            var secretKey = config["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException(nameof(secretKey), "Секретный ключ JWT не настроен в конфигурации.");
            }
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Получаем издателя и аудиторию токена из конфигурации
            _issuer = config["Jwt:Issuer"];
            _audience = config["Jwt:Audience"];
        }

        /// <summary>
        /// Генерирует JWT токен для указанного пользователя.
        /// </summary>
        /// <param name="user">Пользователь, для которого генерируется токен.</param>
        /// <returns>Строка с сгенерированным JWT токеном.</returns>
        public string GenerateToken(User user)
        {
            // Создаем список утверждений (claims), которые будут включены в токен
            var claims = new List<Claim>
            {
                // Добавляем уникальный идентификатор пользователя из БД в утверждения
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                // Можно добавить другие утверждения, например, email или роли, если они есть
                new Claim(ClaimTypes.Email, user.Email)
                // new Claim(ClaimTypes.Role, "User") // Пример добавления роли
            };

            // Создаем учетные данные для подписи токена
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Описываем токен: кто его издал, для кого предназначен, какие утверждения содержит,
            // срок действия и учетные данные для подписи.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Утверждения
                Expires = DateTime.Now.AddDays(7), // Срок действия токена (например, 7 дней)
                SigningCredentials = creds, // Учетные данные для подписи
                Issuer = _issuer, // Издатель токена
                Audience = _audience // Аудитория токена
            };

            // Создаем обработчик токенов
            var tokenHandler = new JwtSecurityTokenHandler();

            // Создаем токен на основе описания
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Сериализуем токен в строку
            return tokenHandler.WriteToken(token);
        }
    }
}