csharp
namespace MyDatingProfileHelper.Backend.Api.DTOs
{
    /// <summary>
    /// DTO для ответа при успешной авторизации.
    /// </summary>
    public class AuthSuccessResponse
    {
        /// <summary>
        /// JSON Web Token (JWT).
        /// </summary>
        public string Token { get; set; }
    }
}