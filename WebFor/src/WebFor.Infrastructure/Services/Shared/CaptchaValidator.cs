using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebFor.Core.Services.Shared;
using WebFor.Core.Types;

namespace WebFor.Infrastructure.Services.Shared
{
    public class CaptchaValidator : ICaptchaValidator
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;

        public CaptchaValidator(IHttpContextAccessor contextAccessor, HttpClient httpClient)
        {
            _contextAccessor = contextAccessor;
            _httpClient = httpClient;
        }

        public async Task<CaptchaResponse> ValidateCaptchaAsync(string secret)
        {
            var answer = _contextAccessor.HttpContext.Request.Form["g-recaptcha-response"];

            _httpClient.BaseAddress = new Uri("https://www.google.com/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync($"recaptcha/api/siteverify?secret={secret}&response={answer}");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResponse);
        }
    }
}
