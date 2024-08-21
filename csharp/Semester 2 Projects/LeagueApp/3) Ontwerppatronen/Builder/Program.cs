using System;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new MealBuilder();
            var burgerMeal = builder.PrepareBurgerMeal();
            Console.WriteLine("curgermeal:");
            burgerMeal.ShowItems();
            Console.WriteLine($"Costs: {burgerMeal.GetCosts().ToString("c")}");
            var chickenMeal = builder.PrepareChickenBurger();
            Console.WriteLine("chickenmeal:");
            chickenMeal.ShowItems();
            Console.WriteLine($"costs: {chickenMeal.GetCosts().ToString("c")}");
            Console.ReadLine();
        }
    }
}
