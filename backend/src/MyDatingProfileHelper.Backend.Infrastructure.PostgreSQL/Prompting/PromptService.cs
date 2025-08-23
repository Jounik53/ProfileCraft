csharp
using MyDatingProfileHelper.Backend.Application.Services;
using MyDatingProfileHelper.Backend.Domain.Models;
using Microsoft.Extensions.Configuration; // Для доступа к конфигурации, если путь к Prompts в appsettings.json
using System.IO;
using System.Threading.Tasks;
using System.Text.Json; // Для работы с JSON
using System.Collections.Generic;

namespace MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Prompting
{
    /// <summary>
    /// Сервис для подготовки промтов для нейросети на основе данных профиля пользователя.
    /// Реализует интерфейс IPromptService.
    /// </summary>
    public class PromptService : IPromptService
    {
        // Путь к папке с файлами промтов. Можно получить из конфигурации.
        private readonly string _promptsFolderPath = "Prompts"; 
        private readonly IConfiguration _configuration; // Добавим зависимость от конфигурации

        /// <summary>
        /// Конструктор с внедрением зависимостей.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public PromptService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Пример получения пути к папке с промтами из конфигурации
            _promptsFolderPath = _configuration["PromptSettings:PromptsFolderPath"] ?? "Prompts";
        }

        /// <summary>
        /// Подготавливает промт для нейросети, вставляя данные профиля в шаблон.
        /// </summary>
        /// <param name="userProfile">Профиль пользователя.</param>
        /// <returns>Подготовленный промт в виде строки.</returns>
        public async Task<string> PreparePromptAsync(UserProfile userProfile)
        {
            // Определяем имя файла промта на основе пола пользователя
            string promptFileName = userProfile.Gender?.ToLower() == "female" ? "female_prompt.json" : "male_prompt.json";
            string promptFilePath = Path.Combine(_promptsFolderPath, promptFileName);

            try
            {
                // Читаем содержимое JSON файла промта
                string jsonContent = await File.ReadAllTextAsync(promptFilePath);

                // Парсим JSON (простая реализация, можно использовать более сложные модели)
                // Предполагаем, что JSON содержит строку промта с плейсхолдерами
                var promptTemplate = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent)?["template"];

                if (string.IsNullOrEmpty(promptTemplate))
                {
                    // Обработка случая, когда шаблон промта пуст или отсутствует
                    throw new InvalidOperationException($"Шаблон промта для пола {userProfile.Gender} не найден или пуст.");
                }

                // Вставляем данные профиля в плейсхолдеры
                // Здесь нужно реализовать логику замены всех плейсхолдеров
                // Пример замены нескольких плейсхолдеров:
                string preparedPrompt = promptTemplate
                    .Replace("{{Name}}", userProfile.Name ?? "")
                    .Replace("{{Description}}", userProfile.Description ?? "")
                    .Replace("{{Interests}}", string.Join(", ", userProfile.Interests ?? new List<string>())) // Предполагаем, что Interests - это List<string> в UserProfile
                    .Replace("{{Dislikes}}", string.Join(", ", userProfile.Dislikes ?? new List<string>()))   // Предполагаем, что Dislikes - это List<string> в UserProfile
                    .Replace("{{Habits}}", userProfile.HarmfulHabits ?? "")
                    .Replace("{{Height}}", userProfile.Height?.ToString() ?? "") // Предполагаем Height int?
                    .Replace("{{HairColor}}", userProfile.HairColor ?? "")
                    .Replace("{{BodyType}}", userProfile.BodyType ?? "");


                // TODO: Добавить замену всех остальных плейсхолдеров по мере необходимости

                return preparedPrompt;
            }
            catch (FileNotFoundException)
            {
                // Обработка ошибки, если файл промта не найден
                // Логирование ошибки
                throw new InvalidOperationException($"Файл промта не найден: {promptFilePath}");
            }
            catch (JsonException)
            {
                // Обработка ошибки парсинга JSON
                // Логирование ошибки
                throw new InvalidOperationException($"Ошибка парсинга JSON файла промта: {promptFilePath}");
            }
            catch (Exception ex)
            {
                // Общая обработка других исключений
                // Логирование ошибки
                throw new InvalidOperationException($"Ошибка при подготовке промта: {ex.Message}", ex);
            }
        }
    }
}