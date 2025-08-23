package com.jounik_projects.mydatingprofilehelper.data

import android.content.Context
import android.content.SharedPreferences

/**
 * Объект-провайдер токена авторизации.
 * Использует SharedPreferences для локального хранения токена.
 * Является синглтоном.
 */
object AuthTokenProvider {

    // Имя файла SharedPreferences для хранения токена
    private const val PREF_FILE_NAME = "auth_prefs"
    // Ключ для сохранения/получения токена
    private const val KEY_AUTH_TOKEN = "auth_token"

    // Переменная для хранения экземпляра SharedPreferences
    private var sharedPreferences: SharedPreferences? = null

    /**
     * Инициализирует провайдер токена.
     * Должен быть вызван при запуске приложения (например, в Application классе).
     * @param context Контекст приложения.
     */
    fun initialize(context: Context) {
        if (sharedPreferences == null) {
            sharedPreferences = context.getSharedPreferences(PREF_FILE_NAME, Context.MODE_PRIVATE)
        }
    }

    /**
     * Сохраняет токен авторизации в SharedPreferences.
     * @param token Токен для сохранения.
     */
    fun saveToken(token: String) {
        sharedPreferences?.edit()?.putString(KEY_AUTH_TOKEN, token)?.apply()
    }

    /**
     * Получает токен авторизации из SharedPreferences.
     * @return Сохраненный токен или null, если токен отсутствует.
     */
    fun getToken(): String? {
        return sharedPreferences?.getString(KEY_AUTH_TOKEN, null)
    }

    /**
     * Удаляет токен авторизации из SharedPreferences.
     */
    fun clearToken() {
        sharedPreferences?.edit()?.remove(KEY_AUTH_TOKEN)?.apply()
    }
}