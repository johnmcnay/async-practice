using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using async_practice_thing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace async_practice_thing.Controllers
{
    public class HomeController : Controller
    {

        private static readonly HttpClient HttpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        private async Task<string> CallEndpoint(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        public async Task<int> RandomNumber()
        {
            return Convert.ToInt32(await CallEndpoint("https://seriouslyfundata.azurewebsites.net/api/generatearandomnumber"));
        }

        public async Task<string> ChuckNorris()
        {
            return await CallEndpoint("https://seriouslyfundata.azurewebsites.net/api/chucknorrisfact");
        }

        public async Task<string> Seleucids()
        {
            return await CallEndpoint("https://seriouslyfundata.azurewebsites.net/api/seleucids");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
