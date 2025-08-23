package com.jounik_projects.mydatingprofilehelper.ui.navigation

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.activity.viewModels // Импортируем для использования activityViewModels
import com.google.android.material.bottomnavigation.BottomNavigationView
import com.jounik_projects.mydatingprofilehelper.R
import com.jounik_projects.mydatingprofilehelper.ui.cards.CardsFragment
import com.jounik_projects.mydatingprofilehelper.ui.profile.ProfileFragment
import com.jounik_projects.mydatingprofilehelper.ui.user.UserViewModel // Предполагается, что у вас есть UserViewModel
import android.widget.TextView // Импортируем TextView
import com.jounik_projects.mydatingprofilehelper.ui.history.HistoryFragment
import com.jounik_projects.mydatingprofilehelper.ui.home.HomeFragment
import com.jounik_projects.mydatingprofilehelper.ui.settings.SettingsFragment

/**
 * Activity, которая содержит BottomNavigationView и отображает различные фрагменты
 * в зависимости от выбранного пункта меню. Служит основным контейнером для
 * экранов профиля, главной страницы и карточек.
 */
class BottomNavigationActivity : AppCompatActivity() {

    // ViewModel для управления данными пользователя (предполагается ее существование)
    private val userViewModel: UserViewModel by viewModels()

    // TextView для отображения баланса кристаллов
    private lateinit var textCrystalBalance: TextView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        // Устанавливаем разметку для этой активности
        setContentView(R.layout.activity_bottom_navigation)

        // Находим BottomNavigationView в разметке
        val navView: BottomNavigationView = findViewById(R.id.nav_view)

        // Находим TextView для баланса кристаллов в разметке
        textCrystalBalance = findViewById(R.id.text_crystal_balance)


        // Устанавливаем слушатель для выбора пунктов меню
        // При выборе пункта меню, заменяем текущий фрагмент в контейнере
        // соответствующим фрагментом.
        navView.setOnItemSelectedListener { item ->
            var selectedFragment: Fragment? = null
            when (item.itemId) {
                R.id.navigation_profile -> {
                    selectedFragment = ProfileFragment()
                }
                R.id.navigation_home -> {
                    selectedFragment = HomeFragment()
                }
                R.id.navigation_cards -> {
                    selectedFragment = CardsFragment()
                }
                R.id.navigation_settings -> {
                    selectedFragment = SettingsFragment()
                }
                // Обработка выбора пункта меню "История"
                R.id.navigation_history -> {
                    selectedFragment = HistoryFragment()
                }
            }
            // Загружаем выбранный фрагмент, если он не null
            selectedFragment?.let {
                // Используем FragmentManager для выполнения транзакции фрагмента
                supportFragmentManager.beginTransaction()
                    // replace заменяет текущий фрагмент в указанном контейнере
                    // новым фрагментом.
                    .replace(R.id.fragment_container, it) // fragment_container - это ID FrameLayout в activity_bottom_navigation.xml
                    .commit()
            }
            true // Возвращаем true, чтобы указать, что событие обработано
        }

        // Загружаем начальный фрагмент при создании активности
        // Например, загрузим главный экран по умолчанию
        // (savedInstanceState == null)
        if (savedInstanceState == null) {
            supportFragmentManager.beginTransaction()
                .replace(R.id.fragment_container, HomeFragment())
                .commit()
        }

        // Наблюдаем за изменениями баланса кристаллов в ViewModel и обновляем TextView
        userViewModel.crystalBalance.observe(this, Observer { balance ->
            // Обновляем текст TextView с текущим балансом
            textCrystalBalance.text = getString(R.string.crystal_balance_format, balance) // Используем строковый ресурс для форматирования
        })

        // Возможно, вам нужно вызвать метод в userViewModel для загрузки начального баланса при создании активности
    }
}


// TODO: Реализовать UserViewModel с LiveData для crystalBalance
// TODO: Реализовать загрузку начального баланса при создании UserViewModel
// TODO: Реализовать логику обновления баланса (например, после покупок) в UserViewModel