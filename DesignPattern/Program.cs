using DesignPattern.BuilderPattern;
using DesignPattern.DependencyInjectionPattern;
using DesignPattern.FactoryMethodPattern;
using DesignPattern.Models;
using DesignPattern.RepositoryPattern;
using DesignPattern.StatePattern;
using DesignPattern.StrategyPattern;
using DesignPattern.UnitOfWorkPattern;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SINGLETON - (instancia única) está diseñado para restringir la creación de objetos pertenecientes a una clase o el valor de un tipo a un único objeto.
            //Su intención consiste en garantizar que una clase sólo tenga una instancia y proporcionar un punto de acceso global a ella.
            //No se encarga de la creación de objetos en sí, sino que se enfoca en la restricción en la creación de un objeto.
            var log = Singleton.Log.Instance;
            log.Save("d");

            //FACTORY METHOD - Es practicamente una fabrica creadora de objetos.

            //Es un patrón de diseño creacional que resuelve el problema de crear objetos de producto sin especificar sus clases concretas.
            //El patrón Factory Method define un método que debe utilizarse para crear objetos, en lugar de una llamada directa al constructor(operador new).
            //Las subclases pueden sobrescribir este método para cambiar las clases de los objetos que se crearán.
            SaleFactory storeSaleFactory = new StoreSaleFactory(10);
            SaleFactory internetSaleFactory = new InternetSaleFactory(2);

            ISale sale1 = storeSaleFactory.GetSale();
            sale1.Sell(15);

            ISale sale2 = internetSaleFactory.GetSale();
            sale2.Sell(15);

            //DEPENDENCY INJECTION - En principio trata sobre quitar la responsabilidad de una clase de crear objetos a partir de otras clases.
            //Además viene a resolver uno de los principios SOLID, el PRINCIPIO DE LA INVERSION DE LA DEPENDENCIA, que es practicamente que no se debe depender
            //de implementaciones, pero si de abstracciones. Es decir, tu clase no deberia depender de como crear las cosas y simplemente recibir las cosas ya hechas.
            var beer1 = new DependencyInjectionPattern.Beer("Pikantus", "Erdinger");
            var drinkWithBeer = new DrinkWithBeer(10, 1, beer1);
            drinkWithBeer.Build();

            using(var context = new DesignPatternsContext())
            {
                //REPOSITORY - Basicamente trata de ser un intermediario entre el manejo de la data y el framework o dominio.
                //En este caso utilizo Repository para hacer de conexion entre la aplicacion y entity framework.
                //Se implementa ademas GENERIC - que es hacer que una clase se comporte igual para distintas fuentes de modelo. En este caso, Repository, se 
                //utiliza para hacer el modelado de las clases Beer y Brand.

                var beerRepository = new Repository<Models.Beer>(context);
                var beer = new Models.Beer() { BeerId = 1, Name = "Corona", Style = "Pilsner" };
                beerRepository.Add(beer);
                beerRepository.Save();

                foreach (var b in beerRepository.Get())
                {
                    Console.WriteLine(b.Name);
                }

                var brandRepository = new Repository<Brand>(context);
                var brand = new Brand() { BrandId = 1, Name = "Fuller" };
                brandRepository.Add(brand);
                brandRepository.Save();

                foreach (var b in brandRepository.Get())
                {
                    Console.WriteLine(b.Name);
                }

                //UNIT OF WORK - Es una forma de agrupar Repositorios y lo que nos sugiere este patron es que si tenemos un conjunto de peticiones en la base de datos,
                //podemos agruparlas y enviarlas todas juntas, ahorrandonos solicitudes por cada interaccion.
                var unitOfWork = new UnitOfWork(context);

                var beers = unitOfWork.Beers;
                var beer2 = new Models.Beer()
                {
                    Name = "Fuller",
                    Style = "Porter"
                };
                beers.Add(beer2);

                var brands = unitOfWork.Brands;
                var brand2 = new Models.Brand()
                {
                    Name = "Fuller"
                };
                brands.Add(brand2);

                unitOfWork.Save();
            }

            //STRATEGY - es un patrón de diseño de comportamiento que convierte un grupo de comportamientos en objetos y los hace intercambiables dentro del objeto de contexto original.
            //El objeto original, llamado contexto, contiene una referencia a un objeto de estrategia y le delega la ejecución del comportamiento.
            //Para cambiar la forma en que el contexto realiza su trabajo, otros objetos pueden sustituir el objeto de estrategia actualmente vinculado, por otro.
            //En este patron, se estan haciendo dos practicas del principio SOLID, uno es el pricipio de responsabilidad unica, 
            //y otro principio es el de abierto cerrado, que nos dice que una clase deberia estar abierta a extension pero cerrada para modificacion.
            var context2 = new Context(new CarStrategy());
            context2.Run();
            context2.Strategy = new MotoStrategy();
            context2.Run();

            //BUILDER PATTERN - es un patrón de diseño creacional que permite construir objetos complejos paso a paso.
            //Resulta especialmente útil cuando debes crear un objeto con muchas opciones posibles de configuración.
            var builder = new PreparedAlcoholicDrinkConcretedBuilder();
            var barmanDirector = new BarmanDirector(builder);

            barmanDirector.PrepareMargerita();

            var preparedBuilder = builder.GetPreparedDrink();

            Console.WriteLine(preparedBuilder.Result);

            //STATE PATTERN - Es un patrón de diseño de comportamiento que permite a un objeto alterar su comportamiento
            //cuando su estado interno cambia. Parece como si el objeto cambiara su clase.
            var customerContext = new CustomerContext();
            Console.WriteLine(customerContext.GetState());
            customerContext.Request(100);
            Console.WriteLine(customerContext.GetState());

            customerContext.Request(50);
            Console.WriteLine(customerContext.GetState());

            customerContext.Request(100);
            Console.WriteLine(customerContext.GetState());

            customerContext.Request(50);
            Console.WriteLine(customerContext.GetState());

            customerContext.Request(50);
            Console.WriteLine(customerContext.GetState());
        }
    }
}
