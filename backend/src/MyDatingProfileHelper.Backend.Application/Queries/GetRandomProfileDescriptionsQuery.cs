csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models;
using System.Collections.Generic;

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос для получения случайных примеров сгенерированных описаний профилей.
    /// </summary>
    public class GetRandomProfileDescriptionsQuery : IRequest<IEnumerable<GeneratedProfileHistory>>
    {
        /// <summary>
        /// Количество случайных описаний для получения.
        /// </summary>
        public int Count { get; set; } = 5; // Значение по умолчанию - 5
    }
}