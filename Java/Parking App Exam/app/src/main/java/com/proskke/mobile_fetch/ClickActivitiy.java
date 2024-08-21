package com.proskke.mobile_fetch;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.util.Locale;


public class ClickActivitiy extends AppCompatActivity {
    TextView ParkingNaam,ParkingAdres,ParkingContactInfo,ParkingMaxCap,ParkingNaamBar;
    Button ParkingAvaible;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.second);

        ActionBar actionBar = getSupportActionBar();

        ParkingAvaible = findViewById(R.id.ParkingIconView);
        ParkingNaamBar = findViewById(R.id.ParkingNaam);
        ParkingNaam = findViewById(R.id.Parking_NaamInInfo);
        ParkingAdres = findViewById(R.id.Parking_Address);
        ParkingContactInfo = findViewById(R.id.Parking_contactInformatie);
        ParkingMaxCap = findViewById(R.id.Parking_TotaalCap);

        Intent intent = getIntent();
        int color = intent.getIntExtra("ColorTime",0);
        String iAvaibleParkingSpots = intent.getStringExtra("parking_aviable_spots");
        String iParkingNaam = intent.getStringExtra("Parking_naam");
        String iParkingDescription = intent.getStringExtra("Description");
        String iParkingAddress = intent.getStringExtra("Parking_adres");
        String iParkingContactInfo = intent.getStringExtra("Parking_contact_informatie");
        String iParkingMaxCap = intent.getStringExtra("parking_totale_capecitieit");
        ParkingAvaible.setBackgroundColor(color);
        Log.i("ynk:",String.valueOf(color));
        ParkingAvaible.setText(iAvaibleParkingSpots);
        ParkingNaamBar.setText(iParkingNaam);
        ParkingNaam.setText(iParkingDescription);
        ParkingAdres.setText(iParkingAddress);
        ParkingContactInfo.setText(iParkingContactInfo);
        ParkingMaxCap.setText(iParkingMaxCap);

    }

    public void OpenGoogleMaps(View v){
        String ParkingAdres = ((TextView)v).getText().toString();
        String uri = String.format(Locale.ENGLISH, "geo:0,0?q="+ParkingAdres);
        Log.i("ynk:",uri);
        Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse(uri));
        startActivity(intent);
    }



}
