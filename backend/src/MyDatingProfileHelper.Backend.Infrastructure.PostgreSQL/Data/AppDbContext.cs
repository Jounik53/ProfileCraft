csharp
using Microsoft.EntityFrameworkCore;
using MyDatingProfileHelper.Backend.Models;

namespace MyDatingProfileHelper.Backend.Data
namespace MyDatingProfileHelper.Backend.Infrastructure.PostgreSQL.Data
    /// <summary>
    /// Контекст базы данных приложения, использующий Entity Framework Core.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Конструктор с опциями DbContext.
        /// </summary>
        /// <param name="options">Опции конфигурации DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Коллекция новостей.
        /// </summary>
        public DbSet<News> News { get; set; }

        /// <summary>
        /// Коллекция пользователей.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Коллекция профилей пользователей.
        /// </summary>
        public DbSet<UserProfile> UserProfiles { get; set; }

        /// <summary>
        /// Коллекция примеров описаний профилей.
        /// </summary>
        public DbSet<ProfileDescription> ProfileDescriptions { get; set; }

        /// <summary>
        /// Коллекция транзакций кристаллов.
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Коллекция ключей API нейросетей.
        /// </summary>
        public DbSet<NeuralNetworkApiKey> NeuralNetworkApiKeys { get; set; }

        // Метод для конфигурации базы данных. В реальном приложении строка подключения
 /// <summary>
 /// Коллекция истории генерации анкет.
 /// </summary>
 public DbSet<GeneratedProfileHistory> GeneratedProfileHistory { get; set; }
 // Метод для конфигурации базы данных. В реальном приложении строка подключения
        // обычно берется из конфигурации (например, appsettings.json).
        // В этом примере используется заглушка.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Пример использования PostgreSQL. Реальная строка подключения
            // должна быть настроена в файле appsettings.json и получена через IConfiguration.
            // string connectionString = Configuration.GetConnectionString("DefaultConnection");
            // optionsBuilder.UseNpgsql(connectionString);

            // Заглушка: в реальном приложении здесь будет вызов UseNpgsql с реальной строкой подключения.
             if (!optionsBuilder.IsConfigured)
             {
                 optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_password"); // Замените на реальные данные подключения
             }
        }

        /// <summary>
        /// Метод для дополнительной конфигурации моделей.
        /// </summary>
        /// <param name="modelBuilder">Builder для конфигурации моделей.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Дополнительные конфигурации моделей, если необходимо.
            // Например, определение первичных ключей, индексов, связей.

            // Пример настройки связи один-к-одному или один-ко-многим:
            // modelBuilder.Entity<User>()
            //     .HasOne(u => u.UserProfile)
            //     .WithOne(up => up.User)
            //     .HasForeignKey<UserProfile>(up => up.UserId);

            // Убедитесь, что GoogleId уникален
            modelBuilder.Entity<User>()
                .HasIndex(u => u.GoogleId)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}