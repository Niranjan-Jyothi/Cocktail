using Cocktails.Data.Data;
using Cocktails.Services.Models;

namespace Cocktails.Services.Implementation.Convertors
{
    internal static class CocktailLogConvertor
    {
        public static CocktailLogData ToData(this CocktailLog logModel)
        {
            if (logModel is null)
            {
                throw new ArgumentNullException(nameof(logModel));
            }

            return new CocktailLogData
            {
                ErrorMessage = logModel.ErrorMessage,
                InData = logModel.InData,
                MethodName = logModel.MethodName,
                OutData = logModel.OutData,
                ReplyDateTime = logModel.ReplyDateTime,
                RequestDateTime = logModel.RequestDateTime
            };
        }
    }
}
