package com.jounik_projects.mydatingprofilehelper.data.network.model

/**
 * Модель данных для запроса сохранения истории сгенерированного профиля на бэкенде.
 */
data class SaveGeneratedProfileHistoryRequest(
    val generatedText: String, // Сгенерированный текст анкеты
    val inputParameters: String? // Входные параметры, использованные для генерации (могут быть в формате JSON)
)
package com.jounik_projects.mydatingprofilehelper.data.network.model

/**
 * Модель данных для запроса сохранения истории сгенерированного профиля на бэкенде.
 */
data class SaveGeneratedProfileHistoryRequest(
    val generatedText: String, // Сгенерированный текст анкеты
    val inputParameters: String? // Входные параметры, использованные для генерации (могут быть в формате JSON)
)