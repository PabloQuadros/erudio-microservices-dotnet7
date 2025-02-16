﻿using GeekShopping.web.Models;
using GeekShopping.web.Services.IServices;
using GeekShopping.web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.web.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/Coupon";

        public CouponService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException();
        }
        public  async Task<CouponViewModel> GetCoupon(string code, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{code}");
            if(response.StatusCode != System.Net.HttpStatusCode.OK ) 
            {
                return new CouponViewModel();
            }
            return await response.ReadContentAs<CouponViewModel>();
        }
    }
}
