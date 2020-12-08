using System;
using Contracts.UnitOfMeasureContracts;
using Data;
using EngineeringUnitsCore.Converter;
using Microsoft.AspNetCore.Mvc;

namespace UomSystem.Controllers
{
    [ApiController]
    [Route("Convert")]
    public class ConversionApiController : ControllerBase
    {
        private readonly IUnitConversion _converter;

        public ConversionApiController(RepositoryContext context) //TODO inject the converter instead
        {
            _converter = new UnitConverter(context);
        }
        

        [HttpGet]
        public IActionResult Index()
        {
             
            return Ok("Convert/unit_id_in+unit_id_out+quantity"); 
        }

        [HttpGet("{unitIdIn}+{unitIdOut}+{quantity}")]
        public IActionResult Get(string unitIdIn, string unitIdOut, double quantity)
        {
            Console.WriteLine(unitIdIn);
            Console.WriteLine(unitIdOut);
            Console.WriteLine(quantity);
            return Ok(_converter.Conversion(unitIdIn, unitIdOut, quantity));
        }
        
        //example tests:
        
    }
}