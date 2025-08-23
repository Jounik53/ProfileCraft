package com.jounik_projects.mydatingprofilehelper.ui.cards

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.jounik_projects.mydatingprofilehelper.data.network.model.GeneratedProfileHistoryDto
import com.jounik_projects.mydatingprofilehelper.data.network.model.ProfileDescriptionDto
import com.jounik_projects.mydatingprofilehelper.data.repository.UserRepository
import kotlinx.coroutines.launch


/**
 * ViewModel for the Cards screen.
 * Manages the list of profile descriptions for cards, the current displayed card,
 * and the history of swiped cards.
 */
class CardsViewModel(private val userRepository: UserRepository) : ViewModel() {

    // LiveData для хранения списка всех доступных описаний профиля для карточек.
    // LiveData to hold the list of all profile descriptions available for cards.
    private val _profileDescriptions = MutableLiveData<List<ProfileDescriptionDto>>()
    val profileDescriptions: LiveData<List<ProfileDescriptionDto>> = _profileDescriptions

    // LiveData для хранения описания текущей отображаемой карточки.
    // LiveData to hold the description of the currently displayed card.
    private val _currentCard = MutableLiveData<ProfileDescriptionDto?>()
    val currentCard: LiveData<ProfileDescriptionDto?> = _currentCard

    // LiveData для хранения списка DTO сгенерированной истории профиля.
    // Эта история может использоваться для отображения или других целей.
    private val _generatedHistory = MutableLiveData<List<GeneratedProfileHistoryDto>>()
    val generatedHistory: LiveData<List<GeneratedProfileHistoryDto>> = _generatedHistory

    // Внутренний индекс для отслеживания текущей карточки в списке profileDescriptions.
    // LiveData to hold the list of descriptions of cards that have been swiped.
    private val _swipedCardsHistory = MutableLiveData<List<String>>(emptyList())
    val swipedCardsHistory: LiveData<List<String>> = _swipedCardsHistory

    // Internal index to track the current card in the profileDescriptions list.
    private var currentCardIndex = 0

    init {
        // Load profile descriptions when the ViewModel is created.
        loadProfileDescriptions()
    }

    /**
     * Загружает описания профиля из UserRepository.
     * Loads profile descriptions from the UserRepository.
     * This function launches a coroutine to perform the asynchronous operation.
     */
    private fun loadProfileDescriptions() {
        viewModelScope.launch {
            // Вызов метода репозитория для получения списка описаний профиля.
            val descriptions = userRepository.getProfileDescriptions() // Теперь вызываем реальный метод API
            _profileDescriptions.postValue(descriptions)
            if (descriptions.isNotEmpty()) {
                _currentCard.postValue(descriptions[currentCardIndex])
            } else {
                _currentCard.postValue(null)
            }
        }
    }

    /**
     * Обрабатывает жест смахивания для текущей карточки.
     * Может отмечать карточку как понравившуюся/непонравившуюся, сохранять в историю,
     * и переходить к следующей карточке.
     * @param isLiked True, если карточка понравилась, False иначе.
     * @param generatedText Сгенерированный текст анкеты для текущих параметров профиля.
     *                      Это может быть получено от нейросети после анализа профиля.
     */
    fun processSwipe(isLiked: Boolean, generatedText: String?) {
        val currentCardDescription = _currentCard.value ?: return // Нет текущей карточки, выход.

        // TODO: Реализовать логику обработки смахивания.
        //  - Если isLiked = true, возможно, сохранить сгенерированный текст
        //    (generatedText) в историю пользователя.
        //  - Переход к следующей карточке.

        if (isLiked && generatedText != null) {
            // Сохранить сгенерированный текст в историю.
            saveGeneratedProfileHistory(generatedText, currentCardDescription.text) // Пример: использовать текст описания как входные параметры
        }

        // Переходим к следующей карточке после обработки смахивания.
        nextCard()
    }

    /**
     * Moves to the next card description.
     * If there are no more cards, sets the current card to null.
     */
    fun nextCard() {
        val descriptions = _profileDescriptions.value ?: return

        // Добавляем текущую карточку в историю смахиваний (просто для отслеживания в ViewModel)
        val currentCardDesc = _currentCard.value
        if (currentCardDesc != null) {
             val history = _swipedCardsHistory.value.orEmpty().toMutableList()
             history.add(currentCardDesc.text) // Добавляем текст описания в историю
             _swipedCardsHistory.postValue(history)
        }

        if (currentCardIndex < descriptions.size - 1) {
            currentCardIndex++
            _currentCard.postValue(descriptions[currentCardIndex])
        } else {
            // Add the last card to history.
            if (descriptions.isNotEmpty()) {
                val lastCard = descriptions[currentCardIndex]
                val history = _swipedCardsHistory.value.orEmpty().toMutableList()
                if (!history.contains(lastCard)) { // Avoid adding the last card twice if nextCard is called multiple times after the last card
                     history.add(lastCard)
                    _swipedCardsHistory.postValue(history)
                }
                // Если это последняя карточка, сбросим индекс и карточку после добавления в историю,
                // чтобы начать сначала при следующем открытии или действии.
                currentCardIndex = 0
                _currentCard.postValue(null) // Нет больше карточек для показа
            } else {
                 _currentCard.postValue(null) // Нет карточек вообще
            }
        }
    }

    /**
     * Сохраняет сгенерированный текст профиля в историю пользователя на бэкенде.
     * @param generatedText Сгенерированный текст анкеты.
     * @param inputParameters Параметры, использованные для генерации (например, текст описания).
     */
    private fun saveGeneratedProfileHistory(generatedText: String, inputParameters: String?) {
        viewModelScope.launch {
            userRepository.saveGeneratedProfileHistory(generatedText, inputParameters)
            // Опционально: Обновить _generatedHistory LiveData после сохранения.
        }
    }

    /**
     * Предоставляет способ сброса представления карточек (например, вернуться к первой карточке).
     * Очищает историю смахиваний (если необходимо) и сбрасывает индекс.
     * Provides a way to reset the card view (e.g., go back to the first card).
     * Clears the history and resets the index.
     */
    fun resetCards() {
        currentCardIndex = 0
        // Опционально: очистить _swipedCardsHistory, если сброс означает полную перезагрузку.
        loadProfileDescriptions() // Перезагружаем описания и сбрасываем к первой карточке
    }
}