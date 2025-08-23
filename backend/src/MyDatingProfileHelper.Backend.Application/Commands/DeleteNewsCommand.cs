csharp
using MediatR;

namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для удаления новости.
    /// </summary>
    public class DeleteNewsCommand : IRequest<Unit>
    {
        /// <summary>
        /// Идентификатор новости для удаления.
        /// </summary>
        public int NewsId { get; set; }
    }
}