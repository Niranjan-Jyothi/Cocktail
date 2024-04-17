using Cocktails.Data.Data;

namespace Cocktails.Data.Implementation
{
    internal sealed class LogDataProvider : ILogDataProvider
    {
        private readonly AppDbContext _context;

        public LogDataProvider(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddLog(CocktailLogData logData)
        {
            if (logData is null)
            {
                throw new ArgumentNullException(nameof(logData));
            }

            _context.CocktailLog.Add(logData);
            _context.SaveChanges();
        }
    }
}
