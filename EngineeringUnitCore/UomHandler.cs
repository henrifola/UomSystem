using System.Collections.Generic;
using System.Linq;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;


namespace EngineeringUnitscore
{
    public class UomHandler : IUomHandler
    {
        private RepositoryContext _context;

        public UomHandler(RepositoryContext context)
        {
            _context = context;
        }

        

        
    }
}
