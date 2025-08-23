package com.jounik_projects.mydatingprofilehelper.data.network.model

/**
 * Модель данных профиля пользователя для обмена с бэкендом.
 * Соответствует DTO UserProfileDto на бэкенде.
 */
data class UserProfileDto(
    val name: String?, // Имя пользователя
    val description: String?, // Описание о себе
    val photos: List<String>?, // Список URL фотографий
    val interests: List<String>?, // Интересы ("Что нравится")
    val dislikes: List<String>?, // Антипатии ("Что не нравится")
    val badHabits: String?, // Отношение к вредным привычкам
    val height: Int?, // Рост
    val hairColor: String?, // Цвет волос
    val bodyType: String?, // Телосложение
    val gender: String? // Пол пользователя
    // Добавьте другие внешние параметры и свойства по необходимости
)
