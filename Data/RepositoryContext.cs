using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {}
        
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<DimensionalClass> DimenensionalClasses { get; set; }

        
    }
}