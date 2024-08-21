package com.proskke.mobile1_jsonfetch;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.TextView;

import com.google.gson.annotations.SerializedName;

public class MainActivity extends AppCompatActivity {
    public static TextView text_car;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        text_car = (TextView) findViewById(R.id.text_car);
        Controller api = new Controller();
        api.execute();


    }


}
