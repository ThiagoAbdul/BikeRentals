package com.rtlocation.bike

import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.rtlocation.bike.PaymentActivity
import com.rtlocation.bike.R

class HomeActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home)

        val welcomeMessage = findViewById<TextView>(R.id.welcomeMessage)
        val btnProfile = findViewById<Button>(R.id.btnProfile)
        val btnPayment = findViewById<Button>(R.id.btnPayment)
        val btnLogout = findViewById<Button>(R.id.btnLogout)

        welcomeMessage.text = "Bem-vindo ao aplicativo!"

        btnProfile.setOnClickListener {
            startActivity(Intent(this@HomeActivity, ListBikesActivity::class.java))

        }

        btnPayment.setOnClickListener {
            val intent = Intent(this, PaymentActivity::class.java)

            startActivity(intent)
        }

        btnLogout.setOnClickListener {
            val sharedPrefs = getSharedPreferences("user_prefs", Context.MODE_PRIVATE)
            val editor = sharedPrefs.edit()
            editor.putBoolean("isLoggedIn", false)
            editor.apply()

            startActivity(Intent(this, LoginActivity::class.java))
            finish()
        }
    }
}
