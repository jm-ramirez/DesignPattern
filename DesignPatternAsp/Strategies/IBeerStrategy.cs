using DesignPatternAsp.Models.ViewModels;
using DesignPatterns.Repository;

namespace DesignPatternAsp.Strategies
{
    public interface IBeerStrategy
    {
        public void Add(FormBeerViewModel beerVM, IUnitOfWork unitOfWork);
    }
}
