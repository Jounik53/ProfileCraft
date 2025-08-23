package com.jounik_projects.mydatingprofilehelper.data.network.api

import com.jounik_projects.mydatingprofilehelper.data.network.model.* // Предполагается, что здесь будут модели запросов и ответов
import retrofit2.Response
import retrofit2.http.*

/**
 * Интерфейс API для взаимодействия с данными пользователя и историей на бэкенде.
 * Использует аннотации Retrofit для определения HTTP запросов.
 */
interface UserProfileApi {

    /**
     * Получение профиля текущего пользователя.
     * @return Ответ Retrofit, содержащий UserProfileDto.
     */
    @GET("api/UserProfile") // Пример пути к endpoint'у бэкенда
    suspend fun getUserProfile(): Response<UserProfileDto> // UserProfileDto нужно создать

    /**
     * Сохранение или обновление профиля пользователя.
     * @param request Тело запроса с данными профиля (SaveUserProfileRequest).
     * @return Ответ Retrofit, содержащий UserProfileDto сохраненного/обновленного профиля.
     */
    @POST("api/UserProfile") // Пример пути к endpoint'у бэкенда
    suspend fun saveUserProfile(@Body request: SaveUserProfileRequest): Response<UserProfileDto> // SaveUserProfileRequest нужно создать

    /**
     * Сохранение записи в историю сгенерированных профилей.
     * @param request Тело запроса с данными истории (SaveGeneratedProfileHistoryRequest).
     * @return Ответ Retrofit, содержащий GeneratedProfileHistoryDto сохраненной записи истории.
     */
    @POST("api/GeneratedProfileHistory") // Пример пути к endpoint'у бэкенда
    suspend fun saveGeneratedProfileHistory(@Body request: SaveGeneratedProfileHistoryRequest): Response<GeneratedProfileHistoryDto> // SaveGeneratedProfileHistoryRequest и GeneratedProfileHistoryDto нужно создать

    /**
     * Получение последней записи из истории сгенерированных профилей текущего пользователя.
     * @return Ответ Retrofit, содержащий GeneratedProfileHistoryDto последней записи истории (или null, если истории нет).
     */
    @GET("api/GeneratedProfileHistory/latest") // Пример пути к endpoint'у бэкенда
    suspend fun getLatestGeneratedHistory(): Response<GeneratedProfileHistoryDto?>

    /**
     * Получение всех записей из истории сгенерированных профилей текущего пользователя.
     * @return Ответ Retrofit, содержащий список GeneratedProfileHistoryDto.
     */
    @GET("api/GeneratedProfileHistory/all") // Пример пути к endpoint'у бэкенда
    suspend fun getAllGeneratedHistory(): Response<List<GeneratedProfileHistoryDto>>

    // Дополнительно: endpoint для получения баланса кристаллов
    @GET("api/User/balance") // Пример пути к endpoint'у бэкенда
    suspend fun getUserCrystalBalance(): Response<Int>

    /**
     * Вызов бэкенд API для генерации описания профиля с помощью нейросети.
     * @return Ответ Retrofit, содержащий сгенерированный текст (GeneratedTextResponse).
     */
    @POST("api/NeuralNetwork/generate") // Пример пути к endpoint'у бэкенда для генерации
    suspend fun generateProfileDescription(): Response<GeneratedTextResponse> // GeneratedTextResponse нужно создать
}