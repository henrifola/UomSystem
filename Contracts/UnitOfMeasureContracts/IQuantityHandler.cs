using System.Collections.Generic;
using Data.Models;

namespace Contracts.UnitOfMeasureContracts
{
    public interface IQuantityHandler
    {
        ICollection<QuantityType> GetAllQuantityTypes();

        QuantityType GetByNotation(string notation);
        
        ICollection<UnitOfMeasureQuantityType> GetUomsByQuantityTypes(string quantityTypeId);
    }
}