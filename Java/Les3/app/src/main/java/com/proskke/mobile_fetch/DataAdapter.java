package com.proskke.mobile_fetch;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;
import java.util.List;

public class DataAdapter extends RecyclerView.Adapter<DataAdapter.ViewHolder> {
    ArrayList<VehicleInformation> autos;
    Context context;

    public DataAdapter(ArrayList<VehicleInformation> autos, Context context) {
        this.autos = autos;
        this.context = context;
    }

    @Override
    public DataAdapter.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        View view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.rec_row, viewGroup, false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(DataAdapter.ViewHolder viewHolder, final int i) {

        final String car_name=autos.get(i).getModel();
        final String car_desc=autos.get(i).getBrand();
        final String auto_foto=autos.get(i).getTransmissionType();
        Log.i("Logger: ", auto_foto);
        viewHolder.autoNaam.setText(car_name);
        viewHolder.autoModel.setText(car_desc);
        if(auto_foto.equals("automatic")){
            viewHolder.mFoto.setImageResource(R.drawable.ic_automatic);
        }else{
            viewHolder.mFoto.setImageResource(R.drawable.ic_manual);
        }


    }

    @Override
    public int getItemCount() {
        return autos.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder{
        private TextView autoNaam,autoModel;
        ImageView mFoto;

        public ViewHolder(View view) {
            super(view);
            mFoto = (ImageView)view.findViewById(R.id.Auto_foto);
            autoNaam = (TextView)view.findViewById(R.id.autoModel);
            autoModel = (TextView)view.findViewById(R.id.autoNaam);
        }
    }

}