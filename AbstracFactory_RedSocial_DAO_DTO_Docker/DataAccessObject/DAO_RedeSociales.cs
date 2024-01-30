using AbstracFactory_RedSocial_DAO_DTO_Docker.Services;
using LinqToTwitter;

namespace AbstracFactory_RedSocial_DAO_DTO_Docker.DataAccessObject
{
    public class DAO_RedeSociales
    {
        public void FeachtDataFacebook()
        {
            var accessToken = ""; // Reemplaza esto con tu token de acceso real
            string IdPage = "61449504559";
            var facebookApiService = new FacebookApiService();
            var userName = facebookApiService.GetFacebookPosts(accessToken);
            
            Console.WriteLine($"-> Data User Facebook is: {userName}");
        }
        public async Task<List<string>> FeacthDataYoutube()
        {
            var accessToken = "AIzaSyBPx1HVC_yy8pHjcescO3chVw1HEtbQSyg";
            var youtubeApiService = new YouTubeApiService(accessToken);
            List <string>listaContenido = await youtubeApiService.GetChannelVideos("UCx6TdW_j_WKafftaMM-Ttzg"); //Televistazo
            //Console.WriteLine($">>>Youtube: {contenido} ");
            foreach (var item in listaContenido)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($">>>Youtube: {item}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine($"-> Data User Instagram is: {userName}");
            //return  userName;
            return listaContenido;
        }
    }
}
