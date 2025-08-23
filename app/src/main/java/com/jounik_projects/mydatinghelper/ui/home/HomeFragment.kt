package com.jounik_projects.mydatingprofilehelper.ui.home

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jounik_projects.mydatingprofilehelper.R

/**
 * Фрагмент для отображения главной страницы приложения.
 * Содержит список примеров описаний профилей.
 */
class HomeFragment : Fragment() {

    private lateinit var recyclerView: RecyclerView
    private lateinit var adapter: ProfileDescriptionAdapter

    // Использование viewModels() делегата для получения экземпляра HomeViewModel
    // ViewModelProvider будет использоваться для связывания ViewModel с жизненным циклом фрагмента
    private val homeViewModel: HomeViewModel by viewModels()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Раздуваем разметку для этого фрагмента
        val root = inflater.inflate(R.layout.fragment_home, container, false)

        // Привязываем RecyclerView из разметки
        recyclerView = root.findViewById(R.id.recycler_view_profile_descriptions)

        // Настраиваем RecyclerView
        recyclerView.layoutManager = LinearLayoutManager(context)

        // Создаем адаптер для RecyclerView
        adapter = ProfileDescriptionAdapter(emptyList()) // Изначально пустой список

        // Устанавливаем адаптер для RecyclerView
        recyclerView.adapter = adapter

        // Наблюдаем за списком описаний профилей из ViewModel
        // При изменении данных в ViewModel, обновляем адаптер RecyclerView
        homeViewModel.profileDescriptions.observe(viewLifecycleOwner, Observer { descriptions ->
            // Проверяем, что список описаний не null
            descriptions?.let {
                // Обновляем данные в адаптере
                adapter.updateDescriptions(it)
            }
        })

        // Загружаем описания профилей при создании фрагмента
        homeViewModel.loadProfileDescriptions()

        return root
    }
}