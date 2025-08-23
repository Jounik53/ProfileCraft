package com.jounik_projects.mydatingprofilehelper.data.model

/**
 * Data class, представляющий новость.
 */
data class News(
    /**
     * Уникальный идентификатор новости.
     */
    val id: Int,
    /**
     * Заголовок новости.
     */
    val title: String,
    /**
     * Содержание новости.
     */
    val content: String,
    /**
     * Дата публикации новости в формате строки.
     */
    val publishDate: String // Для простоты используем String, можно использовать Date или LocalDateTime
)
