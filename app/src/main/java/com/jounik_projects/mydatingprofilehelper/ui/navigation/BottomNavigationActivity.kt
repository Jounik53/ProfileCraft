package com.jounik_projects.mydatingprofilehelper.ui.navigation

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment
import com.google.android.material.bottomnavigation.BottomNavigationView
import com.jounik_projects.mydatingprofilehelper.R
import com.jounik_projects.mydatingprofilehelper.ui.cards.CardsFragment
import com.jounik_projects.mydatingprofilehelper.ui.profile.ProfileFragment
import com.jounik_projects.mydatingprofilehelper.ui.home.HomeFragment
import com.jounik_projects.mydatingprofilehelper.ui.settings.SettingsFragment

/**
 * Activity, которая содержит BottomNavigationView и отображает различные фрагменты
 * в зависимости от выбранного пункта меню. Служит основным контейнером для
 * экранов профиля, главной страницы и карточек.
 */
class BottomNavigationActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        // Устанавливаем разметку для этой активности
        setContentView(R.layout.activity_bottom_navigation)

        // Находим BottomNavigationView в разметке
        val navView: BottomNavigationView = findViewById(R.id.nav_view)

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
    }
}