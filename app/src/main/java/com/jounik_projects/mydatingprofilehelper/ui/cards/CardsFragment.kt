package com.jounik_projects.mydatingprofilehelper.ui.cards

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.viewModels
import com.jounik_projects.mydatingprofilehelper.R
import com.jounik_projects.mydatingprofilehelper.data.model.UserProfile // Assuming UserProfile might be needed for history
import com.jounik_projects.mydatingprofilehelper.databinding.FragmentCardsBinding // Import the generated binding class

import androidx.lifecycle.Observer // Import Observer

/**
 * A simple [Fragment] subclass representing the Cards screen.
 * This screen will display profile descriptions as swipeable cards.
 */
class CardsFragment : Fragment() {
    // TODO: Implement card swiping logic and display profile descriptions

    // View binding instance for accessing UI elements
    private var _binding: FragmentCardsBinding? = null
    private val binding get() = _binding!!

    // Obtain a reference to the CardsViewModel
    private val cardsViewModel: CardsViewModel by viewModels()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        // Inflate the layout for this fragment
        _binding = FragmentCardsBinding.inflate(inflater, container, false)
        val root: View = binding.root

        // Bind the TextView for displaying the description
        val descriptionTextView: TextView = binding.descriptionTextView // Assuming the ID is descriptionTextView in fragment_cards.xml

        // Initial call to load the first card
        cardsViewModel.loadCards()

        // Observe the current card data from the ViewModel
        cardsViewModel.currentCard.observe(viewLifecycleOwner, Observer { cardDescription ->
            // TODO: Update UI to display the current cardDescription
        })

        // Observe the history of swiped cards from the ViewModel
        cardsViewModel.swipedCardsHistory.observe(viewLifecycleOwner, Observer { history ->
            // TODO: Update UI to display the history (e.g., in a list or separate area)
        })

        // TODO: Add listeners for swipe gestures and call ViewModel functions (e.g., swipeRight(), swipeLeft())

        // Placeholder for gesture detection logic:
        // You would typically attach gesture listeners to the view containing the card content.
        // When a swipe gesture is detected (left or right), you would call the appropriate ViewModel function:
        // cardsViewModel.swipeCard(SwipedDirection.RIGHT) // Assuming SwipedDirection enum exists

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null // Clean up view binding
    }
}