package com.proskke.mobile_fetch;

import com.google.gson.annotations.SerializedName;

public class VehicleInformation {
    @SerializedName("brand")
    String brand;
    @SerializedName("model")
    String model;
    @SerializedName("fuelType")
    String fuelType;
    @SerializedName("transmissionType")
    String transmissionType;

    public String getTransmissionType() {
        return transmissionType;
    }

    public void setTransmissionType(String transmissionType) {
        this.transmissionType = transmissionType;
    }

    public String getBrand() {
        return brand;
    }

    public void setBrand(String brand) {
        this.brand = brand;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public String getFuelType() {
        return fuelType;
    }

    public void setFuelType(String fuelType) {
        this.fuelType = fuelType;
    }



}