using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;


namespace UomSystem.Controllers
{
    [ApiController]
    [Route("DimensionalClass")]
    public class DimensionalApiController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;

        public DimensionalApiController(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }
        

        [HttpGet]
        public IActionResult GetAll()
        {
            //var dimensionalClasses = dimensionalHandler.GetAllDimensionalClasses();
            var dimensionalClasses = _wrapper.DimensionalClass.ListAllDimensions();

            var stringRepresentation = dimensionalClasses.ToList().ConvertAll(x => x.Notation);
            
            return Ok(stringRepresentation);
        }
        [HttpGet("{notation}")]
        public async Task<List<string>> GetUom(string notation)
        {
            //notation = notation.ToLower();
            var dim = await _wrapper.DimensionalClass.ListUomForDimension(notation);
            var units = await _wrapper.DimensionalClass.listUnits(dim);
            return units;
        }
    }
}