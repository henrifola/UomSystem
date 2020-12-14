using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace Contracts.RepoContracts
{
    public interface IDimensionalRepo
    {
        public ICollection<DimensionalClass> ListAllDimensions();
        public Task<DimensionalClass> ListUomForDimension(string dimension);
        public Task<List<string>> listUnits(DimensionalClass uomDim);

    }
}