using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace UomSystem.Controllers
{
    [ApiController]
    [Route("Convert")]
    public class ConversionApiController : ControllerBase
    {
        private readonly IEngineeringUnitsWrapper _wrapper;
        public ConversionApiController(IEngineeringUnitsWrapper wrapper) 
        {
            _wrapper = wrapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("USAGE: Convert/unit_id_in+unit_id_out+quantity"); 
        }
        [HttpGet("{unitIdIn}+{unitIdOut}+{quantity}")]
        public async Task<ConversionResult> Get(string unitIdIn, string unitIdOut, double quantity)
        {
            return await _wrapper.UnitConverter.Conversion(unitIdIn, unitIdOut, quantity);
        }
    }


}
