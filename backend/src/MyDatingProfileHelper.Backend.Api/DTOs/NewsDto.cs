csharp
using System;

namespace MyDatingProfileHelper.Backend.Api.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) для передачи информации о новости клиенту.
    /// </summary>
    public class NewsDto
    {
        /// <summary>
        /// Уникальный идентификатор новости.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок новости.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Полное содержание новости.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Дата и время публикации новости.
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Код языка новости (например, 'ru', 'en').
        /// </summary>
        public string LanguageCode { get; set; }
    }
}