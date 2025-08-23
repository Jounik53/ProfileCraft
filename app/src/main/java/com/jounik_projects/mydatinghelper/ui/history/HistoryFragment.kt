package com.jounik_projects.mydatingprofilehelper.ui.history

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jounik_projects.mydatingprofilehelper.R

/**
 * Фрагмент для отображения истории сгенерированных профилей.
 */
class HistoryFragment : Fragment() {

    private lateinit var recyclerView: RecyclerView

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Раздуваем макет для этого фрагмента.
        val view = inflater.inflate(R.layout.fragment_history, container, false)

        // Инициализируем RecyclerView.
        recyclerView = view.findViewById(R.id.history_recycler_view)

        // Настраиваем менеджер компоновки для RecyclerView.
        recyclerView.layoutManager = LinearLayoutManager(context)

        // TODO: Инициализировать адаптер и загрузить данные истории из ViewModel.

        return view
    }
}