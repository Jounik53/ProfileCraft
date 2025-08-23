package com.jounik_projects.mydatingprofilehelper.ui.history

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import com.jounik_projects.mydatingprofilehelper.data.network.model.TransactionDto
import androidx.recyclerview.widget.RecyclerView
import com.jounik_projects.mydatingprofilehelper.R
import java.text.SimpleDateFormat
import java.util.*

/**
 * Адаптер для отображения списка транзакций в RecyclerView.
 */
class TransactionHistoryAdapter(private var transactions: List<TransactionDto>) : RecyclerView.Adapter<TransactionHistoryAdapter.TransactionViewHolder>() {

    /**
     * ViewHolder для элемента списка транзакции.
     * Содержит ссылки на элементы View внутри каждого элемента списка.
     */
    class TransactionViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        // TODO: Добавить TextView для отображения данных транзакции (сумма, тип, дата, детали)
        val amountTextView: TextView = itemView.findViewById(R.id.text_transaction_amount) // Пример ID
        val typeTextView: TextView = itemView.findViewById(R.id.text_transaction_type) // Пример ID
        val timestampTextView: TextView = itemView.findViewById(R.id.text_transaction_timestamp) // Пример ID
        val detailsTextView: TextView = itemView.findViewById(R.id.text_transaction_details) // Пример ID
    }

    /**
     * Создает новые ViewHolders при необходимости.
     * Вызывается LayoutManager'ом.
     */
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): TransactionViewHolder {
        // TODO: Создать разметку для одного элемента транзакции (например, item_transaction.xml)
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_transaction, parent, false) // TODO: Заменить R.layout.item_transaction на вашу разметку
        return TransactionViewHolder(itemView)
    }

    /**
     * Заменяет содержимое ViewHolder'а.
     * Вызывается LayoutManager'ом.
     */
    override fun onBindViewHolder(holder: TransactionViewHolder, position: Int) {
        // TODO: Привязать данные из TransactionDto к TextView
        val currentTransaction = transactions[position]
        val dateFormat = SimpleDateFormat("yyyy-MM-dd HH:mm", Locale.getDefault())
        
        // Заполняем TextView данными из currentTransaction
        holder.amountTextView.text = currentTransaction.amount.toString()
        holder.typeTextView.text = currentTransaction.type ?: "Неизвестно" // Используем "Неизвестно" если тип null
        holder.timestampTextView.text = dateFormat.format(Date(currentTransaction.timestamp)) // Форматируем timestamp в читаемый формат
        holder.detailsTextView.text = currentTransaction.details ?: "Нет деталей" // Используем "Нет деталей" если детали null
    }
    /**
     * Возвращает общее количество элементов в наборе данных.
     */
    override fun getItemCount() = transactions.size

    /**
     * Обновляет данные в адаптере и уведомляет RecyclerView об изменениях.
     *
     * @param transactions Новый список транзакций для отображения.
     */
    fun updateData(transactions: List<TransactionDto>) {
        this.transactions = transactions
        notifyDataSetChanged() // Уведомляем адаптер о том, что данные изменились и требуется перерисовка
    }
}