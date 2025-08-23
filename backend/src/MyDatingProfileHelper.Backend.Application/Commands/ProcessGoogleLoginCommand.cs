csharp
using MediatR;

namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для обработки запроса на Google авторизацию.
    /// </summary>
    public class ProcessGoogleLoginCommand : IRequest<string> // Или IRequest<User>, IRequest<AuthResult> и т.д.
    {
        /// <summary>
        /// Google токен, полученный от клиента.
        /// </summary>
        public string? GoogleToken { get; set; }

        // Дополнительные свойства, если необходимо для обработки логина
    }
}