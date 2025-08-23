package com.jounik_projects.mydatingprofilehelper.ui.profile

import android.content.Intent
import android.net.Uri
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import androidx.fragment.app.viewModels
import androidx.lifecycle.Observer
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.ImageView
import android.widget.ProgressBar
import android.widget.TextView
import android.widget.Toast
import android.widget.RadioGroup
import android.widget.RadioButton
import com.jounik_projects.mydatingprofilehelper.data.network.model.UserProfileDto // Использовать DTO из сети
import com.jounik_projects.mydatingprofilehelper.data.network.model.SaveUserProfileRequest // Использовать Request DTO
import com.jounik_projects.mydatingprofilehelper.data.network.model.GeneratedProfileHistoryDto
import com.jounik_projects.mydatingprofilehelper.data.model.UserProfile // Импортируем локальную модель UserProfile

import com.jounik_projects.mydatingprofilehelper.R // Assuming you have a layout file for the profile fragment

/*
 * Represents the profile screen of the application.
 * A simple [Fragment] subclass.
 * This fragment displays the user's profile information,
 * allowing them to view and edit their data.
 * It will contain fields for name, photos, interests, dating goals, description, and crystals balance.
 */
class ProfileFragment : Fragment() {
/*
 * Фрагмент для отображения и редактирования профиля пользователя.
 */

    // Obtain a ProfileViewModel instance using the viewModels delegate.
    // This ensures the ViewModel survives configuration changes.
    private val profileViewModel: ProfileViewModel by viewModels()

    // UI elements
    private lateinit var nameEditText: EditText
    private lateinit var photosImageView: ImageView // Placeholder for displaying photos
    private lateinit var interestsEditText: EditText // Поле для интересов
    private lateinit var dislikesEditText: EditText // Поле для антипатий
    private lateinit var harmfulHabitsEditText: EditText // Поле для вредных привычек
    private lateinit var descriptionEditText: EditText // Поле для краткого описания
    private lateinit var heightEditText: EditText // Поле для роста
    private lateinit var hairColorEditText: EditText // Поле для цвета волос
    private lateinit var bodyTypeEditText: EditText // Поле для телосложения
    private lateinit var crystalsTextView: TextView
    private lateinit var genderRadioGroup: RadioGroup // Группа радиокнопок для выбора пола

    private lateinit var saveButton: Button
    private lateinit var generateButton: Button // Кнопка "Сгенерировать анкету"
    private lateinit var generatedDescriptionTextView: TextView // TextView для отображения сгенерированного текста
    private lateinit var buyCrystalsButton: Button // Кнопка "Купить кристаллы"

    /**
     * Called to have the fragment instantiate its user interface view.
     * @param inflater The LayoutInflater object that can be used to inflate any views in the fragment.
     * @param container If non-null, this is the parent view that the fragment's UI should be attached to.
     * @param savedInstanceState If non-null, this fragment is being re-constructed from a previous saved state as given here.
     * @return The View for the fragment's UI, or null.
     */
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        // Надуваем разметку для этого фрагмента. R.layout.fragment_profile должен быть создан.
        val view = inflater.inflate(R.layout.fragment_profile, container, false)

        // Привязываем элементы UI к переменным
        nameEditText = view.findViewById(R.id.nameEditText)
        photosImageView = view.findViewById(R.id.photosImageView) // Заглушка для отображения фото. Позже нужно будет реализовать загрузку/отображение.
        interestsEditText = view.findViewById(R.id.interestsEditText)
        dislikesEditText = view.findViewById(R.id.dislikesEditText)
        harmfulHabitsEditText = view.findViewById(R.id.harmfulHabitsEditText)
        descriptionEditText = view.findViewById(R.id.descriptionEditText)
        heightEditText = view.findViewById(R.id.heightEditText)
        hairColorEditText = view.findViewById(R.id.hairColorEditText)
        bodyTypeEditText = view.findViewById(R.id.bodyTypeEditText)
        crystalsTextView = view.findViewById(R.id.crystalsTextView)
        genderRadioGroup = view.findViewById(R.id.genderRadioGroup) // Привязываем RadioGroup
        saveButton = view.findViewById(R.id.saveButton)
        generateButton = view.findViewById(R.id.buttonGenerateProfile) // Привязываем кнопку генерации
        generatedDescriptionTextView = view.findViewById(R.id.generatedDescriptionTextView) // Привязываем TextView для сгенерированного текста
        buyCrystalsButton = view.findViewById(R.id.buttonBuyCrystals) // Привязываем кнопку "Купить кристаллы"

        // Наблюдаем за данными UserProfile из ViewModel.
        // Когда данные профиля изменяются в ViewModel, обновляем UI.
        profileViewModel.userProfile.observe(viewLifecycleOwner, Observer { userProfile ->
            userProfile?.let {
                // Update UI elements with profile details
                nameEditText.setText(it.name)
                // Handle photosImageView (e.g., load images using a library)
                interestsEditText.setText(it.interests.joinToString(", ")) // Отображаем интересы
                // datingGoalsEditText.setText(it.datingGoals.joinToString(", ")) // Display dating goals as a comma-separated string - поле отсутствует в актуальном DTO и модели
                dislikesEditText.setText(it.dislikes.joinToString(", ")) // Отображаем антипатии
                harmfulHabitsEditText.setText(it.harmfulHabits) // Отображаем отношение к вредным привычкам
                descriptionEditText.setText(it.description)
                heightEditText.setText(it.height?.toString()) // Отображаем рост (если не null)
                hairColorEditText.setText(it.hairColor) // Отображаем цвет волос
                bodyTypeEditText.setText(it.bodyType) // Отображаем телосложение
                crystalsTextView.text = "Кристаллы: ${it.crystals}" // Display crystal balance

                // Устанавливаем выбранный пол в RadioGroup
                when (it.gender?.lowercase()) { // Приводим к нижнему регистру для сравнения
                    "мужской" -> genderRadioGroup.check(R.id.radio_male) // R.id.radio_male должен быть ID RadioButton для мужского пола (проверьте ID в layout)
                    "женский" -> genderRadioGroup.check(R.id.radio_female) // R.id.radio_female должен быть ID RadioButton для женского пола
                    else -> genderRadioGroup.clearCheck() // Если пол не определен или другое значение, сбрасываем выбор
                }
            }
        })

        // Настраиваем слушатель кликов для кнопки сохранения
        saveButton.setOnClickListener {
            // Получаем данные из полей ввода
            val name = nameEditText.text.toString()
            val interests = interestsEditText.text.toString().split(",").map { it.trim() }.filter { it.isNotBlank() } // Разбиваем интересы по запятой, обрезаем пробелы и фильтруем пустые
            val dislikes = dislikesEditText.text.toString().split(",").map { it.trim() }.filter { it.isNotBlank() } // Разбиваем антипатии
            val harmfulHabits = harmfulHabitsEditText.text.toString() // Получаем отношение к вредным привычкам
            val description = descriptionEditText.text.toString()
            val height = heightEditText.text.toString().toIntOrNull() // Получаем рост, конвертируем в Int, может быть null
            val hairColor = hairColorEditText.text.toString() // Получаем цвет волос
            val bodyType = bodyTypeEditText.text.toString() // Получаем телосложение

            // Получаем выбранный пол из RadioGroup
            val selectedGenderId = genderRadioGroup.checkedRadioButtonId
            val gender = if (selectedGenderId != -1) {
                view.findViewById<RadioButton>(selectedGenderId).text.toString() // Получаем текст выбранной радиокнопки
            } else {
                "" // Если ничего не выбрано, устанавливаем пустую строку
            }

            // Создаем объект SaveUserProfileRequest для отправки на бэкенд
            // Важно: При сохранении профиля на бэкенде нужно убедиться,
            // что все поля передаются корректно. Возможно, стоит использовать отдельный DTO для сохранения,
            // который содержит только редактируемые поля, а не весь UserProfileDto.
            // Также учтите поле "datingGoals", которое отсутствовало в исходном списке полей для редактирования.
            // В этом примере, assumed that SaveUserProfileRequest matches the updated backend DTO structure.
            val saveRequest = SaveUserProfileRequest(
                userId = profileViewModel.userProfile.value?.userId ?: "", // Use existing userId or a default
                name = name,
                photos = profileViewModel.userProfile.value?.photos ?: emptyList(), // Пока оставляем существующие фото
                interests = interests,
                // datingGoals = datingGoals, // Поле отсутствует в актуальном DTO
                description = description,
                crystals = profileViewModel.userProfile.value?.crystals ?: 0, // Keep existing crystal balance
                dislikes = dislikes, // Добавляем антипатии
                harmfulHabits = harmfulHabits, // Добавляем вредные привычки
                height = height, // Добавляем рост
                hairColor = hairColor, // Добавляем цвет волос
                bodyType = bodyType // Добавляем телосложение
            )

            // Call the saveProfile function in ViewModel
            // Передаем SaveUserProfileRequest
            profileViewModel.saveProfile(saveRequest)

            // Показываем сообщение о сохранении (можно улучшить: показывать после успешного ответа от API)
            Toast.makeText(context, "Профиль сохранен", Toast.LENGTH_SHORT).show()

            // Загружаем профиль снова, чтобы обновить UI после сохранения (опционально,
            // если бэкенд не возвращает обновленный профиль в ответе на сохранение,
            // или если нужно убедиться, что данные корректно загрузились)
            // profileViewModel.loadProfile()
        }

        // Настраиваем слушатель кликов для кнопки генерации
        generateButton.setOnClickListener {
            // Вызываем функцию генерации анкеты в ViewModel
            profileViewModel.generateProfileDescription()
        }

        // Наблюдаем за сгенерированным текстом из ViewModel
        profileViewModel.generatedDescription.observe(viewLifecycleOwner, Observer { generatedText ->
            if (generatedText != null) {
                generatedDescriptionTextView.text = generatedText
                generatedDescriptionTextView.visibility = View.VISIBLE // Показываем TextView со сгенерированным текстом
                // Также можно автоматически сохранить сгенерированный текст в историю здесь, вызвав ViewModel
                profileViewModel.saveGeneratedHistory(generatedText) // Нужно реализовать этот метод в ViewModel
            } else {
                generatedDescriptionTextView.visibility = View.GONE // Скрываем TextView, если текста нет
            }
        })

        // Наблюдаем за состоянием загрузки генерации (опционально, для индикатора прогресса)
        // profileViewModel.isLoading.observe(viewLifecycleOwner, Observer { isLoading ->
        //    // Show/hide progress bar based on isLoading
        // })

        // Настраиваем слушатель кликов для кнопки "Купить кристаллы"
        buyCrystalsButton.setOnClickListener {
            // Замените "YOUR_BOT_USERNAME" на фактический username вашего Telegram бота
            val telegramBotUsername = "YOUR_BOT_USERNAME"
            val telegramUri = Uri.parse("tg://resolve?domain=$telegramBotUsername")
            val intent = Intent(Intent.ACTION_VIEW, telegramUri)

            try {
                startActivity(intent)
            } catch (e: Exception) {
                Toast.makeText(context, "Telegram не установлен", Toast.LENGTH_SHORT).show()
            }
        }

        // Load the user profile when the fragment is created
        profileViewModel.loadProfile() // You'll need to implement this function in ProfileViewModel

        return view
    }
}