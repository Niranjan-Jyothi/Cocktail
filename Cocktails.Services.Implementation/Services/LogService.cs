using Cocktails.Data;
using Cocktails.Services.Implementation.Convertors;
using Cocktails.Services.Models;
using Cocktails.Services.Services;

namespace Cocktails.Services.Implementation.Services
{
    internal class LogService : ILogService
    {
        private readonly ILogDataProvider _logDataProvider;

        public LogService(ILogDataProvider logDataProvider)
        {
            _logDataProvider = logDataProvider ?? throw new ArgumentNullException(nameof(logDataProvider));
        }

        public void AddLog(CocktailLog log)
        {
            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _logDataProvider.AddLog(log.ToData());
        }
    }
}
