csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models;
using System.Collections.Generic;

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос для получения всех новостей.
    /// Используется в основном для административных целей или полного списка.
    /// </summary>
    public class GetAllNewsQuery : IRequest<IEnumerable<News>>
    {
        // Этот запрос не требует дополнительных параметров для получения всех новостей.
    }
}