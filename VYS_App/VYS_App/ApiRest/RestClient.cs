using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace VYS_App.ApiRest
{
    class RestClient
    {
        public async Task<T> Get<T>(String url)
        {

            try
            {
                HttpClient client = new HttpClient();
                //var response = await client.GetAsync("https://vys-server.herokuapp.com/canciones");
                var response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jasonString = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jasonString);
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
            return default(T);
        }

        public async Task<T> Post<T>(String url, T contenido)
        {
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(contenido);
            //var data = Newtonsoft.Json.JsonConvert.SerializeObject(new { _id = contenido._id , titulo = contenido.titulo });
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            string contenidoString = content.ToString();
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.PostAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jasonString = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jasonString);
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
            return default(T);
        }
    }
}
