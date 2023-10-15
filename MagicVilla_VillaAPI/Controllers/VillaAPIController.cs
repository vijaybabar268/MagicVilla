using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]        
        public IEnumerable<VillaDto> GetVillas()
        {
            var villas = new List<VillaDto>()
            {
                new VillaDto {Id=1,Name="Pool View"},
                new VillaDto {Id=2,Name="Beach View"}
            };

            return villas;
        }
    }
}
