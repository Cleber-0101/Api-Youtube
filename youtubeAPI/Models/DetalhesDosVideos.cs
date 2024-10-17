namespace youtubeAPI.Models
{
    public class DetalhesDosVideos
    {
        //pode ser que os dados venham nulos
        public string? Title { get; set; }
        public string? Descricao { get; set; }
        public string?  Link { get; set; }
        public string? tumb { get; set; }
        public DateTime? publicacao { get; set; }
    }
}
