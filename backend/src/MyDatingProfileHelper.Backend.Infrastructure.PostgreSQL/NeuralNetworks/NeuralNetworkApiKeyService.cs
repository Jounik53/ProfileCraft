csharp
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDatingProfileHelper.Backend.Application.Services;
using MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Data;

namespace MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.NeuralNetworks
{
    // Сервис для получения информации о ключах API нейросетей из базы данных.
    // Реализует порт INeuralNetworkApiKeyService из слоя Application.
    public class NeuralNetworkApiKeyService : INeuralNetworkApiKeyService
    {
        private readonly AppDbContext _dbContext;

        // Конструктор с внедрением зависимости AppDbContext.
        public NeuralNetworkApiKeyService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Получает стоимость использования нейросети по ее имени провайдера.
        /// </summary>
        /// <param name="providerName">Имя провайдера нейросети.</param>
        /// <returns>Стоимость использования в кристаллах или null, если провайдер не найден.</returns>
        public async Task<int?> GetUsageCostAsync(string providerName)
        {
            // Находим информацию о ключе API нейросети по имени провайдера.
            var apiKeyInfo = await _dbContext.NeuralNetworkApiKeys
                .AsNoTracking() // Не отслеживаем изменения, так как только читаем данные.
                .FirstOrDefaultAsync(k => k.ProviderName == providerName && k.IsActive); // Ищем активный ключ по имени.

            // Возвращаем стоимость использования, если ключ найден, иначе null.
            return apiKeyInfo?.UsageCostPerCrystal;
        }
    }
}