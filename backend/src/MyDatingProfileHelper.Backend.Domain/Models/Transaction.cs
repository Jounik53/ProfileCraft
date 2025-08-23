using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Представляет транзакцию, связанную с балансом кристаллов пользователя.
namespace MyDatingProfileHelper.Backend.Domain.Models
{
 public class Transaction
    {
 /// <summary>
 /// Уникальный идентификатор транзакции.
 /// </summary>
 [Key]
 public int Id { get; set; }

 /// <summary>
 /// Внешний ключ, связывающий транзакцию с пользователем.
 /// </summary>
 [ForeignKey("User")]
 public int UserId { get; set; }

 /// <summary>
 /// Количество кристаллов, участвующих в транзакции (положительное для пополнения, отрицательное для списания).
 /// Используем int для количества кристаллов.
 /// </summary>
 public int Amount { get; set; }

 /// <summary>
 /// Тип транзакции (например, "пополнение", "списание_нейросеть", "списание_премиум").
 /// </summary>
 public string Type { get; set; }

 /// <summary>
 /// Временная метка транзакции.
 /// </summary>
 public DateTime Timestamp { get; set; }

 /// <summary>
 /// Дополнительные детали транзакции (например, ID использованной нейросети, информация об оплате).
 /// </summary>
 public string Details { get; set; }

 /// <summary>
 /// Навигационное свойство к пользователю, связанному с этой транзакцией.
 /// </summary>
 public User User { get; set; }
    }
}
csharp
// Представляет транзакцию, связанную с балансом кристаллов пользователя.
namespace MyDatingProfileHelper.Backend.Domain.Models
{
    public class Transaction
    {
        /// <summary>
        /// Уникальный идентификатор транзакции.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Внешний ключ, связывающий транзакцию с пользователем.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Количество кристаллов, участвующих в транзакции (положительное для пополнения, отрицательное для списания).
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Тип транзакции (например, "пополнение", "списание_нейросеть", "списание_премиум").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Временная метка транзакции.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Дополнительные детали транзакции (например, ID использованной нейросети, информация об оплате).
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Навигационное свойство к пользователю, связанному с этой транзакцией.
        /// </summary>
        public User User { get; set; }
    }
}