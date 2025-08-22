package com.jounik_projects.mydatingprofilehelper.ui.profile

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.jounik_projects.mydatingprofilehelper.data.model.UserProfile
import com.jounik_projects.mydatingprofilehelper.data.repository.UserRepository
import kotlinx.coroutines.launch

/**
 * ViewModel for the Profile screen.
 * Manages UI-related data and interacts with the UserRepository to load and save user profile.
 */
class ProfileViewModel(private val userRepository: UserRepository) : ViewModel() {

    // LiveData to hold the user profile data.
    // MutableLiveData allows updating the data from the ViewModel.
    private val _userProfile = MutableLiveData<UserProfile?>()
    val userProfile: LiveData<UserProfile?> = _userProfile

    /**
     * Loads the user profile from the repository.
     * This function uses a coroutine to perform the operation asynchronously.
     */
    fun loadUserProfile() {
        viewModelScope.launch {
            // Call the load function from the repository.
            // The actual implementation of loading (from Drive or API) will be in UserRepository.
            val profile = userRepository.loadProfile() // Assuming a loadProfile function that handles data source
            _userProfile.postValue(profile) // Update the LiveData with the loaded profile.
        }
    }

    /**
     * Saves the user profile using the repository.
     * This function uses a coroutine to perform the operation asynchronously.
     */
    fun saveUserProfile(userProfile: UserProfile) {
        viewModelScope.launch {
            // Call the save function in the repository.
            // The actual implementation of saving (to Drive or API) will be in UserRepository.
            userRepository.saveProfile(userProfile) // Assuming a saveProfile function that handles data source
            _userProfile.postValue(userProfile) // Optionally update LiveData after successful save
        }
    }

    /**
 * Debits a specified amount of crystals from the user's balance.
 * Calls the corresponding function in UserRepository and updates the LiveData.
 * @param amount The number of crystals to debit.
 * @return true if debit was successful, false otherwise (e.g., insufficient balance).
 */
 fun debitCrystals(amount: Int): Boolean {
 val success = userRepository.debitCrystals(amount)
 if (success) {
            _userProfile.value = _userProfile.value?.copy(crystals = _userProfile.value?.crystals?.minus(amount) ?: 0)
        }
 return success
    }

    /**
 * Credits a specified amount of crystals to the user's balance.
 * Calls the corresponding function in UserRepository and updates the LiveData.
 * @param amount The number of crystals to credit.
 */
 fun creditCrystals(amount: Int) {
 userRepository.creditCrystals(amount)
        _userProfile.value = _userProfile.value?.copy(crystals = _userProfile.value?.crystals?.plus(amount) ?: 0)
    }


 // You might also add functions to update specific fields of the profile,
 // which would then call saveUserProfile.

 // Example:

    // Example:
    /*
    fun updateDescription(description: String) {
        val currentProfile = _userProfile.value
        if (currentProfile != null) {
            currentProfile.description = description
            saveUserProfile(currentProfile)
        }
    }
    */
}