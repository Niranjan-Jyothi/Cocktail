using Cocktails.Data;
using Cocktails.Data.Data;
using Cocktails.Services.Implementation.Convertors;
using Cocktails.Services.Services;
using Cocktail = Cocktails.Services.Models.Cocktail;

namespace Cocktails.Services.Implementation.Services
{
    internal sealed class CocktailService : ICocktailService
    {
        private readonly ICocktailDataProvider _cocktailDataProvider;

        public CocktailService(ICocktailDataProvider cocktailDataProvider)
        {
            _cocktailDataProvider = cocktailDataProvider ?? throw new ArgumentNullException(nameof(cocktailDataProvider));
        }

        public IEnumerable<Cocktail> GetCocktails(string cocktailSearchKeyword, int itemsToBeReturned)
        {
            if (string.IsNullOrWhiteSpace(cocktailSearchKeyword))
            {
                throw new ArgumentException(nameof(cocktailSearchKeyword));
            }

            if (itemsToBeReturned <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(itemsToBeReturned));
            }

            var cocktailsBySearchTerm = _cocktailDataProvider.GetCockTailsBySearchKeyWord(cocktailSearchKeyword);

            return cocktailsBySearchTerm.Take(itemsToBeReturned).ToModel();
        }
    }
}
