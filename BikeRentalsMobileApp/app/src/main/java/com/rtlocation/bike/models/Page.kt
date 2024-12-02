package com.rtlocation.bike.models

import kotlinx.serialization.Serializable

@Serializable
class Page(val items: List<Bike>,
           val pageIndex: Int,
           val pageSize: Int,
           val pageCount: Int,
           val totalCount: Int) {
}
