using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]    
    [ApiVersion("3.0")]
    public class VillaAPIv3Controller : ControllerBase
    {
        private readonly ILogger<VillaAPIv1Controller> _logger;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaAPIv3Controller(ILogger<VillaAPIv1Controller> logger, IVillaRepository dbVilla, IMapper mapper)
        {
            _logger = logger;
            _dbVilla = dbVilla;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("villas")]
        public async Task<string> GetThird()
        {
            return "Hello world from version 3.0";
        }
    }
}
