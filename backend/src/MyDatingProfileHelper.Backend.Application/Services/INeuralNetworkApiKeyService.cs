csharp
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с ключами API нейросетей и их стоимостью.
    /// </summary>
    public interface INeuralNetworkApiKeyService
    {
        /// <summary>
        /// Получает стоимость использования (в кристаллах) для указанного провайдера нейросети.
        /// </summary>
        /// <param name="providerName">Название провайдера нейросети.</param>
        /// <returns>Стоимость использования в кристаллах или null, если провайдер не найден.</returns>
        Task<int?> GetUsageCostAsync(string providerName);

        // Возможно, здесь также понадобятся методы для получения самого API ключа для использования
        // Task<string?> GetApiKeyAsync(string providerName);
    }
}