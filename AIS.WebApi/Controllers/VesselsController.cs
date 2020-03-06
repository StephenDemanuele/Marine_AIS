using System.Linq;
using AIS.WebApi.DTO;
using AIS.Parser.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AIS.WebApi.Controllers
{
    [Route("api/vessels")]
    [ApiController]
    public class VesselsController : ControllerBase
    {
        private readonly IReadOnlyVesselCollection _vesselCollection;

        public VesselsController(IReadOnlyVesselCollection vesselCollection)
        {
            _vesselCollection = vesselCollection;
        }

        [Route("headers"), HttpGet]
        public ActionResult<IEnumerable<VesselHeaderDto>> GetHeader()
        {
            var vesselHeaderDtos = _vesselCollection.AsReadOnly()
                .Select(x => new VesselHeaderDto(x.UserId, x.Name, x.Talker))
                .ToList();

            return new JsonResult(vesselHeaderDtos);
        }

        [Route("details"), HttpGet]
        public ActionResult<IEnumerable<VesselDetailDto>> GetDetails()
        {
            var vesselDetailDtos = _vesselCollection.AsReadOnly()
                .Select(x => new VesselDetailDto(x))
                .ToList();

            return new JsonResult(vesselDetailDtos);
        }

        [HttpGet("{userId}")]
        public ActionResult<string> Get(int userId)
        {
            var vessel = _vesselCollection.AsReadOnly().FirstOrDefault(x => x.UserId.Equals(userId));
            if (vessel == null)
                return NotFound();

            return new JsonResult(vessel);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Forbid();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Forbid();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Forbid();
        }
    }
}
