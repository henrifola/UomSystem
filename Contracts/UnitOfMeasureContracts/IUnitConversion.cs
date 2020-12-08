using System;
using Data.Models;
using Microsoft.VisualBasic;

namespace Contracts.UnitOfMeasureContracts
{
    public interface IUnitConversion
    {
        Double Conversion(String inputUnitId, String outputUnitId, Double quantity);
        
    }
}