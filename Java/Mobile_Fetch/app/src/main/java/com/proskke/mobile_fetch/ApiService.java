package com.proskke.mobile_fetch;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface ApiService{
    @GET("mobiliteit/deelwagenspartago.json")
    Call<List<PoolCar>> getPoolCars();

}