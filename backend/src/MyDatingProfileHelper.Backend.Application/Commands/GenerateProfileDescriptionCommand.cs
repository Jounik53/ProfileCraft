csharp
using MediatR;

namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для запроса генерации описания профиля пользователя с помощью нейросети.
    /// </summary>
    public class GenerateProfileDescriptionCommand : IRequest<string>
    {
        /// <summary>
        /// Идентификатор пользователя, для которого нужно сгенерировать описание.
        /// </summary>
        public int UserId { get; set; }
    }
}