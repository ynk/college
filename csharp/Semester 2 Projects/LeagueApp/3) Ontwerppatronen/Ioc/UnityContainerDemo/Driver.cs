using System;
using System.Collections.Generic;
using System.Text;
using UnityContainerDemo.Interfaces;

namespace UnityContainerDemo
{
    class Driver
    {

        private ICar _car = null;
        private ICarKey _key = null;
        private string _name = string.Empty;


        public Driver(ICar car)
        {
            _car = car;
        }

        public void RunCar()
        {
            Console.WriteLine($"Running - {_car.Run()} Mile ");
        }
    }
}
