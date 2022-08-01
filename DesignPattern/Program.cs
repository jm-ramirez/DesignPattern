﻿using DesignPattern.FactoryMethodPattern;
using System;

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

        }
    }
}
