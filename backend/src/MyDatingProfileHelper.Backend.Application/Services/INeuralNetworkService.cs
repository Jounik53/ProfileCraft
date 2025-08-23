csharp
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Services
{
    /// <summary>
    /// Интерфейс сервиса для взаимодействия с API нейросетей.
    /// Определяет контракт для вызова нейросетей для генерации описаний.
    /// </summary>
    public interface INeuralNetworkService
    {
        /// <summary>
        /// Асинхронно генерирует описание на основе заданного промта и ключа API.
        /// </summary>
        /// <param name="prompt">Подготовленный промт для нейросети.</param>
        /// <param name="apiKey">Ключ API для доступа к выбранной нейросети.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи - сгенерированный текст описания.</returns>
        Task<string> GenerateDescriptionAsync(string prompt, string apiKey);
    }
}