csharp
using System.ComponentModel.DataAnnotations;

namespace MyDatingProfileHelper.Backend.AdminWebApp.Models
{
    /// <summary>
    /// Модель для данных формы входа администратора.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required(ErrorMessage = "Поле 'Имя пользователя' обязательно для заполнения.")]
        [Display(Name = "Имя пользователя")]
        public string? Username { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }
}