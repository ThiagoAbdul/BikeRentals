package com.rtlocation.bike.models

import kotlinx.serialization.Serializable

@Serializable
data class RentalPoint(val id: String,
                       val unityName: String,
                       val zipCode: String,
                       val addressNumber: Int)