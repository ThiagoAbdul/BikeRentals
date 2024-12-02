package com.rtlocation.bike.models

import kotlinx.serialization.Serializable

@Serializable
data class SignInResponse(val userId: String,
                          val accessToken: String,
                          val refreshToken: String)