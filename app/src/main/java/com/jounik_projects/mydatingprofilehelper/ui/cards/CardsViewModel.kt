package com.jounik_projects.mydatingprofilehelper.ui.cards

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.jounik_projects.mydatingprofilehelper.data.repository.UserRepository
import kotlinx.coroutines.launch

/**
 * ViewModel for the Cards screen.
 * Manages the list of profile descriptions for cards, the current displayed card,
 * and the history of swiped cards.
 */
class CardsViewModel(private val userRepository: UserRepository) : ViewModel() {

    // LiveData to hold the list of all profile descriptions available for cards.
    private val _profileDescriptions = MutableLiveData<List<String>>()
    val profileDescriptions: LiveData<List<String>> = _profileDescriptions

    // LiveData to hold the description of the currently displayed card.
    private val _currentCardDescription = MutableLiveData<String?>()
    val currentCardDescription: LiveData<String?> = _currentCardDescription

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
     * Loads profile descriptions from the UserRepository.
     * This function launches a coroutine to perform the asynchronous operation.
     */
    private fun loadProfileDescriptions() {
        viewModelScope.launch {
            // In a real app, this would call userRepository.getProfileDescriptions()
            // which in turn calls DatingProfileApiService.getProfileDescriptions().
            // For now, we use the dummy data from the stub.
            val descriptions = userRepository.getProfileDescriptionsFromStub() // Placeholder for getting descriptions
            _profileDescriptions.postValue(descriptions)
            if (descriptions.isNotEmpty()) {
                _currentCardDescription.postValue(descriptions[currentCardIndex])
            } else {
                _currentCardDescription.postValue(null)
            }
        }
    }

    /**
     * Moves to the next card description.
     * If there are no more cards, sets the current card to null.
     */
    fun nextCard() {
        val descriptions = _profileDescriptions.value ?: return
        if (currentCardIndex < descriptions.size - 1) {
            // Add the current card to history before moving to the next.
            val currentCard = descriptions[currentCardIndex]
            val history = _swipedCardsHistory.value.orEmpty().toMutableList()
            history.add(currentCard)
            _swipedCardsHistory.postValue(history)

            currentCardIndex++
            _currentCardDescription.postValue(descriptions[currentCardIndex])
        } else {
            // Add the last card to history.
            if (descriptions.isNotEmpty()) {
                val lastCard = descriptions[currentCardIndex]
                val history = _swipedCardsHistory.value.orEmpty().toMutableList()
                if (!history.contains(lastCard)) { // Avoid adding the last card twice if nextCard is called multiple times after the last card
                     history.add(lastCard)
                    _swipedCardsHistory.postValue(history)
                }
            }
            _currentCardDescription.postValue(null) // No more cards
        }
    }

    /**
     * Provides a way to reset the card view (e.g., go back to the first card).
     * Clears the history and resets the index.
     */
    fun resetCards() {
        currentCardIndex = 0
        _swipedCardsHistory.postValue(emptyList())
        val descriptions = _profileDescriptions.value
        if (!descriptions.isNullOrEmpty()) {
            _currentCardDescription.postValue(descriptions[currentCardIndex])
        } else {
            _currentCardDescription.postValue(null)
        }
    }
}