package com.proskke.mobile1_jsonfetch;

import android.os.AsyncTask;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class Controller extends AsyncTask<Void, Void, String> {
    private String carDetails;

    @Override
    protected String doInBackground(Void... voids) {
        try {
            URL URL = new URL("https://datatank.stad.gent/4/mobiliteit/deelwagenspartago.json");
            HttpURLConnection httpURLConnection = (HttpURLConnection) URL.openConnection();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(httpURLConnection.getInputStream()));
            String line="";
            StringBuilder builder = new StringBuilder();
            while((line=bufferedReader.readLine())!=null)
            {
                builder.append(line);
            }
            carDetails= builder.toString();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return carDetails; // Alle autos
    }

    @Override
    protected void onPostExecute(String carDetails) {
        super.onPostExecute(carDetails);
        try {
            JSONArray JsonArr = new JSONArray(carDetails); //maakt json array in
            JSONObject object = JsonArr.getJSONObject(0);// 1ste auto in array
            JSONObject vehicleInfo = object.getJSONObject("vehicleInformation");
            PoolCar newCar = new PoolCar();
            VehicleInformation car = new VehicleInformation();
            car.brand = vehicleInfo.getString("brand");
            car.model = vehicleInfo.getString("model");
            car.fuelType = vehicleInfo.getString("fuelType");

            MainActivity.text_car.setText(newCar.ShowCar(car));
        } catch (JSONException x) {
            x.printStackTrace();
        }

    }
}
