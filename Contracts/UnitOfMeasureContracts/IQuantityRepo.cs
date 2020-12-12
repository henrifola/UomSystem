using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace Contracts.UnitOfMeasureContracts
{
    public interface IQuantityRepo
    {
        public ICollection<QuantityType> ListAllQuantityTypes();
        
        public Task<List<string>> listUnits(QuantityType UomQt);
        public Task<QuantityType> ListUomForQuantityType(string qt);
        
        
    }
}