using Cocktails.Data.Data;

namespace Cocktails.Data
{
    internal interface ICocktailDataProvider
    {
        public IEnumerable<CocktailData> GetCockTailsBySearchKeyWord(string cocktailSearchKeyword);
    }
}
