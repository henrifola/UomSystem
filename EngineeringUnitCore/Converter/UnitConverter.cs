using System;
using System.Threading.Tasks;
using Contracts.RepoContracts;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using EngineeringUnitscore.Repos;
using Microsoft.Extensions.Caching.Memory;

namespace EngineeringUnitsCore.Converter
{
    public class UnitConverter : IUnitConversion
    {
        private readonly ICustomaryUnitRepo _customaryUnitRepo;
        private readonly IMemoryCache _memoryCache;
        public UnitConverter(RepositoryContext context, IMemoryCache memoryCache)
        {
            _customaryUnitRepo = new CustomaryUnitRepo(context);
            _memoryCache = memoryCache;
        }
        public async Task<ConversionResult> Conversion(string inputUnitId, string outputUnitId, double quantity)
        {
            if (ValidateConversion(inputUnitId, outputUnitId).Result is false) throw new ArgumentException("Cant convert units with different base unit");
            var toBaseConversion = await ConversionToBase(inputUnitId, quantity);
            var toCustomaryConversion = await ConversionToCustomary(outputUnitId, toBaseConversion);
            var cu = await _customaryUnitRepo.Get(outputUnitId);
            var conversionResult = new ConversionResult(toCustomaryConversion, cu.Id, cu.Annotation);
            return conversionResult;
        }
        private async Task<ConversionToBaseUnit> GetUnit(string unit)
        {
            if (unit is null) throw new ArgumentException("Unit is null");
            var inputUnit = await _customaryUnitRepo.Get(unit);
            return inputUnit.ConversionToBaseUnit;
        }
        private async Task<double> ConversionToBase(string unit, double quantity)
        {
            if (_memoryCache.TryGetValue(unit, out ConversionToBaseUnit cacheOut)) return ConversionCalculation(cacheOut.A, cacheOut.B, cacheOut.C, cacheOut.D, quantity);
            
            Console.WriteLine("Base conversion not cached, caching now");
            
            cacheOut = await GetUnit(unit);
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            _memoryCache.Set(unit, cacheOut, cacheEntryOptions); 
            return ConversionCalculation(cacheOut.A, cacheOut.B, cacheOut.C, cacheOut.D, quantity); 
        }
        private async Task<double> ConversionToCustomary(string unit, double baseConversion)
        {
            if (_memoryCache.TryGetValue(unit, out ConversionToBaseUnit cacheOut)) return ConversionCalculation(cacheOut.A, cacheOut.C, cacheOut.B, cacheOut.D, baseConversion);
            
            Console.WriteLine("Customary conversion not cached, caching now");
            
            cacheOut = await GetUnit(unit);
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            _memoryCache.Set(unit, cacheOut, cacheEntryOptions);
            return ConversionCalculation(cacheOut.A, cacheOut.C, cacheOut.B, cacheOut.D, baseConversion);
        }
        //swap b and c when going from base to customary unit, and insert base conversion as x
        private static double ConversionCalculation(double a, double b, double c, double d, double x)
        {
            return (a + (b * x)) / (c + (d * x));
        }
        private async Task<bool> ValidateConversion(string inputUnitId, string outputUnitId)
        {
            var fromBaseUnit = await GetUnit(inputUnitId);
            var toBaseUnit = await GetUnit(outputUnitId);
            return fromBaseUnit.BaseUnit == toBaseUnit.BaseUnit;
        }
    }
}