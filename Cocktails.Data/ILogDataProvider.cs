using Cocktails.Data.Data;

namespace Cocktails.Data
{
    internal interface ILogDataProvider
    {
        void AddLog(CocktailLogData logData);
    }
}
