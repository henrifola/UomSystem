using System.Threading.Tasks;
using Data.Models;

namespace Contracts.RepoContracts
{
    public interface IUnitOfMeasureRepo
    {
        public Task<UnitOfMeasure> Get(string id);
    }
}