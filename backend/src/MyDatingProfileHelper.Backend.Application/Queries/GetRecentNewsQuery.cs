csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models;
using System;
using System.Collections.Generic;

namespace MyDatingProfileHelper.Backend.Application.Queries
{
    /// <summary>
    /// Запрос на получение недавних новостей (начиная с определенной даты).
    /// </summary>
    public class GetRecentNewsQuery : IRequest<IEnumerable<News>>
    {
        /// <summary>
        /// Дата, начиная с которой получать новости.
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Код языка для фильтрации новостей.
        /// </summary>
        public string LanguageCode { get; set; }
    }
}