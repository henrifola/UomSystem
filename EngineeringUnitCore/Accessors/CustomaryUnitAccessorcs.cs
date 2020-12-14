using System;
using System.Threading.Tasks;
using Contracts.RepoContracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using UomRepository.Common;

namespace EngineeringUnitscore.Repos
{
    //Class to access customary unit repo
    public class CustomaryUnitRepo : RepositoryBase<CustomaryUnit>, ICustomaryUnitRepo
    {
        public CustomaryUnitRepo(RepositoryContext context) : base(context)
        {
        }

        public new async Task<CustomaryUnit> Get(string id)
        {
            
            var unit = await Context
                .CustomaryUnits
                .Include(u => u.ConversionToBaseUnit)   
                .FirstOrDefaultAsync(u => u.Id == id);
            
            
            return unit;
        }
    }
    
}