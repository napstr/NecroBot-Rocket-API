using System.Net;
using System.Net.Http;
using PokemonGo.RocketAPI.Helpers;

namespace PokemonGo.RocketAPI.HttpClient
{
    public class PokemonHttpClient : System.Net.Http.HttpClient
    {
        /*private static readonly HttpClientHandler Handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            AllowAutoRedirect = false,
            UseProxy = Client.Proxy != null,
            Proxy = Client.Proxy
        };*/
        
        //fix below from https://github.com/NECROBOTIO/NecroBot/issues/3289#issuecomment-238615898
        static PokemonHttpClient()
        {
            Handler = new HttpClientHandler();

            Handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            Handler.AllowAutoRedirect = false;

            if (Client.Proxy != null)
            {
                Handler.UseProxy = true;
                Handler.Proxy = Client.Proxy;
            }
        }
        private static readonly HttpClientHandler Handler;

        public PokemonHttpClient() : base(new RetryHandler(Handler))
        {
            DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Niantic App");
            DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
            DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

            DefaultRequestHeaders.ExpectContinue = false;
        }
    }
}
