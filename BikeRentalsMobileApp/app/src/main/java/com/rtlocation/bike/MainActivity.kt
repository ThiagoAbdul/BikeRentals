package com.rtlocation.bike

import android.content.Context
import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val sharedPrefs = getSharedPreferences("user_prefs", Context.MODE_PRIVATE)
        val isLoggedIn = sharedPrefs.getBoolean("isLoggedIn", false)

        if (isLoggedIn) {
            // Se o usuário estiver logado, redireciona para a HomeActivity
            startActivity(Intent(this, HomeActivity::class.java))
        } else {
            // Caso contrário, redireciona para a LoginActivity
            val intent = Intent(this, LoginActivity::class.java)
            startActivity(intent)
        }

        // Finaliza a MainActivity para que o usuário não retorne a ela ao pressionar "Voltar"
        finish()
    }

}
