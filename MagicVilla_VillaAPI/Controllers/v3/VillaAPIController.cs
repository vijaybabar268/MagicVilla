using Asp.Versioning;
using AutoMapper;
using MagicVilla_VillaAPI.Controllers.v1;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers.v3
{
    [ApiVersion("3.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]    
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<v1.VillaAPIController> _logger;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaAPIController(ILogger<v1.VillaAPIController> logger, IVillaRepository dbVilla, IMapper mapper)
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
