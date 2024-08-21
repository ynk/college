package com.proskke.mobile_fetch;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class MainActivity extends AppCompatActivity {
    RecyclerView recyclerView;


    public boolean runResume = false;
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        fetchData();
    }
    @Override

    protected void onResume() {
        super.onResume();
        if(runResume){
            fetchData();
            Log.i("Parking:","Refreshed the data");
            Toast.makeText(getApplicationContext(), "Refresh",Toast.LENGTH_LONG).show();
        }
        runResume = true;

    }

    static final String BASE_URL = "https://datatank.stad.gent/4/";
    public void fetchData() {
        Gson gson = new GsonBuilder().create();
        Retrofit retrofit = new Retrofit.Builder().baseUrl(BASE_URL).addConverterFactory(GsonConverterFactory.create(gson)).build();
        ApiService apiService = retrofit.create(ApiService.class);
        Call<List<Parking>> call = apiService.getAllParkings();

        call.enqueue(new Callback<List<Parking>>() {
            @Override
            public void onResponse(Call<List<Parking>> call, Response<List<Parking>> response) {
                if(response.isSuccessful()) {
                    // 1ste auto laden
                    List<Parking> parkingList = response.body();
                    ParseTheList(parkingList);
                    Log.i("Parking:","onResponse");
                }
            }

            @Override
            public void onFailure(Call<List<Parking>> call, Throwable t) {

            }
        });
    }

    public void ParseTheList(List<Parking> raw){

        RecyclerView recyclerView ;
        RecyclerView.Adapter mAdapter;
        RecyclerView.LayoutManager layoutManager;
        recyclerView = findViewById(R.id.my_recycler_view);
        recyclerView.setHasFixedSize(true);
        layoutManager = new LinearLayoutManager(this);
        recyclerView.setLayoutManager(layoutManager);
        DataAdapter dataAdapter = new DataAdapter(raw, this);
        recyclerView.setAdapter(dataAdapter);

    }
}