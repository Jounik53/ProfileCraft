csharp
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework; // Или используя ваш выбранный тестовый фреймворк (xUnit, MSTest)
using Shouldly;
using Substitute; // Или используя ваш выбранный mock фреймворк (NSubstitute, Moq)

using MyDatingProfileHelper.Backend.Application.Commands;
using MyDatingProfileHelper.Backend.Application.Handlers;
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Domain.Repositories;

namespace MyDatingProfileHelper.Backend.Application.Tests.Handlers
{
    // Атрибут для обозначения тестового класса (для NUnit)
    [TestFixture]
    public class CreateUserCommandHandlerTests
    {
        // Тестовый метод для проверки обработчика команды CreateUserCommand
        [Test]
        public async Task Handle_ValidCommand_CallsUserRepositoryAndReturnsUser()
        {
            // 1. Создаем mock объекта IUserRepository с использованием Substitute
            // Substitute.For<T>() создает мок-объект, который может имитировать интерфейс T
            var mockUserRepository = Substitute.For<IUserRepository>();

            // Настраиваем мок-объект: при вызове метода AddAsync с любым объектом User,
            // мок должен вернуть Task с тем же объектом User.
            mockUserRepository.AddAsync(Arg.Any<User>()).ReturnsForAnyArgs(x => Task.FromResult(x.Arg<User>()));

            // 2. Создаем экземпляр тестируемого обработчика команды, внедряя mock репозиторий
            var handler = new CreateUserCommandHandler(mockUserRepository);

            // 3. Создаем экземпляр команды CreateUserCommand с тестовыми данными
            var command = new CreateUserCommand
            {
                GoogleId = "test_google_id",
                Email = "test@example.com"
            };

            // Создаем токен отмены (обычно используется в обработчиках MediatR)
            var cancellationToken = new CancellationToken();

            // 4. Вызываем метод Handle тестируемого обработчика
            var result = await handler.Handle(command, cancellationToken);

            // 5. Используем утверждения Shouldly для проверки результатов
            // Проверяем, что результат не равен null
            result.ShouldNotBeNull();
            // Проверяем, что GoogleId возвращенного пользователя соответствует ожидаемому
            result.GoogleId.ShouldBe(command.GoogleId);
            // Проверяем, что Email возвращенного пользователя соответствует ожидаемому
            result.Email.ShouldBe(command.Email);
            // Проверяем, что CrystalBalance возвращенного пользователя равен 0 по умолчанию
            result.CrystalBalance.ShouldBe(0);

            // Проверяем, что метод AddAsync на mock репозитории был вызван ровно один раз
            await mockUserRepository.Received(1).AddAsync(Arg.Any<User>());
            // Проверяем, что метод AddAsync на mock репозитории был вызван с объектом User,
            // у которого GoogleId и Email соответствуют данным из команды
            await mockUserRepository.Received(1).AddAsync(Arg.Is<User>(u =>
                u.GoogleId == command.GoogleId &&
                u.Email == command.Email));
        }

        // Добавьте другие тестовые методы для различных сценариев, например,
        // проверка обработки дублирующихся пользователей (если такая логика будет добавлена в обработчик).
    }
}