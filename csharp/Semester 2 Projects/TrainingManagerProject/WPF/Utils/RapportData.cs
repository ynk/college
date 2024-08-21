using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using DomainLibrary.Domain;

namespace WPF.Utils
{
    public class RapportData
    {
        public int Id { get; set; }
        public DateTime colum1 { get; set; }
        public string colum2 { get; set; }
        public string colum3 { get; set; }
        public string colum4 { get; set; }
        public string colum5 { get; set; }
        public string colum6 { get; set; }
        public string colum7 { get; set; }
        public string colum8 { get; set; }
        public RapportData(CyclingSession cs)
        {
            Id = cs.Id; 
            colum1 = cs.When;
            colum2 = ctxKm(cs.Distance);
            colum3 = ctxTimeDue(cs.Time);
            colum4 = Math.Round((decimal)cs.AverageSpeed) + " km/h";
            colum5 = ctxWatt(cs.AverageWatt);
            colum6 = ctxBiketype(cs.BikeType);
            colum7 = cs.Comments;
            colum8 = cs.BikeType.ToString();
        }
        public RapportData(RunningSession rs)
        {
            Id = rs.Id;
            colum1 = rs.When;
            colum2 = cxtM(rs.Distance);
            colum3 = ctxTimeDue(rs.Time);
           // MessageBox.Show(rs.AverageSpeed.ToString());
            colum4 = Math.Round((decimal)rs.AverageSpeed) + " km/h";
            colum5 = rs.TrainingType.ToString();
            colum6 = "";
            colum7 = rs.Comments;
        }
                public string ctxBiketype(BikeType bike)
        {

            switch (bike)
            {
                case BikeType.CityBike:
                    return "City bike";
                case BikeType.IndoorBike:
                    return "Indoor bike";
                case BikeType.MountainBike:
                    return "Mountain bike";
                case BikeType.RacingBike:
                    return "Racing bike";
                default:
                    return "Unknown bike";
            }
        }
        public string ctxKm(float? a)
        {
            if (a != null)
                return a + " km";
            else
                return "";
        }
        public string cxtM(int a)
        {
            return a + " m";
        }

        public string ctxWatt(int? a)
        {
            if (a != null)
                return a + " Watt";
            else
                return "";
        }
        public string ctxTimeDue(TimeSpan a)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(a.ToString(@"hh\:mm\:ss"));
            return sb.ToString();
        }

    }
}
