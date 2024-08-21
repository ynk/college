package com.proskke.mobile_fetch;

import com.google.gson.annotations.SerializedName;

public class Parking {

    @SerializedName("description")
    String description;
    @SerializedName("name")

    String name;
    @SerializedName("address")
    String address;
    @SerializedName("latitude")
    String latitude;
    @SerializedName("longitude")
    String longitude;
    @SerializedName("contactInfo")
    String contactInfo;
    @SerializedName("parkingStatus")
    ParkingStatus parkingStatus;

    public Parking(String description, String name, String address, String latitude, String longitude, String contactInfo, ParkingStatus parkingStatus) {
        this.description = description;
        this.name = name;
        this.address = address;
        this.latitude = latitude;
        this.longitude = longitude;
        this.contactInfo = contactInfo;
        this.parkingStatus = parkingStatus;
    }




    public String getName() {
        return name;
    }

    public String getAddress() {
        return address;
    }

    public String getContactInfo() {
        return contactInfo;
    }


}
