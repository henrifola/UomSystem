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
        

        

        
    }
}