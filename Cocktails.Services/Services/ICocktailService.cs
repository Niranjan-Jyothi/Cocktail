using Cocktails.Services.Models;

namespace Cocktails.Services.Services
{
    public interface ICocktailService
    {
        public IEnumerable<Cocktail> GetCocktails(string cocktailSearchKeyword, int itemsToBeReturned);
    }
}
