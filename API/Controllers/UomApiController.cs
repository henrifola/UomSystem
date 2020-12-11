using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Data;
using Data.Models;
using EngineeringUnitscore;
using Microsoft.AspNetCore.Mvc;

namespace UomSystem.Controllers
{
    [ApiController]
    [Route("Uom")]
    public class UomAPiController : ControllerBase
    {
        private readonly UomHandler _uomHandler;

        public UomAPiController(RepositoryContext db)
        {
            _uomHandler = new UomHandler(db);
        }
        /*

        [HttpGet]
        public IActionResult GetAll()
        {
            return NoContent();
            return Ok(_uomHandler.GetUomsByQuantityTypes("0"));
        }
        [HttpGet("/DimensionalClass/{dimClassNotation}"),ActionName("GetByClass")]
        public IActionResult GetByClass(string dimClassNotation)
        {
            return Ok(_uomHandler.GetUomsByClass(dimClassNotation));
        }
        [HttpGet("/QuantityType/{qType}"),ActionName("GetByqType")]
        public IActionResult GetByQuantityType(string qType)
        {
            return Ok(_uomHandler.GetUomsByQuantityTypes(qType));
        }
        */
    }
}