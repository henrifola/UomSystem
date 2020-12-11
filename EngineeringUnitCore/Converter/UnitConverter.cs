using System;
using System.Linq;
using System.Threading.Tasks;
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

        private async Task<string> checkBase(string inputUnitId, string outputUnitId)
        {

            return "xd";
        } 
        
        public double Conversion(string inputUnitId, string outputUnitId, double quantity)
        {
            var inputUnit = _context.UnitOfMeasures.Find(inputUnitId);
            var outputUnit = _context.UnitOfMeasures.Find(outputUnitId);
            
            if (inputUnit == outputUnit)
            {
                return quantity;
            }

            if (IsBaseUnit(inputUnit) && IsBaseUnit(outputUnit))
            {
                throw new Exception("Cant convert from base unit to other base unit");
            }


            if (inputUnit is CustomaryUnit) // customary -> base
            {
                
                var conversionToBaseUnit = _context.ConversionToBaseUnits.Find(inputUnitId) 
                                           ?? throw new ArgumentNullException("_context.ConversionToBaseUnits.Find(outputUnitId)");

                quantity = conversionToBaseUnit.Convert(quantity);
                Console.WriteLine("customary -> base");
            }

            if (outputUnit is CustomaryUnit) // base -> customary
            {
                
                var conversionToBaseUnit = _context.ConversionToBaseUnits.Find(outputUnitId) 
                                           ?? throw new ArgumentNullException("_context.ConversionToBaseUnits.Find(outputUnitId)");
                    
                
                quantity = BaseToCustomary(conversionToBaseUnit, quantity);
                Console.WriteLine("base -> customary");
            }
            Console.WriteLine("Conversion result="+quantity);
            return quantity;
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