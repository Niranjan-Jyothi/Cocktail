using Microsoft.Extensions.DependencyInjection;

namespace Cocktails.Data.Implementation
{
    public static class DependencyInjection
    {
        public static void ConfigureDataServices(this IServiceCollection services)
        {
            services.AddTransient<ICocktailDataProvider, CocktailDataProvider>();
            services.AddTransient<ILogDataProvider, LogDataProvider>();
        }
    }
}
