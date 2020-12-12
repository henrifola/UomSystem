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
        private readonly IRepositoryWrapper _wrapper;
        public ConversionApiController(IRepositoryWrapper wrapper) 
        {
            _wrapper = wrapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Correct usage: Convert/unit_id_in+unit_id_out+quantity"); 
        }
        [HttpGet("{unitIdIn}+{unitIdOut}+{quantity}")]
        public async Task<ConversionResult> Get(string unitIdIn, string unitIdOut, double quantity)
        {
            return await _wrapper.UnitConverter.Conversion(unitIdIn, unitIdOut, quantity);
        }
    }
}
