csharp
using MediatR;

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос для получения баланса кристаллов пользователя по его идентификатору в Telegram.
    /// </summary>
    public class GetUserCrystalBalanceByTelegramIdentifierQuery : IRequest<int?>
    {
        /// <summary>
        /// Идентификатор пользователя в Telegram (UserID или Username).
        /// </summary>
        public string TelegramIdentifier { get; set; }
    }
}