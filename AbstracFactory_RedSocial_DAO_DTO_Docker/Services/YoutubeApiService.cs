
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AbstracFactory_RedSocial_DAO_DTO_Docker.Services
{
    public class YouTubeApiService
    {
        private string _apiKey;

        public YouTubeApiService(string apiKey)
        {
            _apiKey = apiKey;
        }

        // Método asincrónico que obtiene una lista de videos de un canal de YouTube dado su channelId
        public async Task<List<string>> GetChannelVideos(string channelId)
        {
            // Inicialización del servicio de YouTube con la clave de API y el nombre de la aplicación
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey, // Clave de API para acceder a la API de YouTube
                ApplicationName = "LocalNear" // Nombre de la aplicación que utiliza la API de YouTube
            });

            // Creación de una solicitud para obtener información del canal incluyendo detalles de fragmentos, detalles de contenido y estadísticas
            var channelsListRequest = youtubeService.Channels.List("snippet,contentDetails,statistics");
            channelsListRequest.Id = channelId; // Especificación del ID del canal para la solicitud
                                                // Ejecución de la solicitud de obtener información del canal de forma asíncrona
            var channelsListResponse = await channelsListRequest.ExecuteAsync();

            // Obtención del ID de la lista de reproducción de uploads del canal
            var uploadsListId = channelsListResponse.Items[0].ContentDetails.RelatedPlaylists.Uploads;

            // Inicialización de una lista para almacenar los datos del canal y los videos
            var channelData = new List<string>
    {
                $"Nombre del canal: {channelsListResponse.Items[0].Snippet.Title}", // Nombre del canal
                $"Descripción del canal: {channelsListResponse.Items[0].Snippet.Description}", // Descripción del canal
                $"Número de videos: {channelsListResponse.Items[0].Statistics.VideoCount}", // Número de videos del canal
                $"Número de suscriptores: {channelsListResponse.Items[0].Statistics.SubscriberCount}" // Número de suscriptores del canal
    };

            // Declaración e inicialización de variables para el manejo de la paginación y el conteo de videos
            var nextPageToken = "";
            var videoCount = 0;

            // Ciclo para obtener los videos del canal mientras haya un token de página siguiente y no se superen los 45 videos
            while (nextPageToken != null && channelData.Count <= 45)
            {
                // Creación de una solicitud para obtener elementos de la lista de reproducción de uploads del canal
                var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet,contentDetails");
                playlistItemsListRequest.PlaylistId = uploadsListId; // Especificación del ID de la lista de reproducción
                playlistItemsListRequest.MaxResults = 45; // Especificación del número máximo de resultados por página
                playlistItemsListRequest.PageToken = nextPageToken; // Token de página para la paginación

                // Ejecución de la solicitud de obtener elementos de la lista de reproducción de forma asíncrona
                var playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();

                // Iteración sobre los elementos de la lista de reproducción obtenida
                foreach (var playlistItem in playlistItemsListResponse.Items)
                {
                    // Creación de una solicitud para obtener detalles de videos
                    var videosListRequest = youtubeService.Videos.List("snippet,statistics");
                    videosListRequest.Id = playlistItem.ContentDetails.VideoId; // Especificación del ID del video
                                                                                // Ejecución de la solicitud de obtener detalles de videos de forma asíncrona
                    var videosListResponse = await videosListRequest.ExecuteAsync();

                    // Agregado de información del video a la lista de datos del canal
                    channelData.Add($"{videosListResponse.Items[0].Snippet.Title}"); // Título del video
                    channelData.Add($"{videosListResponse.Items[0].Statistics.LikeCount}"); // Cantidad de "me gusta" del video
                    channelData.Add($"{videosListResponse.Items[0].Statistics.CommentCount}"); // Cantidad de comentarios del video

                    // Creación de una solicitud para obtener hilos de comentarios del video
                    var commentThreadsRequest = youtubeService.CommentThreads.List("snippet");
                    commentThreadsRequest.VideoId = playlistItem.ContentDetails.VideoId; // Especificación del ID del video
                                                                                         // Ejecución de la solicitud de obtener hilos de comentarios de forma asíncrona
                    var commentThreadsResponse = await commentThreadsRequest.ExecuteAsync();

                    // Iteración sobre los hilos de comentarios obtenidos y agregado a la lista de datos del canal
                    foreach (var commentThread in commentThreadsResponse.Items)
                    {
                        var commentSnippet = commentThread.Snippet.TopLevelComment.Snippet;
                        channelData.Add($"Comentario: {commentSnippet.TextDisplay}"); // Texto del comentario
                    }

                    // Agregado de la URL del video a la lista de datos del canal
                    channelData.Add($"https://www.youtube.com/watch?v={playlistItem.ContentDetails.VideoId}");

                    videoCount++; // Incremento del contador de videos

                    // Si se alcanza o supera el límite de 45 videos, se rompe el ciclo
                    if (videoCount >= 45)
                    {
                        break;
                    }
                }

                // Actualización del token de página siguiente para la paginación
                nextPageToken = playlistItemsListResponse.NextPageToken;
            }

            // Serialización de la lista de datos del canal a formato JSON
            var json = JsonConvert.SerializeObject(channelData, Formatting.Indented);
            // Impresión del JSON en la consola
            Console.WriteLine(json);

            // Devolución de la lista de datos del canal
            return channelData;
        }


    }
}
