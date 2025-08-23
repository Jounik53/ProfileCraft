package com.jounik_projects.mydatingprofilehelper.data.network.model

/**
 * Модель данных для истории сгенерированной анкеты, получаемая с бэкенда.
 */

data class GeneratedProfileHistoryDto(
    val id: Int, // Идентификатор записи истории
 val userProfileId: Int, // Идентификатор профиля пользователя
    val generatedText: String, // Текст сгенерированной анкеты
    val timestamp: String // Время генерации (в формате строки, как приходит с бэкенда)
    // Дополнительные поля, если есть в DTO бэкенда
)