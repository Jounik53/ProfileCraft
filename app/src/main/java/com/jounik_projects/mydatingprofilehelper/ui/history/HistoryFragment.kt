package com.jounik_projects.mydatingprofilehelper.ui.history

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jounik_projects.mydatingprofilehelper.R

class HistoryFragment : Fragment() {

 // Объявление переменных для элементов UI и ViewModel
 private lateinit var transactionRecyclerView: RecyclerView
 private lateinit var historyAdapter: TransactionHistoryAdapter // Адаптер для RecyclerView транзакций
 // Получаем ViewModel с помощью делегата viewModels()
 private val historyViewModel: HistoryViewModel by viewModels() 

 /**
 * Создает и возвращает иерархию представлений, связанную с фрагментом.
 * Здесь мы раздуваем макет и связываем RecyclerView.
 */
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
 ): View? {
 // Раздуваем макет для этого фрагмента
 return inflater.inflate(R.layout.fragment_history, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        savedInstanceState: Bundle?
    ): View? {
        // Раздуваем макет для этого фрагмента
        val root = inflater.inflate(R.layout.fragment_history, container, false)

        // Связываем RecyclerView из макета
        historyRecyclerView = root.findViewById(R.id.history_recycler_view)

        // Настраиваем RecyclerView
        historyRecyclerView.layoutManager = LinearLayoutManager(context)
        historyAdapter = HistoryAdapter() // Создаем адаптер
        historyRecyclerView.adapter = historyAdapter // Устанавливаем адаптер для RecyclerView

        // Наблюдаем за списком истории из ViewModel
        historyViewModel.historyList.observe(viewLifecycleOwner) { history ->
            // Обновляем адаптер с новыми данными истории
            historyAdapter.updateList(history)
 // Примечание: submitList обычно используется с ListAdapter, updateList - с обычным Adapter
        }

        // Загружаем историю при создании фрагмента
        historyViewModel.loadHistory()

        return root
 }
}