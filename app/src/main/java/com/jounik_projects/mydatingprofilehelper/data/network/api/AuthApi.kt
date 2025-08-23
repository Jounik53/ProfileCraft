package com.jounik_projects.mydatingprofilehelper.data.network.api

import com.jounik_projects.mydatingprofilehelper.data.network.model.AuthResponse
import com.jounik_projects.mydatingprofilehelper.data.network.model.GoogleLoginRequest
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.POST

/**
 * Интерфейс API для взаимодействия с эндпоинтами аутентификации на бэкенде.
 */
interface AuthApi {

    /**
     * Отправляет запрос на Google авторизацию на бэкенд.
     * @param request Объект запроса, содержащий Google Token.
     * @return Ответ от бэкенда, содержащий JWT.
     */
    @POST("/api/auth/google-login") // Указываем путь к эндпоинту авторизации на бэкенде
    suspend fun googleLogin(@Body request: GoogleLoginRequest): Response<AuthResponse>
}
