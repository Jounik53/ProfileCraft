csharp
// Пространство имен для DTO, используемых в API слое
namespace MyDatingProfileHelper.Backend.Api.DTOs
{
    /// <summary>
    /// DTO для запроса пополнения баланса кристаллов через Telegram бота.
    /// Содержит идентификатор пользователя в Telegram и сумму пополнения.
    /// </summary>
    public class CreditCrystalsTelegramRequest
    {
        /// <summary>
        /// Идентификатор пользователя в Telegram (Username или User ID).
        /// </summary>
        public string TelegramIdentifier { get; set; }

        /// <summary>
        /// Сумма кристаллов для пополнения.
        /// </summary>
        public int Amount { get; set; }
    }
}