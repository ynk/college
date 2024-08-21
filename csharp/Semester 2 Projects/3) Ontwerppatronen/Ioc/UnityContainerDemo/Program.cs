using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;
using UnityContainerDemo.Interfaces;
using UnityContainerDemo.Manufactureres;

namespace UnityContainerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer()
                .RegisterType<ICar, BMW>(new HierarchicalLifetimeManager());

            var childContainer = container.CreateChildContainer();

            Driver driver1 = container.Resolve<Driver>();
            driver1.RunCar();

            Driver driver2 = container.Resolve<Driver>();
            driver2.RunCar();

            Driver driver3 = childContainer.Resolve<Driver>();
            driver3.RunCar();

            Driver driver4 = childContainer.Resolve<Driver>();
            driver4.RunCar();
        }

    }
}
