package com.proskke.mobile1_jsonfetch;

import com.google.gson.annotations.SerializedName;

public class VehicleInformation {
    @SerializedName("brand")
    String brand;
    @SerializedName("model")
    String model;
    @SerializedName("fuelType")
    String fuelType;
}