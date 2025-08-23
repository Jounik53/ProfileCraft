package com.jounik_projects.mydatingprofilehelper.data.network

import com.google.gson.GsonBuilder
import com.jounik_projects.mydatingprofilehelper.data.api.DatingProfileApi
import okhttp3.Interceptor
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import com.jounik_projects.mydatingprofilehelper.data.authentication.AuthTokenProvider // Предполагается, что у вас есть такой класс
import java.util.Locale
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

/**
 * Объект для настройки и предоставления Retrofit клиента и API сервисов.
 */
object RetrofitClient {

    // Замените на реальный базовый URL вашего бэкенда
    private const val BASE_URL = "YOUR_BACKEND_URL"

    // Интерцептор для логирования сетевых запросов (для отладки)
    private val loggingInterceptor = HttpLoggingInterceptor().apply {
        level = HttpLoggingInterceptor.Level.BODY
    }

    // Интерцептор для добавления токена авторизации
    private val authInterceptor = Interceptor { chain ->
        val original = chain.request()
        val requestBuilder = original.newBuilder()
            // Пример добавления токена. Замените "YOUR_AUTH_TOKEN" на реальный способ получения токена
            // Например, из SharedPreferences или другого хранилища
            // .header("Authorization", "Bearer YOUR_AUTH_TOKEN")
            AuthTokenProvider.getToken()?.let { token ->
                requestBuilder.header("Authorization", "Bearer $token")

            }
            // Добавляем заголовок Accept-Language на основе текущей локали устройства
            requestBuilder.header("Accept-Language", Locale.getDefault().language)
            }
        val request = requestBuilder.build()
        chain.proceed(request)
    }


    // Настройка OkHttpClient
    private val okHttpClient = OkHttpClient.Builder()
        .addInterceptor(authInterceptor) // Добавляем интерцептор для авторизации
        .addInterceptor(loggingInterceptor) // Добавляем интерцептор для логирования
        .build()

    // Создание экземпляра Retrofit
    private val retrofit: Retrofit by lazy {
        Retrofit.Builder()
            .baseUrl(BASE_URL)
            .client(okHttpClient) // Устанавливаем настроенный OkHttpClient
            .addConverterFactory(GsonConverterFactory.create(GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ssZ").create())) // Конвертер для JSON (с поддержкой даты)
            // TODO: Добавить адаптер для корутин, если используется (например, Coil, если не используется отдельный адаптер)
            // .addCallAdapterFactory(CoroutineCallAdapterFactory()) // Пример адаптера для корутин
            .build()
    }

    // Ленивое создание экземпляра DatingProfileApi сервиса
    val datingProfileApiService: DatingProfileApi by lazy {
        retrofit.create(DatingProfileApi::class.java)
    }
}