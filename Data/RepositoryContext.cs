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
        
        public DbSet<SameUnit> SameUnits { get; set; }
        
        public DbSet<QuantityType> QuantityTypes { get; set; }
        
        
        public DbSet<CustomaryUnit> CustomaryUnits { get; set; }
        
        public DbSet<ConversionToBaseUnit> ConversionToBaseUnits { get; set; }

        
    }
}