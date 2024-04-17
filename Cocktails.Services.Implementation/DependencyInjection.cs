using Cocktails.Services.Implementation.Services;
using Cocktails.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cocktails.Services.Implementation
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<ICocktailService, CocktailService>();
            services.AddTransient<ILogService, LogService>();
        }
    }
}
