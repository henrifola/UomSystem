using System;
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
        private readonly string _usageMessage = "USAGE: Convert/unit_id_in+unit_id_out+quantity";
        public ConversionApiController(IEngineeringUnitsWrapper wrapper) 
        {
            _wrapper = wrapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_usageMessage); 
        }
        [HttpGet("{unitIdIn}+{unitIdOut}+{quantity}")]
        public IActionResult Get(string unitIdIn, string unitIdOut, double quantity)
        {
            try
            {
                return Ok(_wrapper.UnitConverter.Conversion(unitIdIn, unitIdOut, quantity).Result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message + _usageMessage);
            }
            catch (Exception e)
            {
                return Conflict(_usageMessage);
                Console.WriteLine(e);
            }
            

        }
    }


}
