package com.jounik_projects.mydatingprofilehelper

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.content.Context
import android.os.Bundle
import android.util.Log
import android.widget.Button
import com.google.android.gms.auth.api.signin.GoogleSignIn
import com.google.android.gms.auth.api.signin.GoogleSignInClient
import com.google.android.gms.auth.api.signin.GoogleSignInOptions
import com.google.android.gms.auth.api.signin.GoogleSignInAccount
import com.google.android.gms.tasks.Task
import com.google.android.gms.common.api.ApiException
import com.jounik_projects.mydatingprofilehelper.data.network.api.AuthApi
import com.jounik_projects.mydatingprofilehelper.data.network.model.GoogleLoginRequest
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class MainActivity : AppCompatActivity() {

    private lateinit var googleSignInClient: GoogleSignInClient
    private val RC_SIGN_IN = 9001 // Request code for Google Sign-In
    // Request code for Google Sign-In

    // TODO: Переместить базовый URL бэкенда в файл конфигурации
    private val BASE_URL = "http://10.0.2.2:5000" // Пример: URL бэкенда (10.0.2.2 для эмулятора Android)

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        // Set the content view to the activity_main layout. This layout will contain the Google Sign-In button.

        // Configure Google Sign-in to request the user's ID, email address, and basic profile information.
        // DEFAULT_SIGN_IN это удобный вариант, который запрашивает ID и базовый профиль.
        val gso = GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
            .requestEmail() // Явно запрашиваем адрес электронной почты пользователя
            .build()

        // Build a GoogleSignInClient with the options specified by gso.
        googleSignInClient = GoogleSignIn.getClient(this, gso)

        // Find the Google Sign-In button in the layout and set its click listener.
        val signInButton: Button = findViewById(R.id.sign_in_button)
        signInButton.setOnClickListener {
            signIn()
        }
    }

    /**
     * Initiates the Google Sign-In flow by creating and launching the sign-in intent.
     */
    private fun signIn() {
        val signInIntent: Intent = googleSignInClient.signInIntent
        startActivityForResult(signInIntent, RC_SIGN_IN)
        // Start the sign-in activity and expect a result back, identified by RC_SIGN_IN.
    }

    // Handle the result of the Google Sign-In intent
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        // Result returned from launching the Intent from GoogleSignInClient.getSignInIntent(...);
        if (requestCode == RC_SIGN_IN) {
            // The Task returned from this call is always completed, no need to attach
            // a listener. Get the GoogleSignInAccount from the intent.
            val task = GoogleSignIn.getSignedInAccountFromIntent(data)
            handleSignInResult(task)
        }
    }

    /**
     * Handles the result of the Google Sign-In operation.
     * If the sign-in is successful, it retrieves the user's account information.
     *
     * @param completedTask The task containing the result of the sign-in operation.
     */
    private fun handleSignInResult(completedTask: Task<GoogleSignInAccount>) {
        try {
            // Attempt to get the signed-in account from the completed task.
            // getResult(ApiException::class.java) will throw an ApiException if the sign-in failed.
            // Попытка получить аккаунт из завершенной задачи.
            // getResult(ApiException::class.java) выбросит ApiException, если вход не удался.
            val account = completedTask.getResult(ApiException::class.java)

            // Signed in successfully.
            // 'account' now contains the signed-in user's Google account information.
            // Вход выполнен успешно.
            // 'account' теперь содержит информацию о Google аккаунте пользователя.

            // Получаем Google ID Token
            val idToken = account?.idToken

            if (idToken != null) {
                // Если токен получен, отправляем его на бэкенд для авторизации
                authenticateWithBackend(idToken)
            } else {
                // TODO: Обработка случая, когда Google Token не был получен
                Log.e("MainActivity", "Google ID Token не получен")
            }

        } catch (e: ApiException) {
            // The ApiException status code indicates the detailed failure reason.
            // Код статуса ApiException указывает на подробную причину сбоя.
            Log.w("MainActivity", "Ошибка входа Google:" + e.statusCode)
            // TODO: Обновить UI с сообщением об ошибке
        }
    }

    /**
     * Sends the Google ID Token to the backend for authentication and obtains a JWT.
     */
    private fun authenticateWithBackend(googleToken: String) {
        // Создаем экземпляр Retrofit
        val retrofit = Retrofit.Builder()
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .build()

        // Создаем экземпляр AuthApi
        val authApi = retrofit.create(AuthApi::class.java)

        // Выполняем запрос к бэкенду в корутине (асинхронно)
        CoroutineScope(Dispatchers.IO).launch {
            try {
                val requestBody = GoogleLoginRequest(googleToken)
                val response = authApi.googleLogin(requestBody)

                if (response.isSuccessful && response.body() != null) {
                    val jwt = response.body()!!.jwt
                    // TODO: Сохранить JWT безопасным способом (например, в SharedPreferences или Keystore)
                    Log.d("MainActivity", "Авторизация на бэкенде успешна. Получен JWT: $jwt")
                    // TODO: Переход на следующий экран (например, BottomNavigationActivity)
                } else {
                    // TODO: Обработка ошибки авторизации на бэкенде
                    Log.e("MainActivity", "Ошибка авторизации на бэкенде: ${response.code()}")
                }
            } catch (e: Exception) {
                // TODO: Обработка сетевой ошибки
                Log.e("MainActivity", "Сетевая ошибка при авторизации на бэкенде", e)
            }
        }
    }
}