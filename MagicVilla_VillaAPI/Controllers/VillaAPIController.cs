using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet("villas")]        
        public IEnumerable<VillaDto> GetVillas()
        {
            var villas = VillaStore.villaList;

            return villas;
        }

        [HttpGet("villa/{id}")]
        public IActionResult GetVilla(int id)
        {
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
                return NotFound();

            return Ok(villa);
        }
    }
}
