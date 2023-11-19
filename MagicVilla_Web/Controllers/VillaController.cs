using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDto> list = new();

            var response = await _villaService.GetAllAsync<APIResponse>();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(response.Result.ToString());
            }

            return View(list);
        }

        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDto model)
        {
            if(ModelState.IsValid)
            {
                var response = await _villaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Error encountered.";
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());

                    }
                }
            }

            TempData["error"] = "Error encountered.";
            return View(model);
        }

        public async Task<IActionResult> EditVilla(int id)
        {
            var response = await _villaService.GetAsync<APIResponse>(id);
            if(response != null && response.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaDto>(response.Result.ToString());
                return View(_mapper.Map<VillaUpdateDto>(villa));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVilla(VillaUpdateDto model)
        {            
            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<APIResponse>(model);
                if(response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Error encountered.";
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());

                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteVilla(int id)
        {
            var response = await _villaService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaDto>(response.Result.ToString());
                return View(villa);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDto model)
        {
            var response = await _villaService.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
    }
}
