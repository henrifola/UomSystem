using Contracts.RepoContracts;
using Contracts.UnitOfMeasureContracts;
using Data;
using EngineeringUnitsCore.Converter;
using EngineeringUnitscore.Repos;
using Microsoft.Extensions.Caching.Memory;

namespace UomRepository.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper

    {
        private readonly RepositoryContext _context;
        private IQuantityRepo _quantityType;
        private IDimensionalRepo _dimensionalClass;
        private ICustomaryUnitRepo _customaryUnitRepo;
        private IUnitConversion _unitConversion;
        private IUnitOfMeasureRepo _unitOfMeasureRepo;
        private readonly IMemoryCache _memoryCache;

        public RepositoryWrapper(RepositoryContext context, IMemoryCache memoryCache)
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