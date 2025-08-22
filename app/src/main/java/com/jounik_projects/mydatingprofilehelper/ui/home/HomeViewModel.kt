package com.jounik_projects.mydatingprofilehelper.ui.home

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.jounik_projects.mydatingprofilehelper.data.repository.UserRepository
import kotlinx.coroutines.launch

/**
 * ViewModel for the Home screen.
 * Manages and provides data related to the home feed, such as sample profile descriptions.
 */
class HomeViewModel(private val userRepository: UserRepository) : ViewModel() {

    // LiveData to hold the list of profile descriptions to be displayed on the home screen.
    private val _profileDescriptions = MutableLiveData<List<String>>()
    val profileDescriptions: LiveData<List<String>> = _profileDescriptions

    /**
     * Loads profile descriptions from the repository.
     * This function uses a coroutine to perform the data loading asynchronously.
     */
    fun loadProfileDescriptions() {
        viewModelScope.launch {
            // Call the UserRepository to get profile descriptions.
            // In a real app, this would interact with DatingProfileApiService.
            val descriptions = userRepository.getSampleProfileDescriptions() // Assuming UserRepository has this function
            _profileDescriptions.postValue(descriptions)
        }
    }
}