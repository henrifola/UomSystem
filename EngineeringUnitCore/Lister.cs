using System.Collections.Generic;
using System.Linq;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EngineeringUnitscore
{
    public class Lister : ILister
    {
        private readonly RepositoryContext _context;
        public Lister(RepositoryContext context)
        {
            _context = context;
        }


        public ICollection<DimensionalClass> GetAllDimensionalClasses()
        {
            return _context.DimensionalClasses.ToList();
        }

        public ICollection<QuantityType> GetAllQuantityTypes()
        {
            return _context.QuantityTypes.ToList();
        }

        public ICollection<UnitOfMeasure> GetUomsByClass(string dimensionalClassId)
        {
            return  _context.DimensionalClasses.
                Where(d => d.Notation == dimensionalClassId).
                Include("UnitOfMeasures").FirstOrDefault()?.Units;
        }

        public ICollection<UnitOfMeasureQuantityType> GetUomsByQuantityTypes(string quantityTypeId)
        {
            var x = _context.UnitOfMeasureQuantityTypes.
                Where(u => u.QuantityTypeId == quantityTypeId).ToList();
            return x;
        }
    }
}