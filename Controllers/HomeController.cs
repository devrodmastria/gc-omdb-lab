using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenMovieCatalog.Models;
using System.Diagnostics;
using System.Net;

namespace OpenMovieCatalog.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MovieSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MovieSearch(string newMovie, string showGenre) 
        {

            MovieModel movie = GetMovie(newMovie);
            GenreModel gm = new GenreModel();
            gm.Movie = movie;
            gm.ShowGenre = (showGenre == "false" ? true : false);
            return View(gm);
        }

        //[HttpGet] //I tried to follow the lab guidelines but the HttpGet/HttpPost tags broke my MovieNight view
        public IActionResult MovieNight(string m1, string m2, string m3) 
        {
            return View(
                new List<MovieModel>() { GetMovie(m1), GetMovie(m2), GetMovie(m3) });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private MovieModel GetMovie(string newTitle)
        {
            string key = ""; //API KEY shared in LMS
            string url = $"http://www.omdbapi.com/?apikey={key}&t={newTitle}";

            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string JSON = reader.ReadToEnd();
            MovieModel result = JsonConvert.DeserializeObject<MovieModel>(JSON);

            return result;
        }

    }
}
