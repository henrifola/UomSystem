using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EngineeringUnitscore
{
    public class QuantityHandler : QuantityType
    {
        private RepositoryContext _context;

        public QuantityHandler(RepositoryContext context)
        {
            _context = context;
        }

        public ICollection<QuantityType> GetAllQuantityTypes()
        {
            return _context.QuantityTypes.ToList();
        }

        public QuantityType GetByNotation(string notation)
        {
            return _context.QuantityTypes.Find(notation);
        }
        public ICollection<UnitOfMeasureQuantityType> GetUomsByQuantityTypes(string quantityTypeId)
        {
            var x = _context.UnitOfMeasureQuantityTypes.
                Where(u => u.QuantityTypeId == quantityTypeId)
                .Include("UnitOfMeasures").ToList();
            return x;
        }
    }
}