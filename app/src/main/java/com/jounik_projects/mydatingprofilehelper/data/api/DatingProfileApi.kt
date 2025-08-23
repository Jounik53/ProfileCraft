package com.jounik_projects.mydatingprofilehelper.data.api

import com.jounik_projects.mydatingprofilehelper.data.model.News
import com.jounik_projects.mydatingprofilehelper.data.model.ProfileDescriptionExample
import com.jounik_projects.mydatingprofilehelper.data.network.model.TransactionDto
import com.jounik_projects.mydatingprofilehelper.data.network.model.GeneratedProfileHistoryDto
import com.jounik_projects.mydatingprofilehelper.data.network.model.SaveGeneratedProfileHistoryRequest

/**
 * Интерфейс для взаимодействия с API бэкенда, предоставляющего описания профилей.
 * Определяет методы для получения данных, связанных с описаниями профилей других пользователей.
 */
interface DatingProfileApi {

    /**
     * Асинхронно получает список описаний профилей от бэкенда.
     * Эта функция предназначена для запроса и получения коллекции текстовых описаний,
     * которые могут быть использованы в приложении, например, для отображения
     * примеров или в разделе с карточками.
     *
     * @return Список строк, где каждая строка представляет собой описание профиля.
     *         Возвращает пустой список в случае ошибки или отсутствия данных.
     *         Функция помечена как `suspend`, что указывает на то, что она
     *         может выполнять длительные операции (например, сетевые запросы)
     *         и должна вызываться из корутины или другой suspend функции.
     * @return A list of strings, each representing a profile description.
     */
    suspend fun getProfileDescriptions(): List<String>

    /**
     * Асинхронно получает список последних новостей с бэкенда.
     * Новости включают заголовки, контент и дату публикации.
     *
     * @return Список объектов News.
     */
    suspend fun getRecentNews(): List<News>

    /**
     * Асинхронно получает список случайных примеров описаний профилей с бэкенда.
     * Используется для отображения вдохновляющих примеров на главной странице.
     *
     * @param count Количество случайных примеров для получения (по умолчанию 5).
     * @return Список объектов ProfileDescriptionExample.
     */
    suspend fun getRandomProfileDescriptions(count: Int = 5): List<ProfileDescriptionExample>

    /**
 * Асинхронно получает текущий баланс кристаллов пользователя с бэкенда.
 *
 * @return Текущий баланс кристаллов в виде целого числа.
 */
 suspend fun getCrystalBalance(): Int

    /**
     * Асинхронно получает всю историю сгенерированных описаний для текущего пользователя с бэкенда.
     *
     * @return Список объектов GeneratedProfileHistoryDto.
     */
    suspend fun getAllGeneratedProfileHistory(): List<GeneratedProfileHistoryDto>

    /**
     * Асинхронно получает историю транзакций для текущего пользователя с бэкенда.
     *
     * @return Список объектов TransactionDto.
     */
    suspend fun getTransactionHistory(): List<TransactionDto>
}