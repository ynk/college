package com.proskke.mobile_fetch;

import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

public class DataAdapter extends RecyclerView.Adapter<DataAdapter.ViewHolder> {
    List<Parking> parking;
    Context context;

    public DataAdapter(List<Parking> autos, Context context) {
        this.parking = autos;
        this.context = context;
    }


    @Override
    public DataAdapter.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        View view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.rec_row, viewGroup, false);
        return new ViewHolder(view);
    }
    public int GetTheColor(int percentage){
        Log.i("GetTheColor:",String.valueOf(percentage));
        if(percentage < 5){
           return (0xFFFF0000);
        }
        else if(percentage > 5 && percentage < 25){
            return (0xFFF57C00);
        }else if(percentage > 25 ){
            return (0xFF558A31);
        }

        return 0;
    }
    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
        final String ParkingNaam = parking.get(position).getName();
        final String ParkingDescription = parking.get(position).getName();
        final String ParkingAdress = parking.get(position).getAddress();
        final String ParkingContactInfo = parking.get(position).getContactInfo();
        final int ParkingPlaatsen = parking.get(position).parkingStatus.getAvailableCapacity();
        final int MaxCap = (parking.get(position).parkingStatus.getTotalCapacity());
        int percentage = ((ParkingPlaatsen * 100)/ MaxCap);
        int Color = GetTheColor(percentage);




        holder.ParkingButton.setBackgroundColor(Color);
        holder.vParkingNaam.setText(ParkingNaam);
        holder.ParkingButton.setText(String.valueOf(ParkingPlaatsen));

        holder.setItemClickListener(new ItemClickListener() {
            @Override
            public void onItemClickListener(View v, int position) {

                Intent intent = new Intent(context, ClickActivitiy.class);

                intent.putExtra("ColorTime",Color);
                intent.putExtra("parking_aviable_spots",String.valueOf(ParkingPlaatsen));
                intent.putExtra("Parking_id", position);
                intent.putExtra("Parking_naam", ParkingNaam);
                intent.putExtra("Description", ParkingDescription);
                intent.putExtra("Parking_adres", ParkingAdress);
                intent.putExtra("Parking_contact_informatie", ParkingContactInfo);
                intent.putExtra("parking_totale_capecitieit", MaxCap);
                context.startActivity(intent);


            }
        });
    }









    @Override
    public int getItemCount() {
        return parking.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        private TextView vParkingNaam,vParkignPlaatsen;
        Button ParkingButton;
        ItemClickListener itemClickListener;
        ViewHolder(View view) {
            super(view);
            vParkingNaam = (TextView) view.findViewById(R.id.ParkingNaam);

            ParkingButton = (Button) view.findViewById(R.id.ParkingButton);
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