using System.Collections.Generic;
using Data.Models;

namespace Contracts.UnitOfMeasureContracts
{
    /*
     This interface describes these requirments:
     The system shall:
        1. List all dimension classes
        2. List all quantity types
        3. List all uom for a given class
        4. List all uom for a given quantity type
    */
    public interface ILister
    {
        ICollection<DimensionalClass> GetAllDimensionalClasses();
        
        ICollection<QuantityType> GetAllQuantityTypes();
        
        ICollection<UnitOfMeasure> GetUomsByClass(string dimensionalClassId);
        
        ICollection<UnitOfMeasureQuantityType> GetUomsByQuantityTypes(string quantityTypeId);
        
    }
}