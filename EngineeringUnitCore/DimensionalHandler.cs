using System.Collections.Generic;
using System.Linq;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EngineeringUnitscore
{
    public class DimensionalHandler : IDimensionalHandler
    {
        private RepositoryContext _context;

        public DimensionalHandler(RepositoryContext context)
        {
            _context = context;
        }

        public ICollection<DimensionalClass> GetAllDimensionalClasses()
        {
            return _context.DimensionalClasses.ToList();
        }
        public ICollection<UnitOfMeasure> GetUomsByClass(string dimensionalClassId)
        {
            return  _context.DimensionalClasses.
                Where(d => d.Notation == dimensionalClassId).
                Include("UnitOfMeasures").FirstOrDefault()?.Units;
        }
    }
}