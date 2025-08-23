csharp
using System.Threading.Tasks;
using MyDatingProfileHelper.Backend.Domain.Models;

namespace MyDatingProfileHelper.Backend.Application.Services
{
    /// <summary>
    /// Интерфейс сервиса для подготовки промтов для нейросети.
    /// Отвечает за выбор шаблона промта на основе данных пользователя (например, пол)
    /// и вставку данных профиля в шаблон.
    /// </summary>
    public interface IPromptService
    {
        /// <summary>
        /// Асинхронно подготавливает промт для нейросети на основе данных профиля пользователя.
        /// </summary>
        /// <param name="userProfile">Профиль пользователя, на основе которого генерируется промт.</param>
        /// <returns>Строка с подготовленным промтом.</returns>
        Task<string> PreparePromptAsync(UserProfile userProfile);
    }
}