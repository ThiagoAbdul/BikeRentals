package com.rtlocation.bike.models

import kotlinx.serialization.Serializable

@Serializable
data class RentRequest(val bikeId: String,
                       val pointId: String,
                       val paymentMethod: Int,
                       val paymentData: String,  // JWE
                       val rentedDays: Int)

