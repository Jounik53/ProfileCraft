csharp
using MediatR;

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос для получения баланса кристаллов пользователя.
    /// </summary>
    public class GetUserCrystalBalanceQuery : IRequest<int>
    {
        /// <summary>
        /// Идентификатор пользователя, баланс которого нужно получить.
        /// </summary>
        public int UserId { get; set; }
    }
}