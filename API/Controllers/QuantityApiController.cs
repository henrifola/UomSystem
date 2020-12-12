using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using EngineeringUnitscore;
using Microsoft.AspNetCore.Mvc;

namespace UomSystem.Controllers
{
    [ApiController]
    [Route("QuantityType")]
    public class QuantityApiController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        public QuantityApiController(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var quantityTypes = _wrapper.QuantityType.ListAllQuantityTypes();
            var stringRepresentation = quantityTypes.ToList().ConvertAll(x => x.Notation);
            return Ok(stringRepresentation);
        }
        [HttpGet("{notation}")]
        public async Task<List<string>> GetUom(string notation)
        {
            notation = notation.ToLower();
            var qt = await _wrapper.QuantityType.ListUomForQuantityType(notation);
            var units = await _wrapper.QuantityType.listUnits(qt);
            return units;
        }
    }
}