using System;
using System.Collections.Generic;
using System.Linq;
using Builder.Interfaces;

namespace Builder.Models
{
    public class Meal
    {
        private List<IItem> items = new List<IItem>();

        public void AddItem(IItem itemToAdd)
        {
            items.Add(itemToAdd);
        }
        public float GetCosts()
        {
            return items.Sum(i => i.Price);
        }
        public void ShowItems()
        {
            foreach (var item in items)
            {
                Console.WriteLine($"Item: {item.Name} packing: {item.Packing}, Price: {item.Price}");
            }
        }
    }
}