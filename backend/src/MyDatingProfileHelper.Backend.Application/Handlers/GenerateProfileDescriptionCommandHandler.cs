csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Application.Services;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using MyDatingProfileHelper.Backend.Domain.Models; // Добавлено для TransactionType
using System;
using System.Threading;
using System.Threading.Tasks;
using MyDatingProfileHelper.Backend.Application.Exceptions; // Добавлено для пользовательских исключений

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик команды для генерации описания профиля с использованием нейросети.
    /// </summary>
    public class GenerateProfileDescriptionCommandHandler : IRequestHandler<GenerateProfileDescriptionCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ITransactionRepository _transactionRepository; // Добавлено для работы с транзакциями
        private readonly IPromptService _promptService;
        private readonly INeuralNetworkService _neuralNetworkService; // Интерфейс сервиса для взаимодействия с нейросетями
        private readonly INeuralNetworkApiKeyService _neuralNetworkApiKeyService; // Добавлено для получения стоимости и ключей

        /// <summary>
        /// Конструктор обработчика команды.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        /// <param name="userProfileRepository">Репозиторий профилей пользователей.</param>
        /// <param name="transactionRepository">Репозиторий транзакций.</param>
        /// <param name="promptService">Сервис подготовки промтов.</param>
        /// <param name="neuralNetworkService">Сервис взаимодействия с нейросетями.</param>
        /// <param name="neuralNetworkApiKeyService">Сервис для работы с API ключами нейросетей.</param>
        public GenerateProfileDescriptionCommandHandler(
            IUserRepository userRepository,
            IUserProfileRepository userProfileRepository,
            ITransactionRepository transactionRepository,
            IPromptService promptService,
            INeuralNetworkService neuralNetworkService,
            INeuralNetworkApiKeyService neuralNetworkApiKeyService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository)); // userProfileRepository уже использует userRepository
            _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
            _promptService = promptService ?? throw new ArgumentNullException(nameof(promptService));
            _neuralNetworkService = neuralNetworkService ?? throw new ArgumentNullException(nameof(neuralNetworkService));
        }

        /// <summary>
        /// Обрабатывает команду генерации описания профиля.
        /// </summary>
        /// <param name="request">Команда GenerateProfileDescriptionCommand.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Сгенерированный текст описания профиля.</returns>
        /// <exception cref="Exception">Исключение, если профиль пользователя не найден.</exception>
        public async Task<string> Handle(GenerateProfileDescriptionCommand request, CancellationToken cancellationToken)
        {
            // 1. Получаем профиль пользователя по User ID из команды.
            // Получение профиля уже включает получение связанного пользователя благодаря Eager Loading или Join в репозитории.
            var userProfile = await _userProfileRepository.GetByUserIdAsync(request.UserId);

            // Проверяем не только профиль, но и связанного пользователя
            if (userProfile?.User == null)
            {
                // Обработка случая, когда профиль пользователя не найден.
                // В реальном приложении можно выбросить специфическое исключение.
                throw new Exception($"Профиль пользователя с UserId {request.UserId} не найден.");
            }

            // 2. Используем PromptService для подготовки промта на основе данных профиля.
            var prompt = await _promptService.PreparePromptAsync(userProfile);

            // 3. Вызываем сервис нейросети с подготовленным промтом.
            // Реализация INeuralNetworkService будет взаимодействовать с выбранным API нейросети.
            // Получаем API ключ и стоимость из сервиса
            // В будущем здесь можно добавить логику выбора нейросети
            var apiKeyInfo = await _neuralNetworkApiKeyService.GetApiKeyInfoForGenerationAsync(); // Предполагается, что этот сервис существует

            if (apiKeyInfo == null)
            {
                // Обработка случая, когда информация о ключе API не найдена (не настроено).
                 throw new Exception("Не настроена информация об API ключе нейросети для генерации.");
            }

            // Проверяем баланс пользователя
            if (userProfile.User.CrystalBalance < apiKeyInfo.CostPerGeneration)
            {
                 // Недостаточно кристаллов
                 throw new InsufficientFundsException("Недостаточно кристаллов для генерации анкеты."); // Пользовательское исключение
            }

            // Вызываем сервис нейросети, передавая промт и ключ API
            string generatedText = await _neuralNetworkService.GenerateDescriptionAsync(prompt, apiKeyInfo.ApiKey);

            // 4. Списываем кристаллы с баланса пользователя.
            userProfile.User.CrystalBalance -= apiKeyInfo.CostPerGeneration;
            await _userRepository.UpdateAsync(userProfile.User);

            // 5. Создаем запись о транзакции.
            var transaction = new Transaction
            {
                UserId = userProfile.UserId,
                Amount = -apiKeyInfo.CostPerGeneration, // Списание - отрицательное значение
                Type = TransactionType.NeuralNetworkGeneration, // Тип транзакции
                Timestamp = DateTime.UtcNow,
                Details = $"Списание за генерацию анкеты ({apiKeyInfo.CostPerGeneration} кристаллов)"
            };
            await _transactionRepository.AddAsync(transaction);
            
            // 4. Возвращаем сгенерированный текст.
            return generatedText;
        }
    }
}