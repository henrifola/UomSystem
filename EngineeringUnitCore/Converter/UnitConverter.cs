using System;
using System.Threading.Tasks;
using Contracts.RepoContracts;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using EngineeringUnitscore.Accessors;
using EngineeringUnitscore.Repos;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;
using UomRepository.Common;

namespace EngineeringUnitsCore.Converter
{
    public class UnitConverter : IUnitConversion
    {
        private readonly ICustomaryUnitRepo _customaryUnitRepo;
        private readonly IUnitOfMeasureRepo _unitOfMeasureRepo;
        private readonly IMemoryCache _memoryCache;
        public UnitConverter(RepositoryContext context, IMemoryCache memoryCache)
        {
            _customaryUnitRepo = new CustomaryUnitRepo(context);
            _memoryCache = memoryCache;
            _unitOfMeasureRepo = new UnitOfMeasureRepo(context);
        }
        public async Task<ConversionResult> Conversion(string inputUnitId, string outputUnitId, double quantity)
        {
            var inputBase = await isBase(inputUnitId);
            var outputBase = await isBase(outputUnitId);
            Console.WriteLine(inputBase is true ? "Input is base unit" : "Input is customary unit");
            Console.WriteLine(outputBase is true ? "Output is base unit" : "Output is customary unit");

            switch (outputBase)
            {
                case false when inputBase is false:
                    return await bothCustom(inputUnitId, outputUnitId, quantity);
                case true when inputBase is true:
                    if (inputUnitId != outputUnitId)
                        throw new ArgumentException("Cant convert units with different base unit");
                    var cu = await _unitOfMeasureRepo.Get(outputUnitId);
                    return new ConversionResult(quantity, cu.Id, cu.Annotation);
                case false when inputBase is true:
                    return await baseInput(inputUnitId, outputUnitId, quantity);
                case true when inputBase is false:
                    return await baseOutput(inputUnitId, outputUnitId, quantity);
            }
       return new ConversionResult(0, "unknown", "unknown"); }
        
        private async Task<ConversionResult> baseInput(string inputUnitId, string outputUnitId, double quantity) //base -> customary
        {
            //only need to convert TO customary
            //if (ValidateConversion(inputUnitId, outputUnitId).Result is false) throw new ArgumentException("Cant convert units with different base unit");
            var IB = await GetBaseUnit(outputUnitId);
            if (IB != inputUnitId) throw new ArgumentException("Cant convert units with different base unit");
            var toCustomaryConversion = await ConversionToCustomary(outputUnitId, quantity);
            var cu = await _customaryUnitRepo.Get(outputUnitId);
            var conversionResult = new ConversionResult(toCustomaryConversion, cu.Id, cu.Annotation);
            return conversionResult;
        }

        private async Task<ConversionResult> baseOutput(string inputUnitId, string outputUnitId, double quantity)
        {
            //if (ValidateConversion(inputUnitId, outputUnitId).Result is false) throw new ArgumentException("Cant convert units with different base unit");
            var OB = await GetBaseUnit(inputUnitId);
            if (OB != outputUnitId ) throw new ArgumentException("Cant convert units with different base unit");
            var toBaseConversion = await ConversionToBase(inputUnitId, quantity);
            var cu = await _unitOfMeasureRepo.Get(outputUnitId);
            var conversionResult = new ConversionResult(toBaseConversion, cu.Id, cu.Annotation);
            return conversionResult;
        }

        private async Task<ConversionResult> bothCustom(string inputUnitId, string outputUnitId, double quantity)
        {
            //if (ValidateConversion(inputUnitId, outputUnitId).Result is false) throw new ArgumentException("Cant convert units with different base unit");
            var IB = await GetBaseUnit(outputUnitId);
            var OB = await GetBaseUnit(inputUnitId);
            if (OB != IB) throw new ArgumentException("Cant convert units with different base unit");
            var toBaseConversion = await ConversionToBase(inputUnitId, quantity);
            var toCustomaryConversion = await ConversionToCustomary(outputUnitId, toBaseConversion);
            var cu = await _customaryUnitRepo.Get(outputUnitId);
            var conversionResult = new ConversionResult(toCustomaryConversion, cu.Id, cu.Annotation);
            return conversionResult;
        }

        private async Task<bool> isBase(string unit)
        {
            try
            {
                var cUnit = await _customaryUnitRepo.Get(unit);
                return cUnit.BaseUnitId == null;
            }
            catch (Exception e)
            {
                var cUnit = await _unitOfMeasureRepo.Get(unit);
                return cUnit.Annotation != null;
            }
            
        }
        private async Task<double> ConversionToBase(string unit, double quantity)
        {
            if (_memoryCache.TryGetValue(unit, out ConversionToBaseUnit cacheOut)) return ConversionCalculation(cacheOut.A, cacheOut.B, cacheOut.C, cacheOut.D, quantity);
            
            Console.WriteLine("Base conversion not cached, caching now");
            
            cacheOut = await GetCacheUnit(unit);
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            _memoryCache.Set(unit, cacheOut, cacheEntryOptions); 
            return ConversionCalculation(cacheOut.A, cacheOut.B, cacheOut.C, cacheOut.D, quantity); 
        }
        private async Task<double> ConversionToCustomary(string unit, double baseConversion)
        {
            //if (_memoryCache.TryGetValue(unit, out ConversionToBaseUnit cacheOut)) return ConversionCalculation(cacheOut.A, cacheOut.C, cacheOut.B, cacheOut.D, baseConversion);
            if (_memoryCache.TryGetValue(unit, out ConversionToBaseUnit cacheOut)) return ConversionCalculationToCustomary(cacheOut.A, cacheOut.B, cacheOut.C, cacheOut.D, baseConversion);
            Console.WriteLine("Customary conversion not cached, caching now");
            
            cacheOut = await GetCacheUnit(unit);
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            _memoryCache.Set(unit, cacheOut, cacheEntryOptions);
            //return ConversionCalculation(cacheOut.A, cacheOut.C, cacheOut.B, cacheOut.D, baseConversion);
            return ConversionCalculationToCustomary(cacheOut.A, cacheOut.B, cacheOut.C, cacheOut.D, baseConversion);
        }
        //swap b and c when going from base to customary unit, and insert base conversion as x
        private static double ConversionCalculation(double a, double b, double c, double d, double x)
        {
            return (a + (b * x)) / (c + (d * x));
        }
        private static double ConversionCalculationToCustomary(double a, double b, double c, double d, double x)
        {
            return (a - (c * x)) / ( (d * x) - b);
        }
        
        
        private async Task<ConversionToBaseUnit> GetCacheUnit(string unit)
        {
            if (unit is null) throw new ArgumentException("Unit is null");
            var inputUnit = await _customaryUnitRepo.Get(unit);
            return inputUnit.ConversionToBaseUnit;
        }
        private async Task<bool> ValidateConversion(string inputUnitId, string outputUnitId)
        {
            var fromBase = await GetBaseUnit(inputUnitId);
            var toBase = await GetBaseUnit(outputUnitId);
            Console.WriteLine(fromBase + " " +  toBase);
            return fromBase == toBase;
        }
        
        private async Task<string> GetBaseUnit(string unit)
        {
            if (unit is null) throw new ArgumentException("Unit is null");
            var inputUnit = await _customaryUnitRepo.Get(unit);
            if (inputUnit is null)
            {
                var baseU = await _unitOfMeasureRepo.Get(unit);
                return baseU.Annotation;
            }
            var baseUnit = await _unitOfMeasureRepo.Get(inputUnit.BaseUnitId);
            return baseUnit.Id;
        }
    }
}
