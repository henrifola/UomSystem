using System;
using System.Linq;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace UomSystem.Controllers
{
    
    //this is a temproray solution, will be implemented with engineerunitscore instead
    
    [ApiController]
    [Route("DimensionalClass")]
    public class DimensionalApiController : ControllerBase
    {
        private readonly RepositoryContext _db;

        public DimensionalApiController(RepositoryContext db)
        {
            _db = db;
        }
        

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.DimenensionalClasses);
        }
        [HttpGet("{notation}")]
        public IActionResult Get(string notation) 
        {
            var dimensionalClass  = _db.DimenensionalClasses.Find(notation);

            if (dimensionalClass == null)
            {
                return NotFound();
            }
            
            foreach (var unit in dimensionalClass.Units)
            {
                Console.WriteLine(unit.Id);
            }
            
            
            return Ok(dimensionalClass);
        }
    }
}