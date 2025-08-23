csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models; // Убедитесь, что путь к модели News правильный

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос на получение новости по ее уникальному идентификатору.
    /// </summary>
    public class GetNewsByIdQuery : IRequest<News?> // IRequest<News?> указывает, что запрос может вернуть News или null
    {
        /// <summary>
        /// Уникальный идентификатор новости.
        /// </summary>
        public int NewsId { get; set; }
    }
}