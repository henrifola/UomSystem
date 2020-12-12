using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using UomRepository.Common;

namespace EngineeringUnitscore.Repos
{
    public class QuantityRepo : RepositoryBase<QuantityType>, IQuantityRepo

    {
        public QuantityRepo(RepositoryContext context) : base(context)
        {
        }


        public ICollection<QuantityType> ListAllQuantityTypes()
        {
            return Context.QuantityTypes.ToList();
        }
        
        public async Task<QuantityType> ListUomForQuantityType(string qt)
        {
            var UomQt = await Context
                .QuantityTypes
                .Include(u => u.UnitOfMeasureQuantityTypes)
                .FirstOrDefaultAsync(u => u.Notation == qt);
            
            
            /*
            var UomQt = Context.UnitOfMeasureQuantityTypes
                .Where(u => u.QuantityTypeId == qt)
                .Include("UnitOfMeasures").ToList();
                */
            if (UomQt is null) throw new ArgumentException("Quantity type is null or invalid");
            Console.WriteLine(UomQt.Notation);
            return UomQt;
        }

        public async Task<List<string>> listUnits(QuantityType uomQt)
        {
           return uomQt.UnitOfMeasureQuantityTypes.Select(u => u.UnitOfMeasureId).ToList();
        }
    }
}