csharp
using System;

namespace MyDatingProfileHelper.Backend.Domain.Models
{
    /// <summary>
    /// Представляет новость или информационное сообщение для пользователей.
    /// </summary>
    public class News
    {
        /// <summary>
        /// Уникальный идентификатор новости (первичный ключ).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок новости.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Содержимое новости.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Дата и время публикации новости.
        /// </summary>
        public DateTime PublishDate { get; set; }
\n+        /// <summary>\n+        /// Код языка новости (например, 'ru', 'en').\n+        /// </summary>\n+        public string LanguageCode { get; set; }\n    }\n}\n
    }
}