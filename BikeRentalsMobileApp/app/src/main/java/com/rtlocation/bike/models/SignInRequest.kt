package com.rtlocation.bike.models

import kotlinx.serialization.Serializable

@Serializable
data class SignInRequest(val email: String, val password: String)