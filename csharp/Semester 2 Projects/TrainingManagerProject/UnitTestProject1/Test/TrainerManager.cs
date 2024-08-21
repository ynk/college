using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DomainLibrary.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class TrainerManager
    {
        private TrainingManager m;
        private Exception ExpectedException;

        [TestInitialize]
        public void Initialize()
        {
            m = new TrainingManager(new UnitOfWork(new TrainingContextTest()));
            m.AddCyclingTraining(new DateTime(2020, 4, 21, 16, 00, 00), 40, new TimeSpan(1, 20, 00), 30, null, TrainingType.Endurance, null, BikeType.RacingBike);
            m.AddCyclingTraining(new DateTime(2020, 4, 18, 18, 00, 00), 40, new TimeSpan(1, 42, 00), null, null, TrainingType.Recuperation, null, BikeType.RacingBike);
            m.AddCyclingTraining(new DateTime(2020, 4, 19, 16, 45, 00), null, new TimeSpan(1, 0, 00), null, 219, TrainingType.Interval, "5x5 min 270", BikeType.IndoorBike);
            m.AddRunningTraining(new DateTime(2020, 4, 17, 12, 30, 00), 5000, new TimeSpan(0, 27, 17), null, TrainingType.Endurance, null);
            m.AddRunningTraining(new DateTime(2020, 4, 19, 12, 30, 00), 5000, new TimeSpan(0, 25, 48), null, TrainingType.Endurance, null);
            m.AddRunningTraining(new DateTime(2020, 3, 17, 11, 0, 00), 5000, new TimeSpan(0, 28, 10), null, TrainingType.Interval, "3x700m");
            m.AddRunningTraining(new DateTime(2020, 3, 17, 11, 0, 00), 8000, new TimeSpan(0, 42, 10), null, TrainingType.Endurance, null);
        }

        public void GenerateMonthlyCyclingReportSuccesful()
        {
            //Arrange
            TrainingManager m = new TrainingManager(new UnitOfWork(new TrainingContextTest()));
            m.AddCyclingTraining(DateTime.Now, 450, new TimeSpan(11, 25, 13), 10, 15, TrainingType.Interval,
                "Unit test", BikeType.MountainBike);
            m.AddCyclingTraining(DateTime.Now, 450, new TimeSpan(11, 25, 13), 10, 15, TrainingType.Interval,
                "Unit test", BikeType.MountainBike);
            int year = 2020;
            int month = 5;
            var report = m.GenerateMonthlyCyclingReport(year, month);
            report.Should();

        }

    }
}
