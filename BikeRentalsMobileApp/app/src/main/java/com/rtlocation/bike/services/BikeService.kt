package com.rtlocation.bike.services

import com.rtlocation.bike.models.Bike
import com.rtlocation.bike.models.RentRequest
import com.rtlocation.bike.repositories.BikeRepository

class BikeService(private val repository: BikeRepository ) {


    suspend fun getBikes(accessToken: String): List<Bike>{
        return repository.getBikes(accessToken).items
    }

    suspend fun rent(bikeId: String, pointId: String, accessToken: String){
        val defaultRentedDays = 14
        val request = RentRequest(bikeId,
                                  pointId,
                    0,
                      "JWE_MOCK",
                                 defaultRentedDays) // 2 semanas
        repository.rent(request, accessToken)

    }
}