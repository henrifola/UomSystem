using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace UomSystem.Controllers
{
    [ApiController]
    [Route("QuantityType")]
    public class QuantityApiController : ControllerBase
    {
        private readonly IEngineeringUnitsWrapper _wrapper;
        private readonly IMemoryCache _memoryCache;
        public QuantityApiController(IEngineeringUnitsWrapper wrapper, IMemoryCache memoryCache)
        {
            _wrapper = wrapper;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_wrapper.QuantityType.ListAllQuantityTypes());
        }
        [HttpGet("{notation}")]
        public async Task<List<string>> GetUom(string notation)
        {
            notation = notation.ToLower();
            
            if (_memoryCache.TryGetValue(notation, out List<string> cacheOut)) return cacheOut;
            var qt = await _wrapper.QuantityType.ListUomForQuantityType(notation);
            cacheOut = await _wrapper.QuantityType.listUnits(qt);
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            _memoryCache.Set(notation, cacheOut, cacheEntryOptions);
            
            return cacheOut;
        }
    }
}