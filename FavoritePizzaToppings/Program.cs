using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FavoritePizzaToppings
{
    class Program
    {
        static void Main(string[] args)
        {
            var pizzas = JsonConvert.DeserializeObject<List<Pizza>>(File.ReadAllText(@"./pizzas.json"));

            var toppingLists = pizzas.Select(p => string.Join(",", p.Toppings.OrderBy(t => t)));

            //Distinct is a Linq method that gives us unique topping combinations
            var distinctToppingCombinations = toppingLists.Distinct();

            //int will be how many times this combination appears
            var countOfCombination = new Dictionary<string, int>();

            foreach (var combination in toppingLists)
            {
                if (!
                    countOfCombination.ContainsKey(combination))
                {
                    countOfCombination.Add(combination, 1);
                }
                else
                {
                    countOfCombination[combination] += 1;
                }
            }
            var mostOrdered = countOfCombination.OrderByDescending(item => item.Value).Take(20);

            foreach (var (combination, count) in mostOrdered)
            {
                Console.WriteLine($"The topping combination of {combination} was ordered {count} times.");
                Console.ReadLine();
            }
        }
    }
}
