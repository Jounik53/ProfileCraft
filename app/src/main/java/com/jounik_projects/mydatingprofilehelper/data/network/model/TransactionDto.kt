package com.jounik_projects.mydatingprofilehelper.data.network.model

import com.google.gson.annotations.SerializedName

/**
 * DTO (Data Transfer Object) для передачи информации о транзакции с бэкенда.
 */
data class TransactionDto(
    @SerializedName("id")
    val id: Int, // Уникальный идентификатор транзакции

    @SerializedName("amount")
    val amount: Double, // Сумма транзакции (кристаллы, может быть положительной или отрицательной)

    @SerializedName("type")
    val type: String, // Тип транзакции (например, "Credit", "Debit", "NeuralNetworkGeneration")

    @SerializedName("timestamp")
    val timestamp: String, // Время создания транзакции (строка, рекомендуется использовать формат ISO 8601)

    @SerializedName("details")
    val details: String? // Дополнительные детали транзакции (необязательно)
)