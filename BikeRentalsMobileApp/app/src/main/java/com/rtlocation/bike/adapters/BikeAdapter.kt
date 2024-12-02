package com.rtlocation.bike.adapters

import android.content.Intent
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.bumptech.glide.request.RequestOptions
import com.rtlocation.bike.R
import com.rtlocation.bike.models.Bike

class BikeAdapter(private val onBikeSelected: (Bike) -> Unit )
    : RecyclerView.Adapter<BikeAdapter.BikeViewHolder>() {

    val bikeList: MutableList<Bike> = mutableListOf()


    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BikeViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_bike, parent, false)

        return BikeViewHolder(itemView)
    }

    override fun getItemCount() = bikeList.size


    override fun onBindViewHolder(holder: BikeViewHolder, position: Int) {
        val bike: Bike = bikeList[position]
        val bikeName = bike.getBikeDescription()

        holder.tvBikeName.text = bikeName

        loadImage(holder, bike.imagePath)

        holder.tvBikeName.setOnClickListener{
            onBikeSelected(bike) // lambda from constructor
        }

        holder.imageView.setOnClickListener{
            onBikeSelected(bike) // lambda from constructor
        }
    }

    class BikeViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView){
        val tvBikeName: TextView = itemView.findViewById(R.id.tv_item_bike_name)
        val imageView = itemView.findViewById<ImageView>(R.id.bikeImg)
    }

    private fun loadImage(holder: BikeViewHolder, imageUrl: String){
        Glide.with(holder.imageView.context)
            .load(imageUrl) // Load the image URL
            .apply(
                RequestOptions())
            .into(holder.imageView)
    }
}
