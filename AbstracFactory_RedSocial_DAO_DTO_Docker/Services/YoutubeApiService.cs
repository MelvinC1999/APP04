
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

        //public async Task<string> GetChannelInfo(string channelId)
        //{
        //    var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        //    {
        //        ApiKey = _apiKey,
        //        ApplicationName = "LocalNear"
        //    });

        //    var channelsListRequest = youtubeService.Channels.List("snippet,contentDetails,statistics");
        //    channelsListRequest.Id = channelId;

        //    var channelsListResponse = await channelsListRequest.ExecuteAsync();

        //    foreach (var channel in channelsListResponse.Items)
        //    {
        //        // Aquí puedes acceder a información sobre el canal, como su título, descripción, etc.
        //        Console.WriteLine($"Título: {channel.Snippet.Title}");
        //        Console.WriteLine($"Descripción: {channel.Snippet.Description}");
        //        Console.WriteLine($"Suscriptores: {channel.Statistics.SubscriberCount}");
        //    }

        //    return "Hecho";
        //}
        //*****************************************************************************************************

        //public async Task<string> GetChannelVideos(string channelId)
        //{
        //    var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        //    {
        //        ApiKey = _apiKey,
        //        ApplicationName = "LocalNear"
        //    });

        //    // Primero, obtenemos el ID de la lista de reproducción que representa las subidas del canal
        //    var channelsListRequest = youtubeService.Channels.List("contentDetails");
        //    channelsListRequest.Id = channelId;
        //    var channelsListResponse = await channelsListRequest.ExecuteAsync();
        //    var uploadsListId = channelsListResponse.Items[0].ContentDetails.RelatedPlaylists.Uploads;

        //    // Luego, obtenemos todos los videos de la lista de reproducción
        //    var nextPageToken = "";
        //    while (nextPageToken != null)
        //    {
        //        var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet,contentDetails");
        //        playlistItemsListRequest.PlaylistId = uploadsListId;
        //        playlistItemsListRequest.MaxResults = 10; // Máximo permitido por la API de YouTube
        //        playlistItemsListRequest.PageToken = nextPageToken;

        //        var playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();

        //        foreach (var playlistItem in playlistItemsListResponse.Items)
        //        {
        //            // Aquí puedes acceder a información sobre cada video, como su título, descripción, etc.
        //            Console.WriteLine($"Título: {playlistItem.Snippet.Title}");
        //            //Console.WriteLine($"Descripción: {playlistItem.Snippet.Description}");

        //            // Para obtener las vistas y likes de cada video, necesitamos hacer una solicitud adicional
        //            var videosListRequest = youtubeService.Videos.List("statistics");
        //            videosListRequest.Id = playlistItem.ContentDetails.VideoId;
        //            var videosListResponse = await videosListRequest.ExecuteAsync();

        //            Console.WriteLine($"Vistas: {videosListResponse.Items[0].Statistics.ViewCount}");
        //            Console.WriteLine($"Likes: {videosListResponse.Items[0].Statistics.LikeCount}");
        //        }

        //        nextPageToken = playlistItemsListResponse.NextPageToken;
        //    }

        //    return "Hecho";
        //}
        //*****************************************************************************************************
        public async Task<List<string>> GetChannelVideos(string channelId)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "LocalNear"
            });

            var channelsListRequest = youtubeService.Channels.List("snippet,contentDetails,statistics");
            channelsListRequest.Id = channelId;
            var channelsListResponse = await channelsListRequest.ExecuteAsync();
            var uploadsListId = channelsListResponse.Items[0].ContentDetails.RelatedPlaylists.Uploads;

            var channelData = new List<string>
                {
                    $"Nombre del canal: {channelsListResponse.Items[0].Snippet.Title}",
                    $"Descripción del canal: {channelsListResponse.Items[0].Snippet.Description}",
                    $"Número de videos: {channelsListResponse.Items[0].Statistics.VideoCount}",
                    $"Número de suscriptores: {channelsListResponse.Items[0].Statistics.SubscriberCount}"
                };

            var nextPageToken = "";
            var videoCount = 0;
            while (nextPageToken != null && channelData.Count <= 10)
            {
                var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet,contentDetails");
                playlistItemsListRequest.PlaylistId = uploadsListId;
                playlistItemsListRequest.MaxResults = 10;
                playlistItemsListRequest.PageToken = nextPageToken;

                var playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();

                foreach (var playlistItem in playlistItemsListResponse.Items)
                {
                    var videosListRequest = youtubeService.Videos.List("snippet,statistics");
                    videosListRequest.Id = playlistItem.ContentDetails.VideoId;
                    var videosListResponse = await videosListRequest.ExecuteAsync();

                    channelData.Add($"{videosListResponse.Items[0].Snippet.Title}");
                    channelData.Add($"{videosListResponse.Items[0].Statistics.LikeCount}");
                    channelData.Add($"{videosListResponse.Items[0].Statistics.CommentCount}");
                    channelData.Add($"https://www.youtube.com/watch?v={playlistItem.ContentDetails.VideoId}");

                    videoCount++;

                    if (videoCount >= 10)
                    {
                        break;
                    }
                }

                nextPageToken = playlistItemsListResponse.NextPageToken;
            }

            var json = JsonConvert.SerializeObject(channelData, Formatting.Indented);
            Console.WriteLine(json);

            return channelData;
        }
    }
}
