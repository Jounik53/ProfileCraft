package com.jounik_projects.mydatingprofilehelper.utils

import android.content.Context
import android.content.res.Configuration
import android.os.Build
import android.preference.PreferenceManager
import java.util.Locale

/**
 * Помощник для управления локалью (языком) приложения.
 */
object LocaleHelper {

    private const val SELECTED_LANGUAGE = "Locale.Helper.Selected.Language"

    /**
     * Устанавливает локаль приложения на основе выбранного кода языка.
     * Сохраняет выбранный язык в SharedPreferences.
     *
     * @param context Контекст приложения или активности.
     * @param languageCode Код выбранного языка (например, "ru", "en").
     * @return Контекст с обновленной конфигурацией.
     */
    fun setLocale(context: Context, languageCode: String): Context {
        persist(context, languageCode)
        return updateResources(context, languageCode)
    }

    /**
     * Получает сохраненный код языка из SharedPreferences.
     *
     * @param context Контекст приложения или активности.
     * @return Код сохраненного языка, по умолчанию "ru".
     */
    fun getLanguage(context: Context): String {
        return PreferenceManager.getDefaultSharedPreferences(context).getString(SELECTED_LANGUAGE, "ru") ?: "ru"
    }

    /**
     * Сохраняет выбранный код языка в SharedPreferences.
     */
    private fun persist(context: Context, languageCode: String) {
        PreferenceManager.getDefaultSharedPreferences(context).edit().putString(SELECTED_LANGUAGE, languageCode).apply()
    }

    /**
     * Обновляет ресурсы контекста с новой локалью.
     */
    private fun updateResources(context: Context, languageCode: String): Context {
        val locale = Locale(languageCode)
        Locale.setDefault(locale)

        val configuration = context.resources.configuration

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
            configuration.setLocale(locale)
            // Для Android N и выше
            val displayMetrics = context.resources.displayMetrics
            return context.createConfigurationContext(configuration)
        } else {
            // Для версий ниже Android N
            configuration.locale = locale
            context.resources.updateConfiguration(configuration, context.resources.displayMetrics)
            return context
        }
    }

    /**
     * Привязывает контекст активности к правильной локали.
     * Рекомендуется вызывать в attachBaseContext активности.
     */
    fun onAttach(context: Context): Context {
        val lang = getLanguage(context)
        return setLocale(context, lang)
    }
}