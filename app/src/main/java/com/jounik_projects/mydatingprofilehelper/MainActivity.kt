package com.jounik_projects.mydatingprofilehelper

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import com.google.android.gms.auth.api.signin.GoogleSignIn
import com.google.android.gms.auth.api.signin.GoogleSignInClient
import com.google.android.gms.auth.api.signin.GoogleSignInOptions
import com.google.android.gms.auth.api.signin.GoogleSignInAccount
import com.google.android.gms.tasks.Task
import com.google.android.gms.drive.Drive
import com.google.android.gms.drive.DriveContents
import com.google.android.gms.common.api.ApiException
import com.jounik_projects.mydatingprofilehelper.data.repository.UserRepository

class MainActivity : AppCompatActivity() {

    private lateinit var googleSignInClient: GoogleSignInClient
    private val RC_SIGN_IN = 9001 // Request code for Google Sign-In
    // Request code for Google Sign-In

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        // Set the content view to the activity_main layout. This layout will contain the Google Sign-In button.

        // Configure Google Sign-in to request the user's ID, email address, and basic profile information.
        // DEFAULT_SIGN_IN is a convenience option that requests the ID and basic profile.
        // requestEmail() explicitly requests the user's email address.
        // requestScopes(com.google.android.gms.drive.DriveContents.SCOPE_APPFOLDER) requests permission to access the app-specific folder on Google Drive for data backup.

        // Configure Google Sign-in to request the user's ID, email, and basic profile.
        // Also request the DriveContentsScope to access user's Google Drive files.
        val gso = GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
            .requestEmail().requestScopes(DriveContents.SCOPE_APPFOLDER) // Request scope for app-specific folder on Drive
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
            val account = completedTask.getResult(ApiException::class.java)

            // Signed in successfully.
            // 'account' now contains the signed-in user's Google account information.

            // Obtain a DriveClient from the signed-in GoogleSignInAccount.
            val driveClient = Drive.getDriveClient(this, account)

            // Create an instance of UserRepository.
            // In a real application, this might be managed by a dependency injection framework.
            val userRepository = UserRepository() // Initialize your UserRepository

            // Now you can potentially load the user's profile from Google Drive.
            // This operation should be handled asynchronously.
            // For now, we just call the function with a comment.
            // userRepository.loadProfileFromDrive(driveClient) // Implement loading logic here

            // TODO: Navigate to the next activity (e.g., BottomNavigationActivity)
        } catch (e: ApiException) {
            // The ApiException status code indicates the detailed failure reason.
        }
    }
}