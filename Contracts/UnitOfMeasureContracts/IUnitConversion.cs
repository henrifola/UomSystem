using System;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.VisualBasic;

namespace Contracts.UnitOfMeasureContracts
{
    public interface IUnitConversion
    {
       public Task<ConversionResult> Conversion(string inputUnitId, string outputUnitId, double quantity);
        
    }
}