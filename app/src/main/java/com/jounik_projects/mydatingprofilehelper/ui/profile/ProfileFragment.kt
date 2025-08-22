package com.jounik_projects.mydatingprofilehelper.ui.profile

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import androidx.fragment.app.viewModels
import androidx.lifecycle.Observer
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import com.jounik_projects.mydatingprofilehelper.data.model.UserProfile
import com.jounik_projects.mydatingprofilehelper.R // Assuming you have a layout file for the profile fragment

/**
 * Represents the profile screen of the application.
 * A simple [Fragment] subclass.
 * This fragment displays the user's profile information,
 * allowing them to view and edit their data.
 * It will contain fields for name, photos, interests, dating goals, description, and crystals balance.
 */
class ProfileFragment : Fragment() {

    // Obtain a ProfileViewModel instance using the viewModels delegate.
    // This ensures the ViewModel survives configuration changes.
    private val profileViewModel: ProfileViewModel by viewModels()

    // UI elements
    private lateinit var nameEditText: EditText
    private lateinit var photosImageView: ImageView // Placeholder for displaying photos
    private lateinit var interestsEditText: EditText
    private lateinit var datingGoalsEditText: EditText
    private lateinit var descriptionEditText: EditText
    private lateinit var crystalsTextView: TextView
    private lateinit var saveButton: Button

    /**
     * Called to have the fragment instantiate its user interface view.
     * @param inflater The LayoutInflater object that can be used to inflate any views in the fragment.
     * @param container If non-null, this is the parent view that the fragment's UI should be attached to.
     * @param savedInstanceState If non-null, this fragment is being re-constructed from a previous saved state as given here.
     * @return The View for the fragment's UI, or null.
     */
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        // Inflate the layout for this fragment. R.layout.fragment_profile needs to be created.
        val view = inflater.inflate(R.layout.fragment_profile, container, false)

        // Bind UI elements
        nameEditText = view.findViewById(R.id.nameEditText)
        photosImageView = view.findViewById(R.id.photosImageView) // You'll need to handle photo loading/display later
        interestsEditText = view.findViewById(R.id.interestsEditText)
        datingGoalsEditText = view.findViewById(R.id.datingGoalsEditText)
        descriptionEditText = view.findViewById(R.id.descriptionEditText)
        crystalsTextView = view.findViewById(R.id.crystalsTextView)
        saveButton = view.findViewById(R.id.saveButton)

        // Observe UserProfile data from ViewModel
        // When the profile data changes in the ViewModel, update the UI.
        profileViewModel.userProfile.observe(viewLifecycleOwner, Observer { userProfile ->
            userProfile?.let {
                // Update UI elements with profile details
                nameEditText.setText(it.name)
                // Handle photosImageView (e.g., load images using a library)
                interestsEditText.setText(it.interests.joinToString(", ")) // Display interests as a comma-separated string
                datingGoalsEditText.setText(it.datingGoals.joinToString(", ")) // Display dating goals as a comma-separated string
                descriptionEditText.setText(it.description)
                crystalsTextView.text = "Кристаллы: ${it.crystals}" // Display crystal balance
            }
        })

        // Set up click listener for the save button
        saveButton.setOnClickListener {
            // Get data from input fields
            val name = nameEditText.text.toString()
            val interests = interestsEditText.text.toString().split(",").map { it.trim() } // Split interests by comma
            val datingGoals = datingGoalsEditText.text.toString().split(",").map { it.trim() } // Split dating goals by comma
            val description = descriptionEditText.text.toString()

            // Create a UserProfile object (userId and photos will be handled elsewhere or be part of the ViewModel's logic)
            // For now, we'll create a new UserProfile with updated data.
            // In a real app, you would likely update the existing UserProfile object observed from the ViewModel.
            val updatedProfile = UserProfile(
                userId = profileViewModel.userProfile.value?.userId ?: "", // Use existing userId or a default
                name = name,
                photos = profileViewModel.userProfile.value?.photos ?: emptyList(), // Keep existing photos for now
                interests = interests,
                datingGoals = datingGoals,
                description = description,
                crystals = profileViewModel.userProfile.value?.crystals ?: 0 // Keep existing crystal balance
            )

            // Call the saveProfile function in ViewModel
            profileViewModel.saveProfile(updatedProfile)

            // Show a confirmation message
            Toast.makeText(context, "Профиль сохранен", Toast.LENGTH_SHORT).show()
        }

        // Load the user profile when the fragment is created
        profileViewModel.loadProfile() // You'll need to implement this function in ProfileViewModel

        return view
    }
}
package com.jounik_projects.mydatingprofilehelper.ui.profile

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.jounik_projects.mydatingprofilehelper.R // Assuming you have a layout file for the profile fragment

/**
 * A simple [Fragment] subclass.
 * This fragment displays the user's profile information,
 * allowing them to view and edit their data.
 */
class ProfileFragment : Fragment() {
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
 return inflater.inflate(R.layout.fragment_profile, container, false) // Inflate the layout for this fragment. R.layout.fragment_profile needs to be created.
    }
}
package com.jounik_projects.mydatingprofilehelper.ui.profile

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.jounik_projects.mydatingprofilehelper.R // Assuming you have a layout file for the profile fragment

/**
 * A simple [Fragment] subclass.
 * This fragment displays the user's profile information,
 * allowing them to view and edit their data.
 */
class ProfileFragment : Fragment() {
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
 return inflater.inflate(R.layout.fragment_profile, container, false) // Inflate the layout for this fragment. R.layout.fragment_profile needs to be created.
    }
}
package com.jounik_projects.mydatingprofilehelper.ui.profile

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.jounik_projects.mydatingprofilehelper.R // Assuming you have a layout file for the profile fragment

/**
 * A simple [Fragment] subclass.
 * This fragment displays the user's profile information,
 * allowing them to view and edit their data.
 */
class ProfileFragment : Fragment() {
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
 return inflater.inflate(R.layout.fragment_profile, container, false) // Inflate the layout for this fragment. R.layout.fragment_profile needs to be created.
    }
}
package com.jounik_projects.mydatingprofilehelper.ui.profile

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.jounik_projects.mydatingprofilehelper.R // Assuming you have a layout file for the profile fragment

/**
 * A simple [Fragment] subclass.
 * This fragment displays the user's profile information,
 * allowing them to view and edit their data.
 */
class ProfileFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        // R.layout.fragment_profile is a placeholder, you'll need to create this layout file
        return inflater.inflate(R.layout.fragment_profile, container, false)
    }
}