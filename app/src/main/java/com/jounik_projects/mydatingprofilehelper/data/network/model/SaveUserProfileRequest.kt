package com.jounik_projects.mydatingprofilehelper.data.network.model

/**
 * Класс данных для запроса сохранения профиля пользователя на бэкенде.
 * Соответствует SaveUserProfileRequest DTO на бэкенде.
 */
data class SaveUserProfileRequest(
    // TODO: Добавить свойства, соответствующие SaveUserProfileRequest на бэкенде
    // Например:
    // val name: String,
    // val photos: List<String>,
    // val interests: List<String>,
    // val datingGoals: List<String>,
    // val description: String,
    // val likes: List<String>, // Что нравится
    // val dislikes: List<String>, // Что не нравится
    // val badHabitsAttitude: String, // Отношение к вредным привычкам
// val height: Int?, // Рост
// val hairColor: String?, // Цвет волос
// val bodyType: String? // Телосложение
 val gender: String // Пол пользователя
)