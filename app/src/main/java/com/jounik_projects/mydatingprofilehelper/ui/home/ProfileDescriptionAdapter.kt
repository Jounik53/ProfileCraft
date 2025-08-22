package com.jounik_projects.mydatingprofilehelper.ui.home

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jounik_projects.mydatingprofilehelper.R

/**
 * RecyclerView Adapter for displaying a list of profile descriptions.
 */
class ProfileDescriptionAdapter : RecyclerView.Adapter<ProfileDescriptionAdapter.DescriptionViewHolder>() {

    private var descriptions: List<String> = emptyList()

    /**
     * ViewHolder for a single profile description item.
     */
    class DescriptionViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val descriptionTextView: TextView = itemView.findViewById(R.id.text_view_description)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): DescriptionViewHolder {
        // Inflate the layout for a single list item.
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_profile_description, parent, false) // Assuming you have a layout file named item_profile_description.xml
        return DescriptionViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: DescriptionViewHolder, position: Int) {
        // Bind the description data to the TextView in the ViewHolder.
        val currentDescription = descriptions[position]
        holder.descriptionTextView.text = currentDescription
    }

    override fun getItemCount(): Int {
        // Return the total number of descriptions in the list.
        return descriptions.size
    }

    /**
     * Updates the list of profile descriptions and notifies the adapter of the data change.
     * @param newDescriptions The new list of profile descriptions.
     */
    fun updateDescriptions(newDescriptions: List<String>) {
        descriptions = newDescriptions
        notifyDataSetChanged() // Notify the adapter that the data set has changed.
    }
}