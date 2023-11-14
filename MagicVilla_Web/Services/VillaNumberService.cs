using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly string _villaUrl;

        public VillaNumberService(IHttpClientFactory httpClient, IConfiguration config)
            : base(httpClient)
        {
            _httpClient = httpClient;
            _villaUrl = config.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.POST,
                Data = dto,
                Url = _villaUrl + "/api/VillaNumber"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.DELETE,
                Url = _villaUrl + "/api/VillaNumber/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.GET,
                Url = _villaUrl + "/api/VillaNumber"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.GET,
                Url = _villaUrl + "/api/VillaNumber/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetail.ApiType.PUT,
                Data = dto,
                Url = _villaUrl + "/api/VillaNumber/" + dto.VillaNo
            });
        }
    }
}
