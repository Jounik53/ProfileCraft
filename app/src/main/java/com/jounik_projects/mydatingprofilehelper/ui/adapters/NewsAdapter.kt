package com.jounik_projects.mydatingprofilehelper.ui.adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jounik_projects.mydatingprofilehelper.R
import com.jounik_projects.mydatingprofilehelper.data.model.News

/**
 * Адаптер для отображения списка новостей в RecyclerView.
 */
class NewsAdapter(
    private var newsList: List<News>,
    private val listener: OnItemClickListener // Слушатель кликов по элементам списка
) : RecyclerView.Adapter<NewsAdapter.NewsViewHolder>() {

    interface OnItemClickListener {
 fun onItemClick(news: News)
    }

    /**
     * Внутренний класс, представляющий ViewHolder для элемента новости.
     * Содержит ссылки на элементы View внутри каждого элемента списка.
     */
    class NewsViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        // Элементы View для отображения новости
        val titleTextView: TextView = itemView.findViewById(R.id.news_title)
        val contentTextView: TextView = itemView.findViewById(R.id.news_content)
        // Можно добавить другие поля, например, дату публикации
        // val dateTextView: TextView = itemView.findViewById(R.id.news_date)
    }

    /**
     * Создает новые ViewHolders при необходимости.
     * Вызывается LayoutManager'ом.
     */
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): NewsViewHolder {
        // Создаем новый View, который определяет внешний вид элемента списка
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_news, parent, false)
        return NewsViewHolder(itemView)
    }

    /**
     * Заменяет содержимое ViewHolder'а.
     * Вызывается LayoutManager'ом.
     */
    override fun onBindViewHolder(holder: NewsViewHolder, position: Int) {
        // Получаем элемент новости по его позиции
        val currentNews = newsList[position]

        // Заполняем View элемента данными из объекта новости
        holder.titleTextView.text = currentNews.title
        holder.contentTextView.text = currentNews.content

        // Устанавливаем слушатель кликов для всего элемента списка
        holder.itemView.setOnClickListener {
            listener.onItemClick(currentNews)
        }
        // Форматирование и отображение даты, если добавлено
        // holder.dateTextView.text = currentNews.publishDate // Потребуется форматирование даты
    }

    /**
     * Возвращает общее количество элементов в наборе данных.
     */
    override fun getItemCount() = newsList.size

    /**
     * Обновляет данные в адаптере и уведомляет RecyclerView об изменениях.
     *
     * @param newsList Новый список новостей для отображения.
     */
    fun updateData(newsList: List<News>) {
        this.newsList = newsList
        notifyDataSetChanged() // Уведомляем адаптер о том, что данные изменились и требуется перерисовка
    }
}