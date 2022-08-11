using DesignPatternAsp.Models.ViewModels;
using DesignPatterns.Repository;
using DessignPatterns.Models.Data;
using System;

namespace DesignPatternAsp.Strategies
{
    public class BeerStrategy : IBeerStrategy
    {
        public void Add(FormBeerViewModel beerVM, IUnitOfWork unitOfWork)
        {
            var beer = new Beer();
            beer.Name = beerVM.Name;
            beer.Style = beerVM.Style;
            beer.BrandId = (Guid)beerVM.BrandId;

            unitOfWork.Beers.Add(beer);
            unitOfWork.Save();

        }
    }
}
