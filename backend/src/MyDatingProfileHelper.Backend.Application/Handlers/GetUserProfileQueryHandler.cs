csharp
using MediatR;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Handlers
{
    /// <summary>
    /// Обработчик запроса на получение профиля пользователя.
    /// Использует репозиторий профилей пользователей для получения данных.
    /// </summary>
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfile>
    {
        private readonly IUserProfileRepository _userProfileRepository;

        /// <summary>
        /// Конструктор обработчика запроса.
        /// </summary>
        /// <param name="userProfileRepository">Репозиторий профилей пользователей.</param>
        public GetUserProfileQueryHandler(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение профиля пользователя.
        /// </summary>
        /// <param name="request">Запрос на получение профиля пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Профиль пользователя или null, если не найден.</returns>
        public async Task<UserProfile> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            // Вызов репозитория для получения профиля пользователя по его ID.
            // В реальной реализации здесь может быть дополнительная логика бизнес-правил,
            // например, проверка прав доступа.
            var userProfile = await _userProfileRepository.GetUserProfileByUserIdAsync(request.UserId);

            return userProfile;
        }
    }
}