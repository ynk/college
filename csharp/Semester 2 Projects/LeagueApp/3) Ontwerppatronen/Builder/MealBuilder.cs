using Builder.Models;

namespace Builder
{
    public class MealBuilder
    {
        public Meal PrepareChickenBurger()
        {
            var meal = new Meal();
            meal.AddItem(new Water());
            meal.AddItem(new ChickenBurger());

            return meal;
        }

        public Meal PrepareBurgerMeal()
        {
            var meal = new Meal();
            meal.AddItem(new Cola());
            meal.AddItem(new HamBurger());

            return meal;
        }
    }
}