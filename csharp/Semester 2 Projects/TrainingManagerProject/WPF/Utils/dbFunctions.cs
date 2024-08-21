using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using DataLayer;
using DomainLibrary.Domain;
using WPF.Utils;

namespace WPF
{
    public static class dbFunctions
    {
        public static Report GenerateMonthReport(int month, int year)
        {
            TrainingManager tm = new TrainingManager(new UnitOfWork(new TrainingContext()));
            return tm.GenerateMonthlyTrainingsReport(year, month);
        }

        public static List<RapportData> GetAllRunningData()
        {
            List<RapportData> date = new List<RapportData>();
            TrainingManager tm = new TrainingManager(new UnitOfWork(new TrainingContext()));
            tm.GetAllRunningSessions().ForEach(x => date.Add(new RapportData(x)));
            return date;
        }
        //    dbFunctions.DeleteRun(ctxData.Id);

        public static void DeleteRun(int id)
        {
            TrainingManager tm = new TrainingManager(new UnitOfWork(new TrainingContext()));
            tm.RemoveTrainings(new List<int> { }, new List<int> { id });
        }
        public static void DeleteCycle(int id)
        {
            TrainingManager tm = new TrainingManager(new UnitOfWork(new TrainingContext()));
            tm.RemoveTrainings(new List<int> { }, new List<int>
            {

                //todo fix
            });
        }
        public static void AddCycling(DateTime when,  TimeSpan time, TrainingType tp, BikeType bt, string comments, float avgSpeed, int avgWatt, float distance)
        {
            TrainingManager tm = new TrainingManager(new UnitOfWork(new TrainingContext()));
            tm.AddCyclingTraining(when, distance, time, avgSpeed, avgWatt, tp, comments, bt);
            MessageBox.Show("AddCycling Toegevoegd!");
        }
        public static void AddRunning(DateTime when, TimeSpan time, TrainingType tp, string comments, float avgSpeed, float distance)
        {
            TrainingManager tm = new TrainingManager(new UnitOfWork(new TrainingContext()));
            tm.AddRunningTraining(when, Convert.ToInt32(distance), time, avgSpeed, tp, comments);
            MessageBox.Show("AddRunning Toegevoegd!");
        }
    }
}
