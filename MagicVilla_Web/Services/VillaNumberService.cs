using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly string _villaUrl;

        public VillaNumberService(IHttpClientFactory httpClient, IConfiguration config)
            : base(httpClient)
        {
            _villaUrl = config.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.POST,
                Data = dto,
                Url = _villaUrl + "/api/VillaNumber",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.DELETE,
                Url = _villaUrl + "/api/VillaNumber/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.GET,
                Url = _villaUrl + "/api/VillaNumber",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.GET,
                Url = _villaUrl + "/api/VillaNumber/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.PUT,
                Data = dto,
                Url = _villaUrl + "/api/VillaNumber/" + dto.VillaNo,
                Token = token
            });
        }
    }
}
