package com.proskke.mobile_fetch;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.util.Pools;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class MainActivity extends AppCompatActivity {
    RecyclerView recyclerView;

    static final String BASE_URL = "https://datatank.stad.gent/4/";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);



        fetchData();
    }

    private void fetchData() {
        Gson gson = new GsonBuilder().create();
        Retrofit retrofit = new Retrofit.Builder().baseUrl(BASE_URL).addConverterFactory(GsonConverterFactory.create(gson)).build();
        ApiService apiService = retrofit.create(ApiService.class);
        Call<List<PoolCar>> call = apiService.getPoolCars();

        call.enqueue(new Callback<List<PoolCar>>() {
            @Override
            public void onResponse(Call<List<PoolCar>> call, Response<List<PoolCar>> response) {
                if(response.isSuccessful()){
                    // 1ste auto laden
                    List<PoolCar> poolCarList = response.body();
                    ParseTheList(poolCarList);
                }
            }

            @Override
            public void onFailure(Call<List<PoolCar>> call, Throwable t) {

            }
        });
    }

    public void ParseTheList(List<PoolCar> list){


        ArrayList<VehicleInformation> autoInfo = new ArrayList<>();
        VehicleInformation auto;
        for(int i = 0; i < list.size(); i++)
        {
            auto = new VehicleInformation();
            auto.model = list.get(i).vehicleInformation.model;
            auto.brand = list.get(i).vehicleInformation.brand;
            auto.transmissionType = list.get(i).vehicleInformation.transmissionType;
            autoInfo.add(auto);
        }
         RecyclerView recyclerView ;
         RecyclerView.Adapter mAdapter;
         RecyclerView.LayoutManager layoutManager;
        recyclerView = findViewById(R.id.my_recycler_view);
        recyclerView.setHasFixedSize(true);
        layoutManager = new LinearLayoutManager(this);
        recyclerView.setLayoutManager(layoutManager);
        DataAdapter dataAdapter = new DataAdapter(autoInfo, this);
        recyclerView.setAdapter(dataAdapter);

    }
}