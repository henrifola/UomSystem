using Contracts.RepoContracts;
using Contracts.UnitOfMeasureContracts;
using Data;
using EngineeringUnitscore.Accessors;
using EngineeringUnitsCore.Converter;
using EngineeringUnitscore.Repos;
using Microsoft.Extensions.Caching.Memory;

namespace EngineeringUnitscore.Wrapper
{
    public class EngineeringUnitsWrapper : IEngineeringUnitsWrapper

    {
        private readonly RepositoryContext _context;
        private IQuantityRepo _quantityType;
        private IDimensionalRepo _dimensionalClass;
        private ICustomaryUnitRepo _customaryUnitRepo;
        private IUnitConversion _unitConversion;
        private IUnitOfMeasureRepo _unitOfMeasureRepo;
        private readonly IMemoryCache _memoryCache;

        public EngineeringUnitsWrapper(RepositoryContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
            
        }

       public IQuantityRepo QuantityType 
           => _quantityType ??= new QuantityRepo(_context);
       
       public IDimensionalRepo DimensionalClass 
           => _dimensionalClass ??= new DimensionalRepo(_context);
       
       public ICustomaryUnitRepo CustomaryUnit 
           => _customaryUnitRepo ??= new CustomaryUnitRepo(_context);
       
       public IUnitConversion UnitConverter 
           => _unitConversion ??= new UnitConverter(_context, _memoryCache);
       
       public IUnitOfMeasureRepo UnitOfMeasure 
           => _unitOfMeasureRepo ??= new UnitOfMeasureRepo(_context);
    }
}