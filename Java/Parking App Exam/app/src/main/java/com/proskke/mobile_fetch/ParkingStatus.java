package com.proskke.mobile_fetch;

import com.google.gson.annotations.SerializedName;

public class ParkingStatus {
    @SerializedName("totalCapacity")
    int totalCapacity;
    @SerializedName("availableCapacity")
    int availableCapacity;

    public int getTotalCapacity() {
        return totalCapacity;
    }



    public int getAvailableCapacity() {
        return availableCapacity;
    }


    public ParkingStatus(int totalCapacity, int availableCapacity) {
        this.totalCapacity = totalCapacity;
        this.availableCapacity = availableCapacity;
    }
}
