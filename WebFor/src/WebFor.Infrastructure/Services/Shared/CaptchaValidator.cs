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

        public CaptchaValidator(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<CaptchaResponse> ValidateCaptchaAsync(string secret)
        {
            using (var client = new HttpClient())
            {
                var answer = _contextAccessor.HttpContext.Request.Form["g-recaptcha-response"];

                client.BaseAddress = new Uri("https://www.google.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"recaptcha/api/siteverify?secret={secret}&response={answer}");

                //if (response.IsSuccessStatusCode) { }

                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResponse);
            }
        }
    }
}
