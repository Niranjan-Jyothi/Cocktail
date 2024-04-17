using Cocktails.Contracts;
using CocktailModel = Cocktails.Services.Models.Cocktail;

namespace Cocktails.Convertors
{
    internal static class CocktailConvertor
    {
        public static IEnumerable<Cocktail> ToContract(this IEnumerable<CocktailModel> modelItems)
        {
            return modelItems.Select(modelItem => new Cocktail()
            {
                Id = modelItem.Id,
                Name = modelItem.Name,
                ImageUrl = modelItem.ImageUrl
            });
        }
    }
}
