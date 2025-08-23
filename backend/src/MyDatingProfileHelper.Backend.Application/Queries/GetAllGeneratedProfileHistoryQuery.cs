csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models;
using System.Collections.Generic;

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос для получения всей истории сгенерированных анкет пользователя.
    /// </summary>
    public class GetAllGeneratedProfileHistoryQuery : IRequest<List<GeneratedProfileHistory>>
    {
        /// <summary>
        /// Идентификатор профиля пользователя.
        /// </summary>
        public int UserProfileId { get; set; }
    }
}