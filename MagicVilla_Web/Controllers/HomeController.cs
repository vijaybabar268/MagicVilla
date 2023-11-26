using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MagicVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;


        public HomeController(IVillaService villaService)
        {
            _villaService = villaService;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDto> getAllVillas = new List<VillaDto>();
           
            var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetail.SessionToken));
            if (response != null && response.IsSuccess) 
            {
                getAllVillas = JsonConvert.DeserializeObject<List<VillaDto>>(response.Result.ToString());                
            }

            return View(getAllVillas);
        }
    }
}