using AbstracFactory_RedSocial_DAO_DTO_Docker.Services;
using LinqToTwitter;

namespace AbstracFactory_RedSocial_DAO_DTO_Docker.DataAccessObject
{
    public class DAO_RedeSociales
    {
        public void FeachtDataFacebook()
        {
            var accessToken = "EAAXDQwSka2QBO5UFEREx1e4VqZBfECZBRyz3U4LWAzvESlSSK2p4GR9Lplbm4jA6nV2lCGQsZBLjJpZAERbAjumojqQo74nVU26xL2ZCiAKaewOIr2yZBfILrmRqHcCrB3YTB5YPT16x57NZAiZAQInNlxABRh244v3cSOIPy2qN5qHqw3DnuGz2zyfIN5wiN6IZAeV86jsC4ZBsde8MDKnzGF48UnUn0i0cWDZAQ9ZA4sCjLZA2lMwnrlfleu0p3H8WEKkUtPuEwpgZDZD"; // Reemplaza esto con tu token de acceso real
            string IdPage = "61449504559";
            var facebookApiService = new FacebookApiService();
            var userName = facebookApiService.GetFacebookPosts(accessToken);
            
            Console.WriteLine($"-> Data User Facebook is: {userName}");
        }
        public async Task<List<string>> FeacthDataYoutube()
        {
            // INSTAGRAM var accessToken = "EABkFGRuP6WcBO2PTKXRlv83PQknRG08N4FdUCm9Gevhd34n69LMd8uymPPHVPrHnnRdg9dQdx3ZCJwE9EjayjmdnfVZBEfs9BjujB4dSyAqNHdIFuuxxNlzDDe5KaAtV3kxcSv1b1uoan3k8Ew5K8uXlp1k1AQdPVBOqv9qjZCGtuZBdkoQ21kzjDaLKm4Rk7aVT95lc69MJDUKnO6OkMDtBeBS05a8bBA4ka1JLEolY4Dlu2RKy6NUG5hIvfJKvfld1CgZDZD";
            var accessToken = "AIzaSyBPx1HVC_yy8pHjcescO3chVw1HEtbQSyg";
            var youtubeApiService = new YouTubeApiService(accessToken);
            List <string>listaContenido = await youtubeApiService.GetChannelVideos("UCLwBAR1YA6bQRNVCLYOM6Sg"); // UCph9U1dGNq4fqV3buJtaN3g // UC0WP5P-ufpRfjbNrmOWwLBQ
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
