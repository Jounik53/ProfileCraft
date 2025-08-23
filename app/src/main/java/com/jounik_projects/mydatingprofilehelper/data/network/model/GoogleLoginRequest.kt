package com.jounik_projects.mydatingprofilehelper.data.network.model

import com.google.gson.annotations.SerializedName

/**
 * Модель данных для отправки Google токена на бэкенд для авторизации.
 */
data class GoogleLoginRequest(
    @SerializedName("googleToken") // Аннотация для маппинга имени поля в JSON
    val googleToken: String // Токен Google, полученный после успешного входа
)