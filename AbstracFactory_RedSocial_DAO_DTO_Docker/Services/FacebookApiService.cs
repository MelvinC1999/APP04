using DotNetOpenAuth.AspNet.Clients;
using Facebook;



namespace AbstracFactory_RedSocial_DAO_DTO_Docker.Services
{
    public class FacebookApiService
    {
        //public dynamic GetDataFacebook(string accessToken)
        //{
        //    try
        //    {
        //        var client = new Facebook.FacebookClient(accessToken);

        //        // Realiza una solicitud a la Graph API de Facebook para obtener los datos que necesitas
        //        dynamic result = client.Get("me?fields=id,name"); // Ejemplo de solicitud para obtener ID, nombre y correo electrónico del usuario

        //        return result;
        //    }
        //    catch (FacebookOAuthException ex)
        //    {
        //        // Maneja las excepciones de autenticación de Facebook
        //        Console.WriteLine($"Error de autenticación de Facebook: {ex.Message}");
        //        throw;
        //    }
        //    catch (FacebookApiException ex)
        //    {
        //        // Maneja otras excepciones de la API de Facebook
        //        Console.WriteLine($"Error de la API de Facebook: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Maneja otras excepciones
        //        Console.WriteLine($"Error: {ex.Message}");
        //        throw;
        //    }
        //}
        //******************************************************************************************************
        //public dynamic GetFacebookPosts(string accessToken)
        //{
        //    try
        //    {
        //        var client = new Facebook.FacebookClient(accessToken);

        //        // Realiza una solicitud a la Graph API de Facebook para obtener los posts del usuario
        //        dynamic result = client.Get("me?fields=id,name,posts");

        //        return result;
        //    }
        //    catch (FacebookOAuthException ex)
        //    {
        //        // Maneja las excepciones de autenticación de Facebook
        //        Console.WriteLine($"Error de autenticación de Facebook: {ex.Message}");
        //        throw;
        //    }
        //    catch (FacebookApiException ex)
        //    {
        //        // Maneja otras excepciones de la API de Facebook
        //        Console.WriteLine($"Error de la API de Facebook: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Maneja otras excepciones
        //        Console.WriteLine($"Error: {ex.Message}");
        //        throw;
        //    }
        //}
        //******************************************************************************************************
        //public dynamic GetFacebookEvents(string pageId, string accessToken)
        //{
        //    try
        //    {
        //        var client = new Facebook.FacebookClient(accessToken);

        //        // Realiza una solicitud a la Graph API de Facebook para obtener los eventos de la página
        //        dynamic result = client.Get(pageId + "/events");

        //        return result;
        //    }
        //    catch (FacebookOAuthException ex)
        //    {
        //        // Maneja las excepciones de autenticación de Facebook
        //        Console.WriteLine($"Error de autenticación de Facebook: {ex.Message}");
        //        throw;
        //    }
        //    catch (FacebookApiException ex)
        //    {
        //        // Maneja otras excepciones de la API de Facebook
        //        Console.WriteLine($"Error de la API de Facebook: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Maneja otras excepciones
        //        Console.WriteLine($"Error: {ex.Message}");
        //        throw;
        //    }
        //}
        //******************************************************************************************************
        public dynamic GetFacebookPosts(string accessToken)
        {
            try
            {
                var client = new Facebook.FacebookClient(accessToken);

                // Realiza una solicitud a la Graph API de Facebook para obtener los posts del usuario
                dynamic result = client.Get("me?fields=id,name,posts.limit(10){message,likes.summary(true).limit(0),comments.summary(true).limit(0)}");

                // Crea una lista para almacenar la información de los posts
                var posts = new List<dynamic>();

                // Añade la información del canal al principio de la lista
                posts.Add(new { Name = result.name, Description = result.description, PostCount = result.posts.data.Count, Followers = result.followers_count });

                // Añade la información de cada post a la lista
                foreach (var post in result.posts.data)
                {
                    posts.Add(new { Likes = post.likes.summary.total_count, Comments = post.comments.summary.total_count, Title = post.message });
                }

                return posts;
            }
            catch (FacebookOAuthException ex)
            {
                // Maneja las excepciones de autenticación de Facebook
                Console.WriteLine($"Error de autenticación de Facebook: {ex.Message}");
                throw;
            }
            catch (FacebookApiException ex)
            {
                // Maneja otras excepciones de la API de Facebook
                Console.WriteLine($"Error de la API de Facebook: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Maneja otras excepciones
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
