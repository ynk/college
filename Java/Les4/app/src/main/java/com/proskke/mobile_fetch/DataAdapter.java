package com.proskke.mobile_fetch;

import android.content.Context;
import android.content.Intent;
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

        viewHolder.setItemClickListener(new ItemClickListener() {
            @Override
            public void onItemClickListener(View v, int position) {

                final String car_name=autos.get(position).getModel();
                final String car_model=autos.get(position).getBrand();
                final String car_transtype =autos.get(position).getTransmissionType();
                final String car_fueltype = autos.get(position).getFuelType();
                Intent intent = new Intent(context, ClickActivitiy.class);
                intent.putExtra("autoNaam", car_name);
                intent.putExtra("autoModel", car_model);
                intent.putExtra("autoTransType", car_transtype);
                intent.putExtra("autoFuelType", car_fueltype);
                context.startActivity(intent);

            }
        });

    }

    @Override
    public int getItemCount() {
        return autos.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        private TextView autoNaam,autoModel;
        ImageView mFoto;
        ItemClickListener itemClickListener;
        ViewHolder(View view) {
            super(view);
            mFoto = (ImageView)view.findViewById(R.id.Auto_foto);
            autoNaam = (TextView)view.findViewById(R.id.autoModel);
            autoModel = (TextView)view.findViewById(R.id.autoNaam);

            itemView.setOnClickListener(this);
        }
        @Override
        public void onClick(View v){
            this.itemClickListener.onItemClickListener(v,getLayoutPosition());
        }
        public void setItemClickListener(ItemClickListener ic){

            this.itemClickListener = ic;
        }
    }

}