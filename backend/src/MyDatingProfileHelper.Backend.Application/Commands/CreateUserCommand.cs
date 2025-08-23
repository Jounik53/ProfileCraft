csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models; // Предполагается, что модель User находится в этом пространстве имен

namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для создания нового пользователя в системе после успешной Google авторизации.
    /// Реализует интерфейс IRequest<User>, указывая, что после выполнения команды
    /// будет возвращен объект User.
    /// </summary>
    public class CreateUserCommand : IRequest<User>
    {
        /// <summary>
        /// Уникальный идентификатор пользователя, предоставленный Google.
        /// Используется для привязки пользователя в нашей системе к его Google аккаунту.
        /// </summary>
        public string GoogleId { get; set; }

        /// <summary>
        /// Адрес электронной почты пользователя, полученный из Google профиля.
        /// </summary>
        public string Email { get; set; }

        // В будущем могут быть добавлены другие свойства, необходимые для создания пользователя.
    }
}