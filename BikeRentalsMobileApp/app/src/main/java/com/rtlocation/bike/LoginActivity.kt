package com.rtlocation.bike

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.rtlocation.bike.models.SignInResponse
import com.rtlocation.bike.services.AuthService
import io.ktor.client.call.body
import io.ktor.http.HttpStatusCode
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import org.koin.android.ext.android.inject

class LoginActivity : AppCompatActivity() {

    private lateinit var inputEmail: EditText
    private lateinit var inputPassword: EditText
    private lateinit var btnLogin: Button
    private lateinit var btnForgotPassword: Button
    private lateinit var btnRegister: Button
    private lateinit var sharedPreferences: SharedPreferences
    private val authService: AuthService by inject()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        inputEmail = findViewById(R.id.inputEmail)
        inputPassword = findViewById(R.id.inputPassword)
        btnLogin = findViewById(R.id.btnLogin)
        btnForgotPassword = findViewById(R.id.btnForgotPassword)
        btnRegister = findViewById(R.id.btnRegister)

        sharedPreferences = getSharedPreferences("UserPrefs", MODE_PRIVATE)


        btnLogin.setOnClickListener {
            val email = inputEmail.text.toString()
            val password = inputPassword.text.toString()

            CoroutineScope(Dispatchers.Main).launch {
                val response = authService.signIn(email, password)
                if(response.status == HttpStatusCode.OK){
                    sharedPreferences.edit()
                        .putBoolean("isLoggedIn", true).apply() // Marcar como logado

                    sharedPreferences.edit()
                        .putString("accessToken", response.body<SignInResponse>().accessToken)
                        .putString("refreshToken", response.body<SignInResponse>().refreshToken)
                        .apply()

                    startActivity(Intent(this@LoginActivity, HomeActivity::class.java))
                    finish()
                }
                else Toast.makeText(this@LoginActivity,
                        "Email ou senha incorretos",
                        Toast.LENGTH_SHORT).show()

            }

        }

        // Navegar para a tela de Esqueci minha senha
        btnForgotPassword.setOnClickListener {
            val intent = Intent(this, ForgotPasswordActivity::class.java)
            startActivity(intent)
        }

        // Navegar para a tela de registro
        btnRegister.setOnClickListener {
            startActivity(Intent(this, RegisterActivity::class.java))
        }
    }
}
