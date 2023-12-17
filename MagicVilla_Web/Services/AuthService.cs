using MagicVilla_Utility;
using MagicVilla_VillaWeb.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string _villaUrl; 

        public AuthService(IHttpClientFactory clientFactory, IConfiguration config)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
            _villaUrl = config.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDto model)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.POST,
                Data = model,
                Url = _villaUrl + "/api/v1/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(UserDto model)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.POST,
                Data = model,
                Url = _villaUrl + "/api/v1/UsersAuth/register"
            });
        }
    }
}
