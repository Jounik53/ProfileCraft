csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models;

namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для создания новой новости.
    /// </summary>
    public class CreateNewsCommand : IRequest<News>
    {
        /// <summary>
        /// Заголовок новости.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Содержимое новости.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Код языка новости (например, 'ru', 'en').
        /// </summary>
        public string LanguageCode { get; set; }
    }
}