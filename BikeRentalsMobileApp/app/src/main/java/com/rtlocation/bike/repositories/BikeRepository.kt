package com.rtlocation.bike.repositories

import com.rtlocation.bike.API_URL
import com.rtlocation.bike.models.Bike
import com.rtlocation.bike.models.Page
import com.rtlocation.bike.models.RentRequest
import io.ktor.client.HttpClient
import io.ktor.client.call.body
import io.ktor.client.request.get
import io.ktor.client.request.headers
import io.ktor.client.request.post
import io.ktor.client.request.setBody
import io.ktor.http.ContentType
import io.ktor.http.HttpHeaders
import io.ktor.http.append
import io.ktor.http.contentType

class BikeRepository(val http: HttpClient) {

    private val baseUrl = "$API_URL/api/bike"

    suspend fun getBikes(accessToken: String, page: Int = 1, size: Int = 10): Page{
        return http.get(baseUrl){
            url {
                parameters.append("page", page.toString())
                parameters.append("size", size.toString())

            }
            headers {
                append(HttpHeaders.Authorization, "Bearer $accessToken")
            }
        }.body()
    }

    suspend fun rent(request: RentRequest, accessToken: String){
        return http.post("$baseUrl/rent"){
            headers {
                append(HttpHeaders.Authorization, "Bearer $accessToken")
            }
            contentType(ContentType.Application.Json)
            setBody(request)
        }.body()
    }
}