csharp
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Services
{
    /// <summary>
    /// Интерфейс для сервиса верификации Google Token.
    /// </summary>
    public interface IGoogleTokenVerifier
    {
        /// <summary>
        /// Верифицирует предоставленный Google Token.
        /// </summary>
        /// <param name="googleToken">Google Token для верификации.</param>
        /// <returns>Объект GoogleUserPayload с информацией о пользователе, если токен действителен, иначе null.</returns>
        Task<GoogleUserPayload?> VerifyTokenAsync(string googleToken);
    }

    /// <summary>
    /// Класс для хранения верифицированной информации о пользователе Google.
    /// </summary>
    public class GoogleUserPayload
    {
        /// <summary>
        /// Уникальный идентификатор пользователя Google.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Адрес электронной почты пользователя Google.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        // Могут быть добавлены другие поля, например, Name, PictureUrl.
    }
}