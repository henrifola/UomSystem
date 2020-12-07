using System;
using Data.Models;
using Microsoft.VisualBasic;

namespace Contracts.UnitOfMeasureContracts
{
    public interface IConversion
    {
        Double Conversion(String inputUnitId, String outputUnitId, Double quantity);
        
    }
}