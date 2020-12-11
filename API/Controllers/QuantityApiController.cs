using System;
using Data;
using EngineeringUnitscore;
using Microsoft.AspNetCore.Mvc;

namespace UomSystem.Controllers
{
    [ApiController]
    [Route("QuantityType")]
    public class QuantityApiController : ControllerBase
    {
        private readonly QuantityHandler quantityHandler;

        public QuantityApiController(RepositoryContext db)
        {
            quantityHandler = new QuantityHandler(db);
        }
        

        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(quantityHandler.GetAllQuantityTypes());
        }
        [HttpGet("{notation}")]
        public IActionResult GetUom(string notation)
        {
            return Ok(quantityHandler.GetUomsByQuantityTypes(notation));
        }
    }
}