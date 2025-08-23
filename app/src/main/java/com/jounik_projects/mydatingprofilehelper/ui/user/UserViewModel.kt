package com.jounik_projects.mydatingprofilehelper.ui.user

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.jounik_projects.mydatingprofilehelper.data.repository.UserRepository
import kotlinx.coroutines.launch
import android.util.Log // Импортируем для логирования

/**
 * ViewModel для управления данными пользователя, такими как баланс кристаллов.
 * Загружает данные пользователя из репозитория при создании ViewModel.
 */
class UserViewModel(private val userRepository: UserRepository) : ViewModel() {

    // LiveData для хранения текущего баланса кристаллов пользователя.
    // Используем MutableLiveData для возможности изменения значения внутри ViewModel.
    private val _crystalBalance = MutableLiveData<Int>()
    // Предоставляем LiveData для наблюдения из UI, ограничивая возможность изменения извне.
    val crystalBalance: LiveData<Int> = _crystalBalance

    init {
        // Блок инициализации, который выполняется при первом создании ViewModel.
        // Запускаем корутину для выполнения асинхронной загрузки данных.
        viewModelScope.launch {
            try {
                // Загружаем баланс кристаллов пользователя из репозитория.
                val balance = userRepository.getCrystalBalance()
                // Обновляем значение LiveData с полученным балансом.
                _crystalBalance.postValue(balance)
            } catch (e: Exception) {
                // Обработка ошибок при загрузке баланса.
                // Например, логирование ошибки.
                Log.e("UserViewModel", "Ошибка при загрузке баланса кристаллов: ${e.message}", e)
                // Возможно, стоит обновить LiveData с индикатором ошибки или установить значение по умолчанию.
                _crystalBalance.postValue(0) // Устанавливаем 0 или другое значение по умолчанию при ошибке
            }
        }
    }

    // TODO: Добавьте другие методы для управления данными пользователя, если необходимо
    // (например, обновление профиля, история транзакций и т.д.)
}