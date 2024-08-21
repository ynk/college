package com.proskke.toast;

import androidx.appcompat.app.AppCompatActivity;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);



        SharedPreferences pref = getApplicationContext().getSharedPreferences("YannickAPP", 0); // 0 - for private mode
        SharedPreferences.Editor editor = pref.edit();

        boolean hasBeenShown = pref.getBoolean("toast_thing",false);
        if(!hasBeenShown){
            //Show
            Toast toast = Toast.makeText(getApplicationContext(),
                    "Welcome!",
                    Toast.LENGTH_SHORT);

            toast.show();
            editor.putBoolean("toast_thing",true).apply();
        }


    }
}
