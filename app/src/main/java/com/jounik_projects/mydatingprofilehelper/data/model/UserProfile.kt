package com.jounik_projects.mydatingprofilehelper.data.model

/**
 * Data class representing a user's dating profile.
 */
data class UserProfile(
    /**
     * Unique identifier for the user, typically obtained from Google Sign-In.
     */
    var userId: String = "",
    /**
     * The user's name.
     */
    var name: String = "",
    /**
     * A list of URLs or paths to the user's photos.
     */
    var photos: List<String> = emptyList(),
    /**
     * A list of the user's interests.
     */
    var interests: List<String> = emptyList(),
    /**
     * A list of the user's goals for using dating apps.
     */
    var datingGoals: List<String> = emptyList(),
    /**
     * The user's free-form profile description.
     */
    var description: String = "",
    /**
     * The user's local currency balance (crystals).
     */
    var crystals: Int = 0
)