using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.VM
{
    public class VillaNumberCreateVM
    {
        public VillaNumberCreateVM()
        {
            VillaNumber = new VillaNumberCreateDto();
        }

        public VillaNumberCreateDto VillaNumber { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
