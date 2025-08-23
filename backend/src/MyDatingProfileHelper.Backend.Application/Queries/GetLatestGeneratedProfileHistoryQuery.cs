csharp
namespace MyDatingProfileHelper.Backend.Application.Queries
{
    // Команда для получения последней сгенерированной анкеты для профиля пользователя.
    // Реализует IRequest<GeneratedProfileHistory?> для запроса данных.
    // Возвращает объект GeneratedProfileHistory или null, если история отсутствует.
    public class GetLatestGeneratedProfileHistoryQuery : IRequest<GeneratedProfileHistory?>
    {
        // Идентификатор профиля пользователя, для которого запрашивается история.
        public int UserProfileId { get; set; }
    }
}