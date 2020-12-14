using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.RepoContracts;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using UomRepository.Common;

namespace EngineeringUnitscore.Repos
{
    public class DimensionalRepo : RepositoryBase<DimensionalClass>, IDimensionalRepo
    {
        public DimensionalRepo(RepositoryContext context) : base(context)
        {
        }

        public ICollection<DimensionalClass> ListAllDimensions()
        {
            return Context.DimensionalClasses.ToList();
        }

        public async Task<DimensionalClass> ListUomForDimension(string dimension)
        {
            var UomDim = await Context
                .DimensionalClasses
                .Include(u => u.Units)
                .FirstOrDefaultAsync(u => u.Notation == dimension);
            if (UomDim is null) throw new ArgumentException("Dimension is null or invalid");
            return UomDim;

        }
        
        public async Task<List<string>> listUnits(DimensionalClass uomDim)
        {
            return uomDim.Units.Select(u => u.Id).ToList();
        }
    } 
}