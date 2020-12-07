using System;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;

namespace EngineeringUnitsCore.Converter
{
    public class Converter : IConversion
    {
        private readonly RepositoryContext _context;
        Converter(RepositoryContext context)
        {
            _context = context;
        }
        
        
        public double Conversion(string inputUnitId, string outputUnitId, double quantity)
        {
            var inputUnit = _context.UnitOfMeasures.Find(inputUnitId);
            var outputUnit = _context.UnitOfMeasures.Find(inputUnitId);
            

            if (IsBaseUnit(inputUnit) && IsBaseUnit(outputUnit))
            {
                if (inputUnit == outputUnit)
                {
                    return quantity;
                }

                throw new Exception("Cant convert from base unit to other base unit");

            }
            
            
            if (inputUnit is CustomaryUnit customaryInput)  // customary -> base
                quantity = customaryInput.ConversionToBaseUnit.Convert(quantity);

            if (outputUnit is CustomaryUnit customaryOutput) // base -> customary
                quantity = BaseToCustomary(customaryOutput, quantity);


            return quantity;
        }
        
        

        private static Boolean IsBaseUnit(UnitOfMeasure uom)
        {
            return !(uom is CustomaryUnit);
        }

        //convert to customary:
        // f(q) = (A-C*q) * (D*q-B)
        
        private static Double BaseToCustomary(CustomaryUnit unitOfMeasure, double quantity)
        {
            var x = unitOfMeasure.ConversionToBaseUnit;
            return (x.A - x.C * quantity) * (x.D * quantity - x.B);
        }
    }
}