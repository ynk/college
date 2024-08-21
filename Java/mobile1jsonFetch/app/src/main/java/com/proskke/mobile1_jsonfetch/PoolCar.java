package com.proskke.mobile1_jsonfetch;

import com.google.gson.annotations.SerializedName;

public class PoolCar {
    @SerializedName("vehicleInformation")
    VehicleInformation vehicleInformation;

    public String ShowCar(VehicleInformation information)
    {
        String brand = information.brand;
        String model = information.model;
        String fuelType = information.fuelType;
        String result = brand + "\n" + model + "\n" + fuelType + "\n";
        return result;
    }

}