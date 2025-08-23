csharp
using MediatR;

namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для пополнения баланса кристаллов пользователя по идентификатору Telegram.
    /// </summary>
    public class CreditCrystalsByTelegramCommand : IRequest<bool>
    {
        /// <summary>
        /// Идентификатор пользователя Telegram (например, Username или User ID).
        /// </summary>
        public string TelegramIdentifier { get; set; }

        /// <summary>
        /// Количество кристаллов для пополнения.
        /// </summary>
        public int Amount { get; set; }
    }
}