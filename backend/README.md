# MyDatingProfileHelper - Backend

## Бэкенд для приложения "Помощник в создании анкеты"

Этот проект представляет собой бэкенд часть приложения MyDatingProfileHelper, реализованную на C# с использованием ASP.NET Core. Он предназначен для управления данными пользователей, профилями, примерами описаний, транзакциями (кристаллами), а также для предоставления API для взаимодействия с нейросетями и интеграции с платежными системами. В качестве базы данных используется PostgreSQL.

## Структура Проекта

Бэкенд проект будет организован по слоям для лучшей структуризации и поддержки:



*   **MyDatingProfileHelper.Backend:** Корневая папка проекта.
*   **MyDatingProfileHelper.Backend/Models:** Содержит классы, представляющие модели данных, которые будут храниться в базе данных.
*   **MyDatingProfileHelper.Backend/Data:** Содержит контекст базы данных (DbContext) и конфигурации Entity Framework Core.
*   **MyDatingProfileHelper.Backend/Migrations:** Содержит файлы миграций Entity Framework Core для управления схемой базы данных.
*   **MyDatingProfileHelper.Backend/Services:** Содержит классы с бизнес-логикой и взаимодействием с внешними сервисами (например, API нейросетей, платежные системы).
*   **MyDatingProfileHelper.Backend/Controllers:** Содержит контроллеры ASP.NET Core, которые обрабатывают входящие HTTP-запросы и возвращают ответы.
*   **MyDatingProfileHelper.Backend/DTOs:** (Data Transfer Objects) Содержит классы для передачи данных между клиентом и сервером.

## Модели Данных (PostgreSQL)

База данных PostgreSQL будет содержать следующие ключевые таблицы:

*   **Users:**
    *   `Id` (Primary Key)
    *   `GoogleId` (Уникальный идентификатор пользователя из Google Auth)
    *   `Email`
    *   `RegistrationDate`
    *   `LastLoginDate`
    *   `CrystalBalance` (Баланс кристаллов пользователя)
*   **UserProfiles:**
    *   `Id` (Primary Key)
    *   `UserId` (Foreign Key to Users)
    *   `Name`
    *   `Photos` (Например, JSON массив ссылок на файлы)
    *   `Interests` (Например, JSON массив строк)
    *   `DatingGoals` (Например, JSON массив строк)
    *   `Description`
*   **ProfileDescriptions:**
    *   `Id` (Primary Key)
    *   `Text`
    *   `Category` (Например, юмористическое, серьезное и т.д.)
    *   `CreatedDate`
    *   `IsApproved` (Для модерации)
*   **Transactions:**
    *   `Id` (Primary Key)
    *   `UserId` (Foreign Key to Users)
    *   `Amount` (Количество кристаллов)
    *   `Type` (Например, "пополнение", "списание за нейросеть")
    *   `Timestamp`
    *   `Details` (Например, ID нейросети, информация об оплате)
*   **NeuralNetworkApiKeys:**
    *   `Id` (Primary Key)
    *   `ProviderName` (Например, "OpenAI", "Google AI")
    *   `ApiKey` (Ключ API)
    *   `UsageCostPerCrystal` (Стоимость использования в кристаллах)
    *   `IsActive`

## Миграции

Для управления схемой базы данных PostgreSQL будет использоваться Entity Framework Core Migrations.

*   Миграции позволят вносить изменения в структуру базы данных по мере развития проекта без необходимости ручного изменения SQL-скриптов.
*   Команды для создания и применения миграций будут доступны через .NET CLI.

## Документация API (Swagger)

Для документирования RESTful API бэкенда будет использоваться Swagger (OpenAPI).

*   Swagger UI будет доступен по определенному endpoint'у (например, `/swagger`).
*   Swagger UI позволит просматривать доступные endpoint'ы, их параметры, ожидаемые ответы и выполнять тестовые запросы.
*   Будет настроена поддержка авторизации в Swagger UI для тестирования защищенных endpoint'ов.

## Аутентификация и Авторизация

*   **Аутентификация:** Пользователи будут аутентифицироваться через Google Sign-In в мобильном приложении. Бэкенд будет верифицировать Google Token, полученный от клиента.
*   **Авторизация:** После успешной аутентификации, бэкенд будет выдавать JSON Web Token (JWT) клиенту. Этот JWT будет использоваться клиентом для доступа к защищенным endpoint'ам API.
*   **Endpoint для авторизации:** Будет создан отдельный endpoint (например, `/api/auth/google-login`), который будет принимать Google Token от клиента, верифицировать его и возвращать JWT.

## Начало Работы

1.  **Установка .NET SDK:** Убедитесь, что у вас установлен .NET SDK (рекомендуется последняя версия, совместимая с ASP.NET Core).
    *   [Скачать .NET SDK](https://dotnet.microsoft.com/download)
2.  **Установка PostgreSQL:** Установите сервер базы данных PostgreSQL.
    *   [Скачать PostgreSQL](https://www.postgresql.org/download/)
3.  **Клонирование репозитория бэкенда:** Клонируйте репозиторий бэкенда.
4.  **Настройка подключения к базе данных:** В файлах конфигурации проекта (например, `appsettings.json`) укажите строку подключения к вашей базе данных PostgreSQL.
5.  **Применение миграций:** Откройте терминал в корневой папке проекта бэкенда и выполните команды Entity Framework Core CLI для создания базы данных и применения миграций:
    
```
bash
    dotnet ef database update
    
```
(Перед этим может потребоваться установить инструмент `dotnet ef`: `dotnet tool install --global dotnet-ef`)
6.  **Настройка Google API Credentials:** Настройте учетные данные Google API для верификации токенов на бэкенде.
7.  **Запуск бэкенда:** Выполните следующую команду в корневой папке проекта бэкенда:
```
bash
    dotnet run
    
```
Бэкенд запустится, и вы сможете получить доступ к Swagger UI по адресу, указанному в консоли (обычно `https://localhost:5001/swagger` или `http://localhost:5000/swagger`).