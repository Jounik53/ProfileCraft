package com.jounik_projects.mydatingprofilehelper.ui.settings

import android.os.Bundle
import android.content.Intent // Импортируем Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import android.widget.AdapterView
import android.widget.ArrayAdapter
import com.jounik_projects.mydatingprofilehelper.R
import com.jounik_projects.mydatingprofilehelper.ui.login.YourLoginActivity // Замените на вашу Activity входа
import android.widget.Button // Import Button
import android.widget.Spinner
import android.widget.TextView
import com.jounik_projects.mydatingprofilehelper.utils.LocaleHelper // Импортируем LocaleHelper



/**
 * A simple [Fragment] subclass representing the Settings screen.
 */
class SettingsFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
 val view = inflater.inflate(R.layout.fragment_settings, container, false)
        
        // Получение экземпляра SettingsViewModel
 val settingsViewModel: SettingsViewModel by viewModels()
        
        // Привязка Spinner для выбора языка из XML разметки
 val languageSpinner = view.findViewById<android.widget.Spinner>(R.id.languageSpinner)
        
        // Заполнение Spinner опциями языков из ресурсов strings.xml
        val languages = arrayOf(
            getString(R.string.language_russian),
            getString(R.string.language_english)
        )
        val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_spinner_item, languages)
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        languageSpinner.adapter = adapter
        
        // Наблюдение за выбранным языком из ViewModel и обновление UI Spinner
        settingsViewModel.selectedLanguage.observe(viewLifecycleOwner) { language ->
            val selection = when (language) {
                "en" -> 1 // Index for English in our array
                else -> 0 // Default to Russian
            }
            languageSpinner.setSelection(selection)
        }
        
        // Установка слушателя для обработки выбора языка в Spinner
 languageSpinner.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
 override fun onItemSelected(parent: AdapterView<*>, view: View?, position: Int, id: Long) {
                // Определение кода выбранного языка
 val selectedLanguageCode = when (position) {
 1 -> "en"
                    else -> "ru" // Default to Russian
 }
                // Вызов функции setLanguage в SettingsViewModel для сохранения выбранного языка
                settingsViewModel.setLanguage(selectedLanguageCode)
                // Примечание: Применение изменения локали требует перезапуска активности или специальной обработки
                // Устанавливаем новую локаль
                LocaleHelper.setLocale(requireContext(), selectedLanguageCode)
                // Пересоздаем активность, чтобы применить изменение языка
                activity?.recreate()
 }
            
 override fun onNothingSelected(parent: AdapterView<*>) {
 // Do nothing
 }
        }
        
        // Привязка Spinner для выбора API нейросетей
        val neuralNetworkApiSpinner = view.findViewById<Spinner>(R.id.neuralNetworkApiSpinner)
        
        // Заполнение Spinner опциями заглушек API (пока используем названия)
        // В реальном приложении здесь будут названия реальных API
        val apiOptions = arrayOf("Заглушка API 1", "Заглушка API 2") // Пример названий заглушек
        val apiAdapter = ArrayAdapter(requireContext(), android.R.layout.simple_spinner_item, apiOptions)
        apiAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        neuralNetworkApiSpinner.adapter = apiAdapter
        
        // Установка начального выбора на основе сохраненного API из ViewModel
        settingsViewModel.selectedNeuralNetworkApi.observe(viewLifecycleOwner) { api ->
            val selection = when (api) {
                "Заглушка API 2" -> 1 // Индекс для "Заглушка API 2"
                else -> 0 // По умолчанию "Заглушка API 1"
            }
            neuralNetworkApiSpinner.setSelection(selection)
        }
        
        // Установка слушателя для изменений выбора API нейросетей
        neuralNetworkApiSpinner.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onItemSelected(parent: AdapterView<*>, view: View?, position: Int, id: Long) {
                // Определение выбранного API по позиции в Spinner
                val selectedApi = apiOptions[position]
                // Вызов функции setNeuralNetworkApi в SettingsViewModel для сохранения выбора
                settingsViewModel.setNeuralNetworkApi(selectedApi)
            }
            
            override fun onNothingSelected(parent: AdapterView<*>) {
                // Ничего не делать
            }
        }
        
        // Привязка Spinner для выбора темы
        val themeSpinner = view.findViewById<Spinner>(R.id.themeSpinner)
        
        // Заполнение Spinner опциями тем
        val themes = arrayOf(
            getString(R.string.theme_light),
            getString(R.string.theme_dark)
        )
        val themeAdapter = ArrayAdapter(requireContext(), android.R.layout.simple_spinner_item, themes)
        themeAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        themeSpinner.adapter = themeAdapter
        
        // Установка начального выбора на основе сохраненной темы из ViewModel
        settingsViewModel.selectedTheme.observe(viewLifecycleOwner) { theme ->
            val selection = when (theme) {
                "dark" -> 1 // Индекс для "Темная тема"
                else -> 0 // По умолчанию "Светлая тема"
            }
            themeSpinner.setSelection(selection)
        }
        
        // Установка слушателя для изменений выбора темы
        themeSpinner.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onItemSelected(parent: AdapterView<*>, view: View?, position: Int, id: Long) {
                // Определение выбранной темы по позиции в Spinner
                val selectedTheme = when (position) {
                    1 -> "dark"
                    else -> "light" // По умолчанию светлая тема
                }
                // Вызов функции setTheme в SettingsViewModel для сохранения выбора темы
                settingsViewModel.setTheme(selectedTheme)
                // Примечание: Применение изменения темы может потребовать перезапуска активности
            }
            
            override fun onNothingSelected(parent: AdapterView<*>) {
                // Ничего не делать
            }
        }

        // Привязка кнопки выхода
        val logoutButton = view.findViewById<Button>(R.id.button_logout) // Предполагается, что у вас есть кнопка с ID button_logout

        // Установка слушателя клика на кнопку выхода
        logoutButton.setOnClickListener {
            // TODO: Выход из Google аккаунта
            // Например: googleSignInClient.signOut()
            // Возможно, также потребуется googleSignInClient.revokeAccess() для полного отзыва доступа
            // Логика очистки токенов аутентификации и локальных данных пользователя
            //AuthTokenProvider.clearToken() // Раскомментируйте и реализуйте очистку токена

            // Создаем Intent для перехода на Activity входа
            val intent = Intent(activity, YourLoginActivity::class.java) // Замените YourLoginActivity на вашу Activity входа
            intent.flags = Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK // Очищаем стек активностей
            startActivity(intent) // Запускаем Activity входа
        }

 return view
    }
}