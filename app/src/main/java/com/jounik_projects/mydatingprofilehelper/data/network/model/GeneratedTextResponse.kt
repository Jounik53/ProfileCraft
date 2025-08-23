package com.jounik_projects.mydatingprofilehelper.data.network.model

/**
 * Модель данных для ответа от API бэкенда при генерации описания анкеты.
 * Содержит сгенерированный текст.
 */
data class GeneratedTextResponse(
    // Сгенерированный текст описания анкеты
    val generatedText: String
)