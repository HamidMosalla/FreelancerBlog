using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Queries.Services.Shared;
using FreelancerBlog.Core.Types;
using MediatR;
using FreelancerBlog.Data.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FreelancerBlog.Services.Queries.Shared
{
    public class ValidateCaptchaQueryHandler : AsyncRequestHandler<ValidateCaptchaQuery, CaptchaResponse>
    {
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;

        public ValidateCaptchaQueryHandler(IConfiguration configuration, IHttpContextAccessor contextAccessor, HttpClient httpClient)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _httpClient = httpClient;
        }

        protected override async Task<CaptchaResponse> HandleCore(ValidateCaptchaQuery message)
        {
            var secret = _configuration.GetValue<string>("reChaptchaSecret:server-secret");
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