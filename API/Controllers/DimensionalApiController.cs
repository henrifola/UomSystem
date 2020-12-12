using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
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
            return Ok(_wrapper.DimensionalClass.ListAllDimensions());
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