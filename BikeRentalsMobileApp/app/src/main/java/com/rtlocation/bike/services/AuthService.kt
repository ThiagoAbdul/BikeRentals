package com.rtlocation.bike.services

import com.rtlocation.bike.API_URL
import com.rtlocation.bike.models.SignInRequest
import com.rtlocation.bike.models.SignInResponse
import com.rtlocation.bike.models.SignUpRequest
import io.ktor.client.HttpClient
import io.ktor.client.call.body
import io.ktor.client.request.post
import io.ktor.client.request.setBody
import io.ktor.client.statement.HttpResponse
import io.ktor.http.ContentType
import io.ktor.http.contentType

class AuthService(private val http: HttpClient) {

    private val baseUrl = API_URL + "/auth"

    suspend fun signIn(email: String, password: String) : HttpResponse{
        return http.post("$baseUrl/sign-in")
        {
            contentType(ContentType.Application.Json)
            setBody(SignInRequest(email, password))
        }
    }

    suspend fun signUp(fullName: String, email: String, password: String) : HttpResponse{
        return http.post("$baseUrl/sign-up")
        {
            contentType(ContentType.Application.Json)
            setBody(SignUpRequest(fullName, email, password))
        }
    }
}