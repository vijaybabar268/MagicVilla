using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;

        public VillaAPIController(ILogger<VillaAPIController> logger, IVillaRepository dbVilla, IMapper mapper)
        {
            _logger = logger;
            _dbVilla = dbVilla;
            _mapper = mapper;
        }

        [HttpGet("villas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Start: GetVillas() Invoke");
            
            var villas = await _dbVilla.GetAllAsync();
            if (villas.Count() == 0)
            {
                _logger.LogInformation("We don't have any villa yet.");
                return NoContent();
            }                

            _logger.LogInformation("Finish: GetVillas() Count:"+ villas.Count());
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villas));
        }

        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = await _dbVilla.GetAsync(v => v.Id == id);
            if (villa == null)
                return NotFound();

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody]VillaCreateDto createDto)
        {
            if (createDto == null) return BadRequest();

            if(_dbVilla.GetAsync(u=>u.Name.ToLower() == createDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("customError", "Villa already exists!");
                return BadRequest(ModelState);
            }

            Villa model = _mapper.Map<Villa>(createDto);

            await _dbVilla.CreateAsync(model);

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if (villa == null)
                return NotFound();

            await _dbVilla.RemoveAsync(villa);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id || id <= 0) return BadRequest();

            Villa model = _mapper.Map<Villa>(updateDto);

            await _dbVilla.UpdateAsync(model);

            return NoContent();
        }

    }
}
