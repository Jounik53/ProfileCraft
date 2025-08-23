package com.jounik_projects.mydatingprofilehelper.ui.common

import android.app.Dialog
import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AlertDialog
import androidx.fragment.app.DialogFragment

/**
 * Диалоговое окно для отображения полного текста новости или описания.
 */
class FullTextDialogFragment : DialogFragment() {

    companion object {
        private const val ARG_FULL_TEXT = "full_text"

        /**
         * Создает новый экземпляр FullTextDialogFragment с заданным текстом.
         *
         * @param fullText Полный текст для отображения в диалоге.
         * @return Новый экземпляр FullTextDialogFragment.
         */
        fun newInstance(fullText: String): FullTextDialogFragment {
            val fragment = FullTextDialogFragment()
            val args = Bundle()
            args.putString(ARG_FULL_TEXT, fullText)
            fragment.arguments = args
            return fragment
        }
    }

    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {
        // Получаем полный текст из аргументов фрагмента
        val fullText = requireArguments().getString(ARG_FULL_TEXT)

        // Создаем TextView для отображения текста
        val textView = TextView(requireContext()).apply {
            text = fullText
            // Можно добавить отступы или другие стили
            setPadding(16, 16, 16, 16)
        }

        // Строим AlertDialog
        return AlertDialog.Builder(requireContext())
            .setTitle("Полный текст") // Заголовок диалога
            .setView(textView) // Устанавливаем TextView как содержимое диалога
            .setPositiveButton("Закрыть") { dialog, _ ->
                // Обработка нажатия на кнопку "Закрыть"
                dialog.dismiss() // Закрываем диалог
            }
            .create() // Создаем и возвращаем диалог
    }
}