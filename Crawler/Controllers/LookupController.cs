using System;
using Microsoft.AspNetCore.Mvc;
using Crawler.Services;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Serilog;

namespace Crawler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        ILookupService lookupService;

        public LookupController(ILookupService lookupService)
        {
            this.lookupService = lookupService;
        }

        // GET api/lookup/www.smokeball.com.au
        [HttpGet("{keyword}/{target}")]
        [SwaggerOperation(Description = "The result of this API ignore the Ads.")]
        public async Task<ActionResult<string>> Get(string keyword = "conveyancing software", string target = "www.smokeball.com.au")
        {
            try {
                return Ok(await lookupService.GetOccurrence(keyword, target));
            } catch (Exception ex) {
                Log.Error($"ERROR: {ex.Message}");
                throw ex;
            }
        }
    }
}
