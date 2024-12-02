package com.rtlocation.bike

import android.content.SharedPreferences
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.bumptech.glide.Glide
import com.bumptech.glide.request.RequestOptions
import com.rtlocation.bike.R
import com.rtlocation.bike.services.BikeService
import io.ktor.http.ContentType.Image.SVG
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import org.koin.android.ext.android.inject

class PaymentActivity : AppCompatActivity() {

    private val bikeService: BikeService by inject()
    private lateinit var bikeId: String
    private lateinit var pointId: String
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_payment)

        sharedPreferences = getSharedPreferences("UserPrefs", MODE_PRIVATE)


        val inputCardNumber = findViewById<EditText>(R.id.inputCardNumber)
        val inputCardExpiry = findViewById<EditText>(R.id.inputCardExpiry)
        val inputCardCVC = findViewById<EditText>(R.id.inputCardCVC)
        val btnConfirmPayment = findViewById<Button>(R.id.btnConfirmPayment)
        val imageView = findViewById<ImageView>(R.id.bikeFullImg)
        val tvBikeDescription = findViewById<TextView>(R.id.tvBikeKeDescription)

        val bikeDescription = intent.getStringExtra("bikeDescription")
        bikeId = intent.getStringExtra("bikeId")!!
        val imageUrl = intent.getStringExtra("imageUrl")
        pointId = intent.getStringExtra("pointId")!!


        Glide.with(this)
            .load(imageUrl) // Load the image URL
            .apply(
                RequestOptions()
            )
            .into(imageView)

        tvBikeDescription.text = bikeDescription


        btnConfirmPayment.setOnClickListener {
            val cardNumber = inputCardNumber.text.toString()
            val expiry = inputCardExpiry.text.toString()
            val cvc = inputCardCVC.text.toString()

            if (cardNumber.isBlank() || expiry.isBlank() || cvc.isBlank()) {
                return@setOnClickListener
                Toast.makeText(this, "Preencha todos os campos", Toast.LENGTH_SHORT).show()
            }

            val accessToken = sharedPreferences.getString("accessToken", "")!!

            CoroutineScope(Dispatchers.Main).launch {
                bikeService.rent(bikeId, pointId, accessToken)
                Toast.makeText(this@PaymentActivity,
                    "Pagamento realizado com sucesso",
                    Toast.LENGTH_SHORT).show()
                finish() // Retorna para a tela anterior após o pagamento

            }
        }
    }
}
