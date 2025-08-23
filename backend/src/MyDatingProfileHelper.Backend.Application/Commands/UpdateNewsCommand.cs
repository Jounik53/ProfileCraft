csharp
namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для обновления существующей новости.
    /// </summary>
    public class UpdateNewsCommand : IRequest<News> // Указываем, что команда возвращает объект News (обновленную новость)
    {
        /// <summary>
        /// Уникальный идентификатор новости, которую необходимо обновить.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Новый заголовок новости.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Новое содержимое новости.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Новый код языка новости (например, 'ru', 'en').
        /// </summary>
        public string LanguageCode { get; set; }
    }
}