package com.jounik_projects.mydatingprofilehelper.ui.settings

import android.app.Application
import android.content.Context
import android.content.SharedPreferences
import androidx.lifecycle.AndroidViewModel
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData

// ViewModel для экрана настроек
class SettingsViewModel(application: Application) : AndroidViewModel(application) {

    // Название файла SharedPreferences для хранения настроек
    private val PREFS_NAME = "app_settings"
    // Ключ для хранения выбранного языка
    private val LANGUAGE_KEY = "selected_language"
    // Ключ для хранения выбранного API нейросети
    private val NEURAL_NETWORK_API_KEY = "neural_network_api"
    // Ключ для хранения выбранной темы
    private val THEME_KEY = "app_theme"

    // Константы для тем
    companion object {
        const val THEME_LIGHT = "light"
        const val THEME_DARK = "dark"
    }

    // Получаем доступ к SharedPreferences
    private val sharedPreferences: SharedPreferences =
        application.getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE)
    // LiveData для хранения и наблюдения за выбранным языком из UI
    private val _selectedLanguage = MutableLiveData<String>()
    val selectedLanguage: LiveData<String> = _selectedLanguage
 
    // LiveData для хранения и наблюдения за выбранным API нейросети из UI
 private val _selectedNeuralNetworkApi = MutableLiveData<String>()
 val selectedNeuralNetworkApi: LiveData<String> = _selectedNeuralNetworkApi
    // LiveData для хранения и наблюдения за выбранной темой из UI
    private val _selectedTheme = MutableLiveData<String>()
    val selectedTheme: LiveData<String> = _selectedTheme


    init {
        // Загружаем сохраненный язык при инициализации ViewModel
        loadSelectedLanguage()
 loadSelectedNeuralNetworkApi()
        loadSelectedTheme()
    }

    /**
     * Загружает выбранный язык из SharedPreferences.
     * Если язык не сохранен, устанавливает язык по умолчанию (русский).
     */
    private fun loadSelectedLanguage() {
        // Получаем сохраненный язык, по умолчанию "ru" (русский)
        val language = sharedPreferences.getString(LANGUAGE_KEY, "ru") ?: "ru"
        _selectedLanguage.value = language
    }

    /**
     * Устанавливает выбранный язык и сохраняет его в SharedPreferences.
     *
     * @param languageCode Код выбранного языка (например, "ru" или "en").
     */
    fun setSelectedLanguage(languageCode: String) {
        // Сохраняем выбранный язык в SharedPreferences
        with(sharedPreferences.edit()) {
            putString(LANGUAGE_KEY, languageCode)
            apply()
        }
        // Обновляем LiveData для уведомления UI
        _selectedLanguage.value = languageCode
    }

    /**
 * Загружает выбранный API нейросети из SharedPreferences.
 * Если API не сохранен, устанавливает API по умолчанию.
 */
 private fun loadSelectedNeuralNetworkApi() {
 // Получаем сохраненный API, по умолчанию "stub1" (например, имя первой заглушки)
 val api = sharedPreferences.getString(NEURAL_NETWORK_API_KEY, "stub1") ?: "stub1"
 _selectedNeuralNetworkApi.value = api
 }

    /**
 * Устанавливает выбранный API нейросети и сохраняет его в SharedPreferences.
 *
 * @param apiKey Идентификатор выбранного API нейросети (например, "stub1", "stub2").
 */
 fun setSelectedNeuralNetworkApi(apiKey: String) {
 // Сохраняем выбранный API в SharedPreferences
 with(sharedPreferences.edit()) {
 putString(NEURAL_NETWORK_API_KEY, apiKey)
 apply()
 }
 // Обновляем LiveData для уведомления UI
 _selectedNeuralNetworkApi.value = apiKey
 }

    /**
     * Возвращает текущий выбранный язык.
     *
     * @return Код выбранного языка.
     */
    fun getSelectedLanguage(): String {
        return _selectedLanguage.value ?: "ru" // Возвращаем текущее значение или "ru" по умолчанию
    }
}
}