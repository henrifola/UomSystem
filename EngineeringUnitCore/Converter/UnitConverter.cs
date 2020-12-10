using System;
using System.Linq;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EngineeringUnitsCore.Converter
{
    public class UnitConverter : IUnitConversion
    {
        private readonly RepositoryContext _context;
        public UnitConverter(RepositoryContext context)
        {
            _context = context;
        }
        
        
        public ConversionResult Conversion(string inputUnitId, string outputUnitId, double quantity)
        {
            var inputUnit = _context.UnitOfMeasures.Find(inputUnitId);
            var outputUnit = _context.UnitOfMeasures.Find(outputUnitId);

            if (inputUnit == null )
                throw new ArgumentException("First unit does not exist");
            
            if(outputUnit == null)
                throw new ArgumentException("Second unit does not exist");

            if (inputUnit.BaseUnitId != outputUnit.BaseUnitId)
            {
                throw new ArgumentException("Cant convert units with different base unit");
            }
            
            if (inputUnit == outputUnit)
            {
                return new ConversionResult(quantity,outputUnit);
            }

            if (inputUnit is CustomaryUnit) // customary -> base
            {

                var conversionToBaseUnit = _context.ConversionToBaseUnits.Find(inputUnitId);
                                           

                quantity = conversionToBaseUnit.Convert(quantity);
                Console.WriteLine("customary -> base");
            }

            if (outputUnit is CustomaryUnit) // base -> customary
            {

                var conversionToBaseUnit = _context.ConversionToBaseUnits.Find(outputUnitId);


                quantity = BaseToCustomary(conversionToBaseUnit, quantity);
                Console.WriteLine("base -> customary");
            }
            Console.WriteLine("Conversion result="+quantity);
            
            return new ConversionResult(quantity,outputUnit);
        }
        
        

        private static Boolean IsBaseUnit(UnitOfMeasure uom)
        {
            return !(uom is CustomaryUnit);
        }

        //convert to customary:
        // f(q) = (A-C*q) * (D*q-B)
        
        private static Double BaseToCustomary(ConversionToBaseUnit x, double quantity)
        {
            return (x.A - x.C * quantity) * (x.D * quantity - x.B);
        }
    }
}