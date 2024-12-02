package com.rtlocation.bike

import android.content.SharedPreferences
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class ForgotPasswordActivity : AppCompatActivity() {

    private lateinit var etEmail: EditText
    private lateinit var etOldPassword: EditText
    private lateinit var etNewPassword: EditText
    private lateinit var etConfirmNewPassword: EditText
    private lateinit var btnConfirmReset: Button
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_forgot_password)

        etEmail = findViewById(R.id.etEmail)
        etOldPassword = findViewById(R.id.etOldPassword)
        etNewPassword = findViewById(R.id.etNewPassword)
        etConfirmNewPassword = findViewById(R.id.etConfirmNewPassword)
        btnConfirmReset = findViewById(R.id.btnConfirmReset)

        // Inicializando SharedPreferences
        sharedPreferences = getSharedPreferences("UserPrefs", MODE_PRIVATE)

        btnConfirmReset.setOnClickListener {
            resetPassword()
        }
    }

    private fun resetPassword() {
        val email = etEmail.text.toString().trim()
        val oldPassword = etOldPassword.text.toString()
        val newPassword = etNewPassword.text.toString()
        val confirmNewPassword = etConfirmNewPassword.text.toString()

        // Verificando se todos os campos foram preenchidos
        if (email.isEmpty() || oldPassword.isEmpty() || newPassword.isEmpty() || confirmNewPassword.isEmpty()) {
            Toast.makeText(this, "Por favor, preencha todos os campos", Toast.LENGTH_SHORT).show()
            return
        }

        // Verificando se as novas senhas coincidem
        if (newPassword != confirmNewPassword) {
            Toast.makeText(this, "As novas senhas não coincidem", Toast.LENGTH_SHORT).show()
            return
        }

        // Recuperando as credenciais armazenadas
        val storedEmail = sharedPreferences.getString("email", null)
        val storedPassword = sharedPreferences.getString("password", null)

        // Verificando se o email e a senha antiga estão corretos
        if (email == storedEmail && oldPassword == storedPassword) {
            // Atualizando a senha no SharedPreferences
            val editor = sharedPreferences.edit()
            editor.putString("password", newPassword)
            editor.apply()

            Toast.makeText(this, "Senha redefinida com sucesso", Toast.LENGTH_SHORT).show()
            finish() // Fechar a atividade e voltar para a tela de login ou outra tela
        } else {
            // Caso o email ou a senha antiga estejam incorretos
            Toast.makeText(this, "Email ou senha antiga incorretos", Toast.LENGTH_SHORT).show()
        }
    }
}
