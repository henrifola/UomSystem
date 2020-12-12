using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Contracts.RepoContracts
{
    public interface ICustomaryUnitRepo : IRepositoryBase<CustomaryUnit>
    {
        //DbSet<CustomaryUnit> Get(string id);
    }
}