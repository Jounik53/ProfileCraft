package com.jounik_projects.mydatingprofilehelper.ui.home

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jounik_projects.mydatingprofilehelper.R
import com.jounik_projects.mydatingprofilehelper.data.model.News
import com.jounik_projects.mydatingprofilehelper.ui.adapters.NewsAdapter
import com.jounik_projects.mydatingprofilehelper.ui.adapters.ProfileDescriptionExampleAdapter
import com.jounik_projects.mydatingprofilehelper.ui.common.FullTextDialogFragment
import android.widget.Button // Импорт Button


/**
 * A simple [Fragment] subclass for the Home screen.
 * This fragment will display news and examples of other user descriptions.
 */
class HomeFragment : Fragment(), NewsAdapter.OnItemClickListener, ProfileDescriptionExampleAdapter.OnItemClickListener {

    /**
     * ViewModel for the Home screen, shared with the activity if needed.
     */
    private val homeViewModel: HomeViewModel by activityViewModels()

    /**
     * RecyclerView для отображения списка примеров описаний профилей.
     */
    private lateinit var exampleDescriptionsRecyclerView: RecyclerView

    /**
     * Адаптер для RecyclerView с примерами описаний профилей.
     */
    private lateinit var exampleDescriptionAdapter: ProfileDescriptionExampleAdapter

    /**
     * RecyclerView for displaying the list of profile descriptions.
     */
    private lateinit var newsRecyclerView: RecyclerView

    /**
     * Adapter for the RecyclerView. Placeholder for now.
     */
    private lateinit var profileDescriptionAdapter: ProfileDescriptionAdapter
 private lateinit var newsAdapter: NewsAdapter
    /**
     * Called to have the fragment instantiate its user interface view.
     * This is where you should inflate your layout and initialize your UI.
     *
     * @param inflater The LayoutInflater object that can be used to inflate any views in the fragment,
     * @param container If non-null, this is the parent view that the fragment's UI should be attached to.
     *                  The fragment should not add the view itself, but this can be used to generate
     *                  the LayoutParams of the view.
     * @param savedInstanceState If non-null, this fragment is being re-constructed from a previous
     *                           saved state as given here.
     * @return The View for the fragment's UI, or null.
     */
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? { // The root view of the fragment.
        // Inflate the layout for this fragment (assuming you have a layout file named fragment_home.xml)
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_home, container, false) // Assuming you have a layout file named fragment_home.xml
    }

    /**
     * Called immediately after onCreateView() has returned, but before any saved state has been restored in to the view.
     * This is where you should do any final customization of the fragment's view.
     *
     * @param view The View returned by onCreateView(LayoutInflater, ViewGroup, Bundle).
     * @param savedInstanceState If non-null, this fragment is being re-constructed from a previous
     *                           saved state as given here.
     */
    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        // Привязываем RecyclerView для примеров описаний профилей из разметки по его ID

        // Привязываем кнопку генерации описания
 val generateButton = view.findViewById<Button>(R.id.button_generate_description)

        // Устанавливаем слушатель клика для кнопки генерации
 generateButton.setOnClickListener {
            // TODO: Показать диалог или перейти на экран для ввода параметров и инициировать генерацию.
 }

        exampleDescriptionsRecyclerView = view.findViewById(R.id.recycler_view_descriptions)

        // Set up the RecyclerView with a LayoutManager and adapter
        // Use a LinearLayoutManager for a standard vertical list
        // Настраиваем LayoutManager для вертикального списка
        exampleDescriptionsRecyclerView.layoutManager = LinearLayoutManager(context)

        // Initialize the adapter for displaying profile descriptions.
        // Start with an empty list and it will be updated when data is available from the ViewModel.
        // Инициализируем адаптер для примеров описаний с пустым списком
 exampleDescriptionAdapter = ProfileDescriptionExampleAdapter(emptyList(), this) // Передаем текущий фрагмент как слушатель кликов
        // Set the adapter for the RecyclerView.
        // Устанавливаем адаптер для RecyclerView
        exampleDescriptionsRecyclerView.adapter = exampleDescriptionAdapter

        // Привязываем RecyclerView для новостей из разметки по его ID
        newsRecyclerView = view.findViewById(R.id.recycler_view_news)

        // Настраиваем LayoutManager для вертикального списка новостей
        newsRecyclerView.layoutManager = LinearLayoutManager(context)

        // Инициализируем адаптер для новостей с пустым списком
 newsAdapter = NewsAdapter(emptyList(), this) // Передаем текущий фрагмент как слушатель кликов

        // Наблюдаем за списком примеров описаний из HomeViewModel
        homeViewModel.exampleDescriptions.observe(viewLifecycleOwner) { descriptions ->
            // Update the adapter with the new list of descriptions
            exampleDescriptionAdapter.updateData(descriptions)
        }

        // Наблюдаем за списком свежих новостей из HomeViewModel
        homeViewModel.recentNews.observe(viewLifecycleOwner) { news ->
            // Обновляем адаптер новыми данными
            newsAdapter.updateData(news)
        }

        // Вызываем функцию ViewModel для загрузки данных главной страницы (новости и примеры описаний)
        // This triggers fetching the data, which will eventually update the LiveData and trigger the observer.
        homeViewModel.loadHomeData()
    }
    /**
 * Реализация метода onItemClick из интерфейса NewsAdapter.OnItemClickListener.
 * Вызывается при клике на элемент новости в списке.
 * Отображает полный текст новости в диалоговом окне.
 */
    // Реализация метода onItemClick для новостей
    override fun onItemClick(news: News) {
        // Создаем экземпляр FullTextDialogFragment, передавая полный текст новости
        val dialogFragment = FullTextDialogFragment.newInstance(news.content)
        dialogFragment.show(childFragmentManager, "full_news_dialog")
    }

    // Реализация метода onItemClick для примеров описаний
    override fun onItemClick(description: ProfileDescriptionExample) {
        // Создаем экземпляр FullTextDialogFragment, передавая полный текст описания
        val dialogFragment = FullTextDialogFragment.newInstance(description.generatedText)
        dialogFragment.show(childFragmentManager, "full_description_dialog")
    }

    // Реализация метода onAddToYoursClick для примеров описаний (при нажатии на кнопку "Добавить к своим")
    override fun onAddToYoursClick(description: ProfileDescriptionExample) {
        homeViewModel.addProfileDescriptionToYours(description.generatedText)
    }
}