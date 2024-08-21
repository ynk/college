using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DomainLibrary.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace UnitTestProject1.Test
{
    class TrainingModel
    {
        [TestClass]
        public class TrainingManagerTests
        {


            [TestMethod]
            public void AddCyclingTrainingWithInvalidDateInTheFuture()
            {
                DateTime future = new DateTime(2100, 1, 30);
                float distance = 400;
                TimeSpan time = new TimeSpan(0, 5, 0);
                int averageWatt = 350;
                float averageSpeed = 35;
                TrainingManager t = new TrainingManager(new UnitOfWork(new TrainingContextTest(false)));
                Action act = () => t.AddCyclingTraining(future, distance, time, averageSpeed, averageWatt, TrainingType.Interval, "Onze Unit Test", BikeType.MountainBike);
                act.Should().Throw<DomainException>().WithMessage("Training is in the future");
            }
            [TestMethod]
            public void TestRunningSessionConstructorShouldRunSucesfull()
            {
                DateTime now = DateTime.Now;
                int distance = 3500;
                TimeSpan time = new TimeSpan(0, 25, 30);
                float? averageSpeed = null;
                RunningSession rs = new RunningSession(now, distance, time, averageSpeed, TrainingType.Endurance, "Onze Unit Test");
                rs.AverageSpeed.Should().Be(7.0588236F);
            }

            [TestMethod]
            public void CreatingCityCyclingSessionShouldRunSucesfull()
            {
                DateTime now = DateTime.Now;
                float distance = 350;
                TimeSpan time = new TimeSpan(12, 25, 30);
                float? avg = null;
                int averageWatt = 400;
                CyclingSession cs = new CyclingSession(now, distance, time, avg, averageWatt, TrainingType.Recuperation, "Onze UnitTest", BikeType.CityBike);
                cs.AverageSpeed.Should().Be(28.169014F);
            }

            [TestMethod]
            public void AddCyclingTrainingInvalidWattage()
            {
                //Arrange
                TrainingManager tm = new TrainingManager(new UnitOfWork(new TrainingContext("Test")));
                int wattage = 1000;
                //Act
                Action result = () =>
                {
                    tm.AddCyclingTraining(DateTime.Now, 1, new TimeSpan(12, 25, 30), 10, wattage, TrainingType.Interval,
                        "Onze Unit test", BikeType.MountainBike);
                };
                //Assert
                result.Should().Throw<DomainException>().WithMessage("Average watt invalid value");
            }

        }
    }

}
