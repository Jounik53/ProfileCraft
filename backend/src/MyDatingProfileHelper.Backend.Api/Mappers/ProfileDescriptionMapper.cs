csharp
using MyDatingProfileHelper.Backend.Domain.Models;
using MyDatingProfileHelper.Backend.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace MyDatingProfileHelper.Backend.Api.Mappers
{
    /// <summary>
    /// Статический класс для преобразования моделей домена ProfileDescription в DTO API ProfileDescriptionDto.
    /// </summary>
    public static class ProfileDescriptionMapper
    {
        /// <summary>
        /// Преобразует объект ProfileDescription домена в объект ProfileDescriptionDto API.
        /// </summary>
        /// <param name="profileDescription">Объект ProfileDescription домена.</param>
        /// <returns>Объект ProfileDescriptionDto API.</returns>
        public static ProfileDescriptionDto ToProfileDescriptionDto(ProfileDescription profileDescription)
        {
            return new ProfileDescriptionDto
            {
                Id = profileDescription.Id,
                Text = profileDescription.Text,
                Category = profileDescription.Category
            };
        }

        /// <summary>
        /// Преобразует список объектов ProfileDescription домена в список объектов ProfileDescriptionDto API.
        /// </summary>
        /// <param name="profileDescriptions">Список объектов ProfileDescription домена.</param>
        /// <returns>Список объектов ProfileDescriptionDto API.</returns>
        public static List<ProfileDescriptionDto> ToProfileDescriptionDtoList(IEnumerable<ProfileDescription> profileDescriptions)
        {
            return profileDescriptions.Select(pd => ToProfileDescriptionDto(pd)).ToList();
        }
    }
}