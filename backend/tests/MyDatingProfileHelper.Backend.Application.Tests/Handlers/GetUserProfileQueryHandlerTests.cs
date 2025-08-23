csharp
using MyDatingProfileHelper.Backend.Application.Handlers;
using MyDatingProfileHelper.Backend.Application.Queries;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;
using NUnit.Framework;
using Shouldly;
using Substitute;
using System.Threading;
using System.Threading.Tasks;

namespace MyDatingProfileHelper.Backend.Application.Tests.Handlers
{
    /// <summary>
    /// Тесты для обработчика запроса GetUserProfileQueryHandler.
    /// </summary>
    [TestFixture] // Атрибут для тестового класса в NUnit
    public class GetUserProfileQueryHandlerTests
    {
        /// <summary>
        /// Тестовый метод для проверки обработки запроса GetUserProfileQuery.
        /// </summary>
        [Test] // Атрибут для тестового метода в NUnit
        public async Task Handle_ValidQuery_ReturnsUserProfile()
        {
            // 1. Создаем мок-объект для IUserProfileRepository с использованием Substitute.
            var userProfileRepositoryMock = Substitute.For<IUserProfileRepository>();

            // Создаем ожидаемый результат, который вернет мок-репозиторий.
            var expectedUserProfile = new UserProfile { Id = 1, UserId = 1, Name = "Тестовый Пользователь" };

            // Настраиваем мок-репозиторий: при вызове GetByUserIdAsync с любым int и CancellationToken,
            // он должен вернуть ожидаемый UserProfile.
            userProfileRepositoryMock.GetByUserIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                                     .Returns(Task.FromResult(expectedUserProfile));

            // 2. Создаем экземпляр GetUserProfileQueryHandler, внедряя мок-репозиторий.
            var handler = new GetUserProfileQueryHandler(userProfileRepositoryMock);

            // 3. Создаем экземпляр запроса GetUserProfileQuery.
            var query = new GetUserProfileQuery { UserId = 1 };

            // 4. Вызываем метод Handle обработчика запроса.
            var result = await handler.Handle(query, CancellationToken.None);

            // 5. Используем утверждения Shouldly для проверки результата.
            // Проверяем, что результат не равен null.
            result.ShouldNotBeNull();
            // Проверяем, что возвращенный профиль совпадает с ожидаемым.
            result.ShouldBe(expectedUserProfile);
            // Проверяем, что метод GetByUserIdAsync был вызван на мок-репозитории ровно один раз
            // с правильным аргументом UserId.
            await userProfileRepositoryMock.Received(1).GetByUserIdAsync(query.UserId, Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Тестовый метод для проверки случая, когда профиль пользователя не найден.
        /// </summary>
        [Test]
        public async Task Handle_UserProfileNotFound_ReturnsNull()
        {
            // 1. Создаем мок-объект для IUserProfileRepository.
            var userProfileRepositoryMock = Substitute.For<IUserProfileRepository>();

            // Настраиваем мок-репозиторий: при вызове GetByUserIdAsync, он должен вернуть null.
            userProfileRepositoryMock.GetByUserIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                                     .Returns(Task.FromResult<UserProfile>(null)); // Указываем тип явно

            // 2. Создаем экземпляр GetUserProfileQueryHandler, внедряя мок-репозиторий.
            var handler = new GetUserProfileQueryHandler(userProfileRepositoryMock);

            // 3. Создаем экземпляр запроса GetUserProfileQuery.
            var query = new GetUserProfileQuery { UserId = 999 }; // Пользователь, которого нет

            // 4. Вызываем метод Handle обработчика запроса.
            var result = await handler.Handle(query, CancellationToken.None);

            // 5. Используем утверждения Shouldly для проверки результата.
            // Проверяем, что результат равен null.
            result.ShouldBeNull();
            // Проверяем, что метод GetByUserIdAsync был вызван на мок-репозитории ровно один раз
            // с правильным аргументом UserId.
            await userProfileRepositoryMock.Received(1).GetByUserIdAsync(query.UserId, Arg.Any<CancellationToken>());
        }
    }
}