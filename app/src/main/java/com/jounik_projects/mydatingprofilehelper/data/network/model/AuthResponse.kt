package com.jounik_projects.mydatingprofilehelper.data.network.model

import com.google.gson.annotations.SerializedName

/**
 * Модель данных для ответа авторизации от бэкенда.
 * Содержит JWT (JSON Web Token).
 */
data class AuthResponse(
    @SerializedName("jwt") // Указываем имя поля в JSON ответе
    val jwt: String
)