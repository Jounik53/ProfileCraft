package com.jounik_projects.mydatingprofilehelper.ui.history

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.jounik_projects.mydatingprofilehelper.data.network.model.TransactionDto
import com.jounik_projects.mydatingprofilehelper.data.repository.UserRepository // Assuming UserRepository handles history fetching
import kotlinx.coroutines.launch

/**
 * ViewModel для экрана истории транзакций.
 * Отвечает за загрузку и предоставление списка истории для UI.
 */
class HistoryViewModel(private val userRepository: UserRepository) : ViewModel() {

    // LiveData для хранения списка транзакций
 private val _transactionHistory = MutableLiveData<List<TransactionDto>>()
 val transactionHistory: LiveData<List<TransactionDto>> get() = _transactionHistory
    // LiveData для отслеживания ошибок
    private val _errorMessage = MutableLiveData<String>()
    val errorMessage: LiveData<String> get() = _errorMessage


    /**
     * Загружает список истории генерации анкет.
 * Загружает историю транзакций.
     */
    fun loadTransactionHistory() {
        viewModelScope.launch {
            try {
                // Предполагается, что UserRepository имеет функцию для получения истории транзакций
 val history = userRepository.getTransactionHistory()
 _transactionHistory.value = history
            } catch (e: Exception) {
                // Обработка ошибок при загрузке
                _errorMessage.value = "Ошибка при загрузке истории: ${e.message}"
                // Можно также установить historyList в пустой список или null в случае ошибки
                _historyList.value = emptyList()
            } finally {
                _isLoading.value = false
            }
        }
    }
}