using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GetWeatherLocation
{
    public class APIManager
    {
        public async static Task<RootObject> GetWeather(double lat, double lon)
        {
            var http = new HttpClient();
            var url = string.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=metric&appid=96381a872b1b405c5bf83b2ed63d9561", lat, lon);
            var respone = await http.GetAsync(url); // nhan data json tu weathermap
            var result = await respone.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            // khoi tao stream de doc json
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            //doc object da phan tich duoc tu json vao stream local de phan tich
            var data = (RootObject)serializer.ReadObject(ms);


            return data;
        }


        [DataContract]
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }
        [DataContract]
        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        [DataContract]
        public class Main
        {
            public int temp { get; set; }
            public double feels_like { get; set; }
            public int temp_min { get; set; }
            public int temp_max { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
        }
        [DataContract]
        public class Wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
        }
        [DataContract]
        public class Clouds
        {
            public int all { get; set; }
        }
        [DataContract]
        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }
        [DataContract]
        public class RootObject
        {
            public Coord coord { get; set; }
            [DataMember]
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            [DataMember]
            public Main main { get; set; }
            public int visibility { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public int timezone { get; set; }
            public int id { get; set; }
            [DataMember]
            public string name { get; set; }
            public int cod { get; set; }
        }

    }
}
