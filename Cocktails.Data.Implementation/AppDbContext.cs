using Cocktails.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Data.Implementation
{
    internal sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<CocktailLogData> CocktailLog { get; set;}
    }
}
