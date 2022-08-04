using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.DependencyInjectionPattern
{
    internal class DrinkWithBeer
    {
        private Beer _beer;
        private decimal _water;
        private decimal _suger;
        
        public DrinkWithBeer(decimal water, decimal sugar, Beer beer)
        {
            _water = water;
            _suger = sugar;
            _beer = beer;
        }
        public void Build()
        {
            Console.WriteLine($"Preparamos bebida que tiene agua {_water}" +
                $" azúcar {_suger} y cerveza {_beer.Name}");
        }
    }
}
