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
                ApiKey = "AIzaSyB_fLZcJiXvruTZQL2Ic4u9uYgbUkRuOH4",  // Insira a chave válida aqui
                ApplicationName = "YoutubeApiVideo"
            });

            var searchRequest = youtubeServices.Search.List("snippet");
            searchRequest.ChannelId = "UCwVzFkXszeP__iWXNV2EjhA";  // Insira o ID correto do canal
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            searchRequest.MaxResults = 10;

            try
            {
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
                ).OrderByDescending(video => video.publicacao).ToList();

                return videoList;
            }
            catch (Google.GoogleApiException ex)
            {
                // Log do erro
                Console.WriteLine($"Erro ao buscar vídeos: {ex.Message}");
                return new List<DetalhesDosVideos>(); // Retorna uma lista vazia em caso de erro
            }
        }
    }
}
