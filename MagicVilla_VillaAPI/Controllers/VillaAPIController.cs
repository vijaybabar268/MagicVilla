﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("villas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Start: GetVillas() Invoke");
            
            var villas = await _db.Villas.ToListAsync();
            if (villas.Count == 0)
            {
                _logger.LogInformation("We don't have any villa yet.");
                return NoContent();
            }                

            _logger.LogInformation("Finish: GetVillas() Count:"+ villas.Count);
            return Ok(villas);
        }

        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
                return NotFound();

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody]VillaCreateDto villaDto)
        {
            if (villaDto == null) return BadRequest();

            //if (villaDto.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);

            if(_db.Villas.FirstOrDefault(u=>u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("customError", "Villa already exists!");
                return BadRequest(ModelState);
            }

            var model = new Villa
            {
                Name = villaDto.Name,
                Details = villaDto.Details,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
                Occupancy = villaDto.Occupancy,
                ImageUrl = villaDto.ImageUrl,
                Amenity = villaDto.Amenity,
                CreatedDate = DateTime.UtcNow
            };

            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

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

            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
                return NotFound();

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDto villaDto)
        {
            if (villaDto == null || id != villaDto.Id || id <= 0) return BadRequest();

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
                return NotFound();

            villa.Name = villaDto.Name;
            villa.Details = villaDto.Details;
            villa.Rate = villaDto.Rate;
            villa.Sqft = villaDto.Sqft;
            villa.Occupancy = villa.Occupancy;
            villa.ImageUrl = villaDto.ImageUrl;
            villa.Amenity = villaDto.Amenity;

            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
