using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public HomeController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDto> getAllVillas = new List<VillaDto>();
           
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) 
            {
                getAllVillas = JsonConvert.DeserializeObject<List<VillaDto>>(response.Result.ToString());                
            }

            return View(getAllVillas);
        }
    }
}