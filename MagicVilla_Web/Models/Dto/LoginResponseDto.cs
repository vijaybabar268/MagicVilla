using MagicVilla_Web.Models.Dto;

namespace MagicVilla_VillaWeb.Models.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
