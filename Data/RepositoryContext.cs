using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {}
        
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<DimensionalClass> DimensionalClasses { get; set; }
        
        public DbSet<SameUnit> SameUnits { get; set; }
        
        public DbSet<QuantityType> QuantityTypes { get; set; }
        
        
        public DbSet<CustomaryUnit> CustomaryUnits { get; set; }
        
        public DbSet<ConversionToBaseUnit> ConversionToBaseUnits { get; set; }
        
       
        
        public DbSet<UnitOfMeasureQuantityType> UnitOfMeasureQuantityTypes { get; set; }

        //to achieve two keys in a model class
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitOfMeasureQuantityType>().HasKey(keys => new { keys.UnitOfMeasureId, keys.QuantityTypeId });
        }
        
        
    }


}
