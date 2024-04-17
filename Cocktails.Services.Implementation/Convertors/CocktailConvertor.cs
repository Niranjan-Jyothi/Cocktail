using Cocktails.Data.Data;
using Cocktails.Services.Models;

namespace Cocktails.Services.Implementation.Convertors
{
    internal static class CocktailConvertor
    {
        public static IEnumerable<Cocktail> ToModel(this IEnumerable<CocktailData> dataEntities)
        {
            return dataEntities.Select(dataEntity => new Cocktail()
            {
                Id = int.Parse(dataEntity.idDrink),
                Name = dataEntity.strDrink,
                ImageUrl = dataEntity.strDrinkThumb
            });
        }
    }
}
