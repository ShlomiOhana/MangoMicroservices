using Mango.Web.Models;
using Mango.Web.Services.IService;
using Mango.Web.Utility;

namespace Mango.Web.Services
{
	public class CouponService : ICouponService
	{
		private readonly IBaseService _baseService;

		public CouponService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> GetCouponAsync(string couponCode)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponAPIBase + "coupon/GetByCode/" + couponCode,
			});
		}

		public async Task<ResponseDto?> GetAllCouponsAsync()
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponAPIBase + "coupon",
			});
		}

		public async Task<ResponseDto?> GetCouponByIdAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponAPIBase + "coupon/" + id.ToString(),
			});
		}

		public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.POST,
				Url = SD.CouponAPIBase + "coupon/",
				Data = couponDto,
			});
		}

		public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.PUT,
				Url = SD.CouponAPIBase + "coupon/",
				Data = couponDto,
			});
		}

		public async Task<ResponseDto?> DeleteCouponByIdAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.DELETE,
				Url = SD.CouponAPIBase + "coupon/" + id.ToString(),
			});
		}
	}
}