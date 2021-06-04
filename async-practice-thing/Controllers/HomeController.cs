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

        public async Task<int> RandomNumber()
        {

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://seriouslyfundata.azurewebsites.net/api/generatearandomnumber");

            var responseString = await response.Content.ReadAsStringAsync();

            return Convert.ToInt32(responseString);
        }

        public async Task<string> ChuckNorris()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://seriouslyfundata.azurewebsites.net/api/chucknorrisfact");

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;

        }

        public async Task<string> Seleucids()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://seriouslyfundata.azurewebsites.net/api/seleucids");

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;

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
