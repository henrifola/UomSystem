using System.Threading.Tasks;
using Contracts.RepoContracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using UomRepository.Common;

namespace EngineeringUnitscore.Accessors
{
    //Class to access the uom repo
    public class UnitOfMeasureRepo: RepositoryBase<UnitOfMeasure>, IUnitOfMeasureRepo

    //public class CustomaryUnitRepo : RepositoryBase<CustomaryUnit>, ICustomaryUnitRepo
    {
        public UnitOfMeasureRepo(RepositoryContext context) : base(context)
        {
        }
        
        public async Task<UnitOfMeasure>Get(string id)
        {
            
            var unit = await Context
                .UnitOfMeasures 
                .FirstOrDefaultAsync(u => u.Id == id || u.Annotation==id);
            
            
            return unit;
        }
    }
}