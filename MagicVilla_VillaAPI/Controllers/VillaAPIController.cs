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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            var villas = VillaStore.villaList;
            if (villas.Count == 0)
                return NoContent();

            return Ok(villas);
        }

        [HttpGet("villa/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
                return NotFound();

            return Ok(villa);
        }
    }
}
