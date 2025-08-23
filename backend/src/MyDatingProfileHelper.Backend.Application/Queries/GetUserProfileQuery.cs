csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models; // Предполагается, что модель UserProfile находится здесь

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос для получения профиля пользователя по его идентификатору.
    /// </summary>
    public class GetUserProfileQuery : IRequest<UserProfile>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Конструктор запроса.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public GetUserProfileQuery(int userId)
        {
            UserId = userId;
        }
    }
}