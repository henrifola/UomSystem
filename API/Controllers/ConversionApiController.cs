using System;
using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace UomSystem.Controllers
{
    [ApiController]
    [Route("Convert")]
    public class ConversionApiController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMemoryCache _memoryCache;
        public ConversionApiController(IRepositoryWrapper wrapper, IMemoryCache memoryCache) 
        {
            _wrapper = wrapper;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Convert/unit_id_in+unit_id_out+quantity"); 
        }
        [HttpGet("{unitIdIn}+{unitIdOut}+{quantity}")]
        public async Task<ConversionResult> Get(string unitIdIn, string unitIdOut, double quantity)
        {
            return await _wrapper.UnitConverter.Conversion(unitIdIn, unitIdOut, quantity);
        }
    }


}
