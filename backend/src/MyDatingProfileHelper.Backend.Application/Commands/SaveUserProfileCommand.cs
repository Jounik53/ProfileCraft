csharp
using MediatR;
using MyDatingProfileHelper.Backend.Domain.Models; // Предполагается, что UserProfile находится в слое Domain
using System.Collections.Generic;

namespace MyDatingProfileHelper.Backend.Application.Commands
{
    /// <summary>
    /// Команда для сохранения или обновления профиля пользователя.
    /// </summary>
    public class SaveUserProfileCommand : IRequest<UserProfile>
    {
        /// <summary>
        /// Идентификатор пользователя, к которому относится профиль.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список ссылок на фотографии пользователя.
        /// </summary>
        public List<string> Photos { get; set; } = new List<string>();

        /// <summary>
        /// Список интересов пользователя.
        /// </summary>
        public List<string> Interests { get; set; } = new List<string>();

        /// <summary>
        /// Список целей знакомства пользователя.
        /// </summary>
        public List<string> DatingGoals { get; set; } = new List<string>();

        /// <summary>
        /// Текстовое описание профиля.
        /// </summary>
        public string Description { get; set; }
    }
}