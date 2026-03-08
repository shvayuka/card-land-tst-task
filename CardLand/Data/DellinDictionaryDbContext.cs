using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CardLand.Data
{
    public class DellinDictionaryDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
