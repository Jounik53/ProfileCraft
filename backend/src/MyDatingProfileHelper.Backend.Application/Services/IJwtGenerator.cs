csharp
using MyDatingProfileHelper.Backend.Domain.Models; // Предполагается, что модель User находится в этом пространстве имен

namespace MyDatingProfileHelper.Backend.Application.Services
{
    /// <summary>
    /// Интерфейс для сервиса генерации JSON Web Token (JWT).
    /// Определяет контракт для создания токенов аутентификации.
    /// </summary>
    public interface IJwtGenerator
    {
        /// <summary>
        /// Генерирует JWT для указанного пользователя.
        /// </summary>
        /// <param name="user">Объект пользователя, для которого генерируется токен.</param>
        /// <returns>Сгенерированный JWT в виде строки.</returns>
        string GenerateToken(User user);
    }
}