package com.rtlocation.bike

import android.app.Application
import com.rtlocation.bike.di.appModule
import com.rtlocation.bike.di.networkModule
import org.koin.android.ext.koin.androidContext
import org.koin.android.ext.koin.androidLogger
import org.koin.core.context.startKoin

class BikeRentalsApplication : Application(){

    override fun onCreate() {
        super.onCreate()


        startKoin{
            androidLogger()
            androidContext(this@BikeRentalsApplication)
            modules(appModule, networkModule)
        }

    }
}