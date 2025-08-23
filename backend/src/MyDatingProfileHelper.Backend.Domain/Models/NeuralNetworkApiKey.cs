csharp
namespace MyDatingProfileHelper.Backend.Domain.Models
{
    /// <summary>
    /// Представляет ключ API для нейросети.
    /// </summary>
    public class NeuralNetworkApiKey
    {
        /// <summary>
        /// Уникальный идентификатор ключа API.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название провайдера нейросети (например, "OpenAI", "Google AI").
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Сам ключ API. В реальном приложении должен быть зашифрован или храниться безопасно.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Стоимость использования нейросети в кристаллах за одну операцию (или другую единицу измерения).
        /// </summary>
        public int UsageCostPerCrystal { get; set; }

        /// <summary>
        /// Флаг, указывающий, активен ли данный ключ API.
        /// </summary>
        public bool IsActive { get; set; }
    }
}