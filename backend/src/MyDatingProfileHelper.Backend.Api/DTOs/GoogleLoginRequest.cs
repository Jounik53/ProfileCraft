csharp
// DTO для запроса Google авторизации
namespace MyDatingProfileHelper.Backend.DTOs
{
    /// <summary>
    /// Представляет данные запроса для входа через Google.
    /// </summary>
    public class GoogleLoginRequest
    {
        /// <summary>
        /// Google токен, полученный от клиента Android.
        /// </summary>
        public string? GoogleToken { get; set; }
    }
}