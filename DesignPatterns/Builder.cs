using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    //Builder Interface
    public interface IFood 
    {
        string Name();

        Double Price();
    }

    //Concrete Builder
    public class Chicken : IFood
    {
        public Double Price()
        {
            return 50.5;
        }

        public string Name()
        {
            return "Chicken Burger";
        }

    }

    public class Veg : IFood
    {
        public Double Price()
        {
            return 25.0;
        }

        public string Name()
        {
            return "Veg Burger";
        }

    }

    public class Drink : IFood
    {
        public Double Price()
        {
            return 12;
        }

        public string Name()
        {
            return "Pepsi";
        }

    }

    //Product
    public class Meal
    {
        private List<IFood> items = new List<IFood>();

        public void AddItem(IFood item)
        {
            items.Add(item);
        }

        public Double TotalCost()
        {
            Double cost = 0.0f;

            foreach (IFood item in items)
            {
                cost += item.Price();
            }
            return cost;
        }

        public void ShowItems()
        {
            foreach (IFood item in items)
            {
                Console.Write("Item : " + item.Name());
                Console.WriteLine(", Price : " + item.Price());
            }
        }
    }

    //Director
    public class MealBuilder
    {
        public Meal VegMeal()
        {
            Meal meal = new Meal();
            meal.AddItem(new Veg());
            meal.AddItem(new Drink());
            return meal;
        }

        public Meal NonVegMeal()
        {
            Meal meal = new Meal();
            meal.AddItem(new Chicken());
            meal.AddItem(new Drink());
            return meal;
        }
    }

    public class Client
    {
        public static void Demo()
        {
            MealBuilder mealBuilder = new MealBuilder();

            Meal vegMeal = mealBuilder.VegMeal();
            Console.WriteLine("Veg Meal");
            vegMeal.ShowItems();
            Console.WriteLine("Total Cost: " + vegMeal.TotalCost());

            Meal nonVegMeal = mealBuilder.NonVegMeal();
            Console.WriteLine("\n\nNon-Veg Meal");
            nonVegMeal.ShowItems();
            Console.WriteLine("Total Cost: " + nonVegMeal.TotalCost());

            Console.ReadLine();
        }
    }
}
