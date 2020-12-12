using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace UomSystem.Controllers
{
    [ApiController]
    [Route("DimensionalClass")]
    public class DimensionalApiController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMemoryCache _memoryCache;


        public DimensionalApiController(IRepositoryWrapper wrapper, IMemoryCache memoryCache)
        {
            _wrapper = wrapper;
            _memoryCache = memoryCache;
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
            
            if (_memoryCache.TryGetValue(notation, out List<string> cacheOut))return cacheOut;

            var dim = await _wrapper.DimensionalClass.ListUomForDimension(notation);
            cacheOut = await _wrapper.DimensionalClass.listUnits(dim);
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            _memoryCache.Set(notation, cacheOut, cacheEntryOptions);
            
            return cacheOut;
        }
    }
}