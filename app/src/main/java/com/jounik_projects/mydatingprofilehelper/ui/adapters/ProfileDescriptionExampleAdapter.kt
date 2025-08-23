package com.jounik_projects.mydatingprofilehelper.ui.adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jounik_projects.mydatingprofilehelper.R
import com.jounik_projects.mydatingprofilehelper.data.model.ProfileDescriptionExample

/**
 * Адаптер для отображения примеров описаний профилей в RecyclerView.
 */
class ProfileDescriptionExampleAdapter(
    private var descriptions: List<ProfileDescriptionExample>,
 private val listener: OnItemClickListener // Слушатель событий нажатия на элементы списка
) : RecyclerView.Adapter<ProfileDescriptionExampleAdapter.ProfileDescriptionExampleViewHolder>() {

    /**
     * ViewHolder для элемента списка примера описания профиля.
     */
    class ProfileDescriptionExampleViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        // Представление для текста описания
        val contentTextView: TextView = itemView.findViewById(R.id.text_example_description_content)
        // Кнопка "Добавить к своим"
        val addButton: Button = itemView.findViewById(R.id.button_add_to_yours)
    }

    /**
     * Создает новый ViewHolder.
     * Вызывается RecyclerView при необходимости создания нового ViewHolder для отображения элемента.
     */
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ProfileDescriptionExampleViewHolder {
        // Инфлейтим разметку для одного элемента списка
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_profile_description_example, parent, false)
        return ProfileDescriptionExampleViewHolder(view)
    }

    /**
     * Привязывает данные к ViewHolder.
     * Вызывается RecyclerView для отображения данных в указанной позиции.
     */
    override fun onBindViewHolder(holder: ProfileDescriptionExampleViewHolder, position: Int) {
        val description = descriptions[position]
        // Устанавливаем текст описания
        holder.contentTextView.text = description.generatedText
        
        // Устанавливаем слушатель нажатия на кнопку "Добавить к своим"
        holder.addButton.setOnClickListener {
 // Вызываем метод слушателя при нажатии на кнопку
 listener.onAddToYoursClick(description)
        }
        
        // Устанавливаем слушатель нажатия на всю карточку для просмотра полного текста
        holder.itemView.setOnClickListener {
 // Вызываем метод слушателя при нажатии на элемент списка
 listener.onItemClick(description)
        }
 // TODO: Возможно, стоит ограничить отображение текста в item_profile_description_example.xml
    }

    /**
     * Возвращает общее количество элементов в наборе данных.
     */
    override fun getItemCount(): Int {
        return descriptions.size
    }

    /**
     * Обновляет данные адаптера и уведомляет RecyclerView об изменениях.
     */
    fun updateData(descriptions: List<ProfileDescriptionExample>) {
        this.descriptions = descriptions
        notifyDataSetChanged() // Уведомляем адаптер, что данные изменились
    }

    /**
     * Интерфейс для обработки событий нажатия на элементы списка и кнопку в них.
     */
    interface OnItemClickListener {
        /**
         * Вызывается при нажатии на сам элемент списка (карточку).
         * @param description Пример описания, на который было совершено нажатие.
         */
 fun onItemClick(description: ProfileDescriptionExample)
        /**
         * Вызывается при нажатии на кнопку "Добавить к своим".
         * @param description Пример описания, кнопку которого нажали.
         */
 fun onAddToYoursClick(description: ProfileDescriptionExample)
    }
}