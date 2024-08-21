package com.proskke.mobile_fetch;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.TextView;

public class ClickActivitiy extends AppCompatActivity {
    TextView autoNaam,autoModel,autoFuelType,autoTransType;
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.second);

        ActionBar actionBar = getSupportActionBar();
        autoNaam = findViewById(R.id.click_autoNaam);
        autoModel = findViewById(R.id.click_autoModel);
        autoFuelType = findViewById(R.id.click_autoFuelType);
        autoTransType = findViewById(R.id.click_autoTranstype);

        Intent intent = getIntent();

        String mAutoNaam = intent.getStringExtra("autoNaam");
        String mautoModel = intent.getStringExtra("autoModel");
        String mautoTransType = intent.getStringExtra("autoFuelType");
        String mautoFuelType = intent.getStringExtra("autoTransType");

        actionBar.setTitle(mautoModel + "  | " + mAutoNaam); // vind dit iets beter
        autoNaam.setText(mAutoNaam);
        autoModel.setText("Brand: " + mautoModel);
        autoFuelType.setText("Fuel type: " + mautoFuelType);
        autoTransType.setText("Transmission: " + mautoTransType);

    }
}
