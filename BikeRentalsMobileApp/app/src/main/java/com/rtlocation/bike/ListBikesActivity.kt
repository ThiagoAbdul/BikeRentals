package com.rtlocation.bike

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.recyclerview.widget.LinearLayoutManager
import com.rtlocation.bike.activities.BikeDetailsActivity
import com.rtlocation.bike.adapters.BikeAdapter
import com.rtlocation.bike.databinding.ActivityListBikesBinding
import com.rtlocation.bike.services.BikeService
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import org.koin.android.ext.android.inject

class ListBikesActivity : AppCompatActivity() {
    private lateinit var binding: ActivityListBikesBinding
    private lateinit var adapter: BikeAdapter
    private lateinit var sharedPreferences: SharedPreferences

    private val bikeService: BikeService by inject()


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()

        binding = ActivityListBikesBinding.inflate(layoutInflater)
        setContentView(binding.root)


        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        sharedPreferences = getSharedPreferences("UserPrefs", MODE_PRIVATE)


        initRecyclerView()

        fetchData()
    }

    private fun initRecyclerView(){
        adapter = BikeAdapter { bike ->
            val intent = Intent(this@ListBikesActivity, PaymentActivity::class.java)
            intent.putExtra("bikeDescription", bike.getBikeDescription())
            intent.putExtra("imageUrl", bike.imagePath)
            intent.putExtra("bikeId", bike.id)
            intent.putExtra("pointId", bike.rentalPoints[0].id)

            startActivity(intent)
        }

        binding.bikeRecyclerView.layoutManager = LinearLayoutManager(this)
        binding.bikeRecyclerView.setHasFixedSize(false)
        binding.bikeRecyclerView.adapter = adapter
    }

    private fun fetchData(){
        val accessToken = sharedPreferences.getString("accessToken", "")!!

        CoroutineScope(Dispatchers.Main).launch {
            val bikes = bikeService.getBikes(accessToken)
            adapter.bikeList.addAll(bikes)
            adapter.notifyDataSetChanged()
        }
    }
}