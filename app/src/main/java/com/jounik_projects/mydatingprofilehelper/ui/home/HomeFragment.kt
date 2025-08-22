package com.jounik_projects.mydatingprofilehelper.ui.home

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView // Import RecyclerView
import com.jounik_projects.mydatingprofilehelper.ui.adapters.ProfileDescriptionAdapter // Placeholder adapter


/**
 * A simple [Fragment] subclass for the Home screen.
 * This fragment will display news and examples of other user descriptions.
 */
class HomeFragment : Fragment() {

    /**
     * ViewModel for the Home screen, shared with the activity if needed.
     */
    private val homeViewModel: HomeViewModel by activityViewModels()

    /**
     * RecyclerView for displaying the list of profile descriptions.
     */
    private lateinit var recyclerView: RecyclerView

    /**
     * Adapter for the RecyclerView. Placeholder for now.
     */
    private lateinit var profileDescriptionAdapter: ProfileDescriptionAdapter

    /**
     * Called to have the fragment instantiate its user interface view.
     * This is where you should inflate your layout and initialize your UI.
     *
     * @param inflater The LayoutInflater object that can be used to inflate any views in the fragment,
     * @param container If non-null, this is the parent view that the fragment's UI should be attached to.
     *                  The fragment should not add the view itself, but this can be used to generate
     *                  the LayoutParams of the view.
     * @param savedInstanceState If non-null, this fragment is being re-constructed from a previous
     *                           saved state as given here.
     * @return The View for the fragment's UI, or null.
     */
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? { // The root view of the fragment.
        // Inflate the layout for this fragment (assuming you have a layout file named fragment_home.xml)
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_home, container, false) // Assuming you have a layout file named fragment_home.xml
    }

    /**
     * Called immediately after onCreateView() has returned, but before any saved state has been restored in to the view.
     * This is where you should do any final customization of the fragment's view.
     *
     * @param view The View returned by onCreateView(LayoutInflater, ViewGroup, Bundle).
     * @param savedInstanceState If non-null, this fragment is being re-constructed from a previous
     *                           saved state as given here.
     */
    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        // Bind the RecyclerView from the layout using its ID (assuming it's defined in fragment_home.xml)
        recyclerView = view.findViewById(R.id.recyclerViewProfileDescriptions) // Assuming your RecyclerView has the ID recyclerViewProfileDescriptions

        // Set up the RecyclerView with a LayoutManager and adapter
        // Use a LinearLayoutManager for a standard vertical list
        recyclerView.layoutManager = LinearLayoutManager(context)

        // Initialize the adapter for displaying profile descriptions.
        // Start with an empty list and it will be updated when data is available from the ViewModel.
        profileDescriptionAdapter = ProfileDescriptionAdapter(emptyList())
        // Set the adapter for the RecyclerView.
        recyclerView.adapter = profileDescriptionAdapter

        // Observe the list of profile descriptions from the HomeViewModel.
        // Observe the list of profile descriptions from the ViewModel
        // When the data changes, update the UI (e.g., a RecyclerView or TextView)
        homeViewModel.profileDescriptions.observe(viewLifecycleOwner, Observer { descriptions ->
            // Update the adapter with the new list of descriptions
            adapter.updateData(descriptions)
        })
        // Call the ViewModel function to load profile descriptions
        // This triggers fetching the data, which will eventually update the LiveData and trigger the observer.
        homeViewModel.loadProfileDescriptions()
    }
}