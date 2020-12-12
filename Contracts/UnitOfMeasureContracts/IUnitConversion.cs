using System.Threading.Tasks;
using Data.Models;

namespace Contracts.UnitOfMeasureContracts
{
    public interface IUnitConversion
    {
       public Task<ConversionResult> Conversion(string inputUnitId, string outputUnitId, double quantity);
        
    }
}