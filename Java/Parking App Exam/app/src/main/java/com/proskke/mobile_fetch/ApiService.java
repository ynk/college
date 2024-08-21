package com.proskke.mobile_fetch;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface ApiService{
    @GET("mobiliteit/bezettingparkingsrealtime.json")
    Call<List<Parking>> getAllParkings();

}