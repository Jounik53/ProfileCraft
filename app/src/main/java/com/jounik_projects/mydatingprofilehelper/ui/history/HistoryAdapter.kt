package com.jounik_projects.mydatingprofilehelper.ui.history

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jounik_projects.mydatingprofilehelper.R
import com.jounik_projects.mydatingprofilehelper.data.network.model.GeneratedProfileHistoryDto

/**
 * Адаптер для отображения списка сгенерированных анкет в RecyclerView.
 */
class HistoryAdapter(private var historyList: List<GeneratedProfileHistoryDto> = emptyList()) :
    RecyclerView.Adapter<HistoryAdapter.HistoryViewHolder>() {

    /**
     * ViewHolder для элементов списка истории.
     */
    class HistoryViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val generatedText: TextView = itemView.findViewById(R.id.text_generated_profile) // TextView для отображения сгенерированного текста
        val timestamp: TextView = itemView.findViewById(R.id.text_timestamp) // TextView для отображения временной метки
        // Добавьте другие элементы UI, если необходимо
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): HistoryViewHolder {
        // Создание нового ViewHolder путем раздувания макета элемента списка
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_generated_profile_history, parent, false) // Макет элемента истории (нужно создать)
        return HistoryViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: HistoryViewHolder, position: Int) {
        // Привязка данных из элемента списка к элементам UI ViewHolder
        val historyItem = historyList[position]
        holder.generatedText.text = historyItem.generatedText // Отображение сгенерированного текста
        holder.timestamp.text = historyItem.timestamp // Отображение временной метки (возможно, потребуется форматирование)
        // Привязка других данных, если есть
    }

    override fun getItemCount(): Int {
        // Возвращает общее количество элементов в списке
        return historyList.size
    }

    /**
     * Обновляет список данных и уведомляет адаптер об изменениях.
     * @param newList Новый список сгенерированной истории профилей.
     */
    fun updateList(newList: List<GeneratedProfileHistoryDto>) {
        historyList = newList
        notifyDataSetChanged() // Уведомление адаптера об изменении данных
    }
}