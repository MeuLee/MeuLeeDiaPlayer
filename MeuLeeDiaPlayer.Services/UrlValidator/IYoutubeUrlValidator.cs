namespace MeuLeeDiaPlayer.Services.UrlValidator
{
    public interface IYoutubeUrlValidator
    {
        YoutubeUrlType GetYoutubeUrlType(string input);
    }
}
