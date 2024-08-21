package com.proskke.mobile_fetch;

import com.google.gson.annotations.SerializedName;

public class PoolCar {


    @SerializedName("vehicleInformation")
    VehicleInformation vehicleInformation;

    public PoolCar(VehicleInformation vehicleInformation) {
        this.vehicleInformation = vehicleInformation;
    }
}
