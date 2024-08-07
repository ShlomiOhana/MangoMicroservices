using Mango.Web.Models;
using Mango.Web.Services.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
            HttpRequestMessage msg = new();
            msg.Headers.Add("Accept", "application/json");

            // Token

            msg.RequestUri = new Uri(requestDto.Url);
            if (requestDto.Data != null)
            {
                msg.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            switch (requestDto.ApiType)
            {
                case ApiType.GET:
                    msg.Method = HttpMethod.Get;
                    break;
                case ApiType.POST:
                    msg.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    msg.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    msg.Method = HttpMethod.Delete;
                    break;
                default:
                    break;
            }

            HttpResponseMessage apiResponse = await client.SendAsync(msg);
            try
            {
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDto { IsSuccess = false, Message = "Forbidden" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto { IsSuccess = false, Message = "InternalServerError" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto { IsSuccess = false, Message = "Unauthorized" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dtoResponse = new ResponseDto
                {
                    Message = ex.Message,
                    IsSuccess = false
                };

                return dtoResponse;
            }
        }
    }
}
