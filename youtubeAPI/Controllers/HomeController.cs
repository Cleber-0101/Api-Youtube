using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using youtubeAPI.Models;

namespace youtubeAPI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        private async Task<List<DetalhesDosVideos>> BuscarVideoDoCanalCasemiro()
        {
            var youtubeServices = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "AIzaSyB_fLZcJiXvruTZQL2Ic4u9uYgbUkRuOH4",
                ApplicationName = "YoutubeApiVideo"
            });
        }

    }
}
