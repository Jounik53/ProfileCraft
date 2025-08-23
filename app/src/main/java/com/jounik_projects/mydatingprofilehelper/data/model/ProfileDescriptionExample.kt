package com.jounik_projects.mydatingprofilehelper.data.model

/**
 * Класс данных, представляющий пример сгенерированного описания профиля.
 * Используется для отображения случайных описаний на главной странице.
 */
data class ProfileDescriptionExample(
    /**
     * Уникальный идентификатор примера описания.
     */
    val id: Int,
    /**
     * Сгенерированный текст описания профиля.
     */
    val generatedText: String,
    /**
     * Временная метка генерации описания.
     */
    val timestamp: String // Используем String для временной метки
)