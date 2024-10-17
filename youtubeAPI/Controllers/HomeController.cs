using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using youtubeAPI.Models;

namespace youtubeAPI.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {

            var videos = await BuscarVideoDoCanalCasemiro();
            return View(videos);
        }

        private async Task<List<DetalhesDosVideos>> BuscarVideoDoCanalCasemiro()
        {
            var youtubeServices = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "testeeeee",
                ApplicationName = "YoutubeApiVideo"
            });

            var searchRequest = youtubeServices.Search.List("snippet");
            searchRequest.ChannelId = "@CortesdoCasimitoOFICIAL";
            //organizar pela ordem de Data 
            searchRequest.Order  = SearchResource.ListRequest.OrderEnum.Date;
            searchRequest.MaxResults = 20;

            //executa a solicitação , usei o await porque pode ser que demore 
            var searchResponse = await searchRequest.ExecuteAsync();

            List<DetalhesDosVideos> videoList = searchResponse.Items.Select(item =>
                                      new DetalhesDosVideos
                                      {
                                          Titulo = item.Snippet.Title,
                                          Descricao = item.Snippet.Description,
                                          tumb = item.Snippet.Thumbnails.Medium.Url,
                                          Link = $"https://www.youtube.com/watch?v={item.Id.VideoId}",
                                          publicacao = item.Snippet.PublishedAt
                                      }
            ).OrderByDescending(video
             => video.publicacao)
             .ToList();

            return videoList;
        }

    }
}
