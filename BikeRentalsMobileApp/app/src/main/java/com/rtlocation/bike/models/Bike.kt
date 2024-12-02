package com.rtlocation.bike.models

import kotlinx.serialization.Serializable

@Serializable
class Bike(val id: String,
                val brand: Brand,
                val basePrice: Double,
                val wheelSize: Double,
                val imagePath: String,
                val rentalPoints: List<RentalPoint>,
                val numberOfGears: Int)
{
    fun getBikeDescription(): String{
        return this.brand.name + " aro " + this.wheelSize.toInt() + " - R$" + this.basePrice
    }
}



//    {
//        "bikeType": 0,
//        "frameMaterial": 1,
//        "frameSize": 2,
//        "wheelSize": 26,
//        "suspensionType": 0,
//        "transmissionType": 1,
//        "color": 6,
//        "brakeType": 1,
//    }
