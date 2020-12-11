using System.Collections.Generic;
using Data.Models;

namespace Contracts.UnitOfMeasureContracts
{
    /*
     *  1. List all dimension classes
     *  2. Get uoms by class
     *  3. modify/add elements
     */
    public interface IDimensionalHandler
    {
        ICollection<DimensionalClass> GetAllDimensionalClasses();
        
        ICollection<UnitOfMeasure> GetUomsByClass(string dimensionalClassId);
    }
}