package com.jounik_projects.mydatingprofilehelper.ui.home

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.jounik_projects.mydatingprofilehelper.data.model.News // Импорт модели News
import com.jounik_projects.mydatingprofilehelper.data.model.ProfileDescriptionExample
import com.jounik_projects.mydatingprofilehelper.data.network.model.ProfileDescriptionDto
import com.jounik_projects.mydatingprofilehelper.data.network.model.GeneratedDescriptionResponse // Import GeneratedDescriptionResponse
import com.jounik_projects.mydatingprofilehelper.data.repository.DatingProfileRepository // Changed import
import kotlinx.coroutines.launch

/**
 * ViewModel for the Home screen.
 * Manages and provides data related to the home feed, such as sample profile descriptions.
 */
class HomeViewModel(private val userRepository: UserRepository) : ViewModel() {
// Changed the injected dependency to DatingProfileRepository
// class HomeViewModel(private val datingProfileRepository: DatingProfileRepository) : ViewModel() {

    // LiveData to hold the list of profile descriptions to be displayed on the home screen.
 private val _profileDescriptions = MutableLiveData<List<ProfileDescriptionDto>>()
 val profileDescriptions: LiveData<List<ProfileDescriptionDto>> = _profileDescriptions

    // LiveData для хранения списка новостей для отображения на главном экране.
 private val _recentNews = MutableLiveData<List<News>>()
 val recentNews: LiveData<List<News>> = _recentNews

    // LiveData для хранения списка примеров описаний профилей для отображения на главном экране.
 private val _exampleDescriptions = MutableLiveData<List<ProfileDescriptionExample>>()
 val exampleDescriptions: LiveData<List<ProfileDescriptionExample>> = _exampleDescriptions

    /**
     * Загружает данные для главного экрана: новости и примеры описаний профилей.
     * This function uses a coroutine to perform the data loading asynchronously.
     */
 fun loadHomeData() {
        viewModelScope.launch {
            try {
                // Загружаем новости (нужно добавить метод в DatingProfileRepository)
                val news = userRepository.getRecentNews() // Предполагается, что этот метод будет добавлен в UserRepository
                _recentNews.postValue(news)

                // Загружаем примеры описаний (нужно добавить метод в DatingProfileRepository)
                val examples = userRepository.getRandomProfileDescriptions() // Предполагается, что этот метод будет добавлен в UserRepository
                _exampleDescriptions.postValue(examples)
            } catch (e: Exception) {
                // Обработка ошибок при загрузке данных
                // Например, логирование или обновление LiveData с ошибкой
            }
        }
    }

    /**
     * Добавляет сгенерированное описание профиля к текущему описанию пользователя.
     *
     * @param description Текст описания, которое нужно добавить.
     */
    fun addProfileDescriptionToYours(description: String) {
        viewModelScope.launch {
            try {
                userRepository.addDescriptionToUserProfile(description) // Вызываем метод репозитория для добавления и сохранения описания
            } catch (e: Exception) {
                // Обработка ошибок при сохранении профиля
            }
        }
    }

    /**
     * Запускает процесс генерации описания профиля по заданным параметрам.
     *
     * @param params Параметры для генерации (в формате строки, требуется адаптация).
     */
    fun generateProfileDescriptionWithParams(params: String) {
        viewModelScope.launch {
            try {
                // Вызываем метод репозитория для генерации описания с параметрами
                val generatedDescription: GeneratedDescriptionResponse = userRepository.generateProfileDescription(params)
                // TODO: Обработать результат генерации (например, отобразить пользователю)

            } catch (e: Exception) {
                // Обработка ошибок при генерации
            }
        }
    }
}