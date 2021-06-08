using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using async_practice_thing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace async_practice_thing.Controllers
{
    public class HomeController : Controller
    {

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
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        public async Task<IActionResult> SingleTeacher()
        {

            string teacherStr = await CallEndpoint("https://seriouslyfundata.azurewebsites.net/api/ateacher");

            using (var stringReader = new System.IO.StringReader(teacherStr))
            {
                var serializer = new XmlSerializer(typeof(Teacher));
                var ret = serializer.Deserialize(stringReader) as Teacher;

                ViewData["Name"] = ret.Name;
                ViewData["HomeState"] = ret.HomeState;

                return View();
            }
        }

        public async Task<IActionResult> GetTeachers()
        {
            string teacherStr = await CallEndpoint("https://seriouslyfundata.azurewebsites.net/api/yourteachers");

            using (var stringReader = new System.IO.StringReader(teacherStr))
            {
                var serializer = new XmlSerializer(typeof(List<Teacher>));
                var ret = serializer.Deserialize(stringReader) as List<Teacher>;

                ViewBag.teachers = ret;

                ViewData["Teachers"] = ret;

                return View();
            }
        }

        public async Task<IActionResult> RandomNumber()
        {
            string randomNumber = await CallEndpoint("https://seriouslyfundata.azurewebsites.net/api/generatearandomnumber");


            ViewData["randomNumber"] = randomNumber;

            return View();
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

    public class Teacher
    {
        public string Name { get; set; }
        public string HomeState { get; set; }
    }
}
