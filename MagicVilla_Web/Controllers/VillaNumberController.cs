﻿using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.VM;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IMapper _mapper;
        private readonly IVillaService _villaService;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaNumberDto> list = new();

            var response = await _villaNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDto>>(response.Result.ToString());
            }

            return View(list);
        }

        public async Task<IActionResult> CreateVillaNumbers()
        {
            VillaNumberCreateVM villaNumberVM = new();

            var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(response.Result.ToString())
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(villaNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumbers(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(StaticDetail.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa number created successfully";
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

            var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(resp.Result.ToString())
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(model);
        }


        public async Task<IActionResult> EditVillaNumbers(int id)
        {
            VillaNumberEditVM villaNumberVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (response != null && response.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaNumberDto>(response.Result.ToString());
                villaNumberVM.VillaNumber = _mapper.Map<VillaNumberUpdateDto>(villa);
            }

            var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(resp.Result.ToString())
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVillaNumbers(VillaNumberEditVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(StaticDetail.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa number updated successfully";
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

            var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(resp.Result.ToString())
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteVillaNumbers(int id)
        {
            VillaNumberDeleteVM villaNumberVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (response != null && response.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaNumberDto>(response.Result.ToString());
                villaNumberVM.VillaNumber = villa;
            }

            var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(resp.Result.ToString())
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumbers(VillaNumberDeleteVM model)
        {
            var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo, HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa number deleted successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
