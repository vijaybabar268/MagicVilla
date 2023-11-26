using MagicVilla_VillaWeb.Models.Dto;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto model);
        Task<T> RegisterAsync<T>(UserDto model);
    }
}
