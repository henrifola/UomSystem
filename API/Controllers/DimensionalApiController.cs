using System;
using System.Linq;
using Data;
using Data.Models;
using EngineeringUnitscore;
using Microsoft.AspNetCore.Mvc;

namespace UomSystem.Controllers
{
    
  
    
    [ApiController]
    [Route("DimensionalClass")]
    public class DimensionalApiController : ControllerBase
    {
        private DimensionalHandler dimensionalHandler;

        public DimensionalApiController(RepositoryContext db)
        {
            dimensionalHandler = new DimensionalHandler(db);
        }
        

        [HttpGet]
        public IActionResult GetAll()
        {
            var dimensionalClasses = dimensionalHandler.GetAllDimensionalClasses();
            
            return Ok(dimensionalClasses);
        }
        [HttpGet("{notation}")]
        public IActionResult GetUom(string notation)
        {
            return Ok(dimensionalHandler.GetUomsByClass(notation));
        }
    }
}


