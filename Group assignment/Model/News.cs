﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Group_assignment.Model
{
    public class News
    {

        public class Content
        {
            public string description { get; set; }
            public string url { get; set; }
        }

        public class Root
        {
            public int id { get; set; }
            public string date { get; set; }
            public string title { get; set; }
            public string image { get; set; }
            public Content content { get; set; }
        }
        public async static Task<Root> GetNews()
        {
            var http = new HttpClient();
            var url = String.Format("http://api-demo-anhth.herokuapp.com/data.json");
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Root));
            //Khoi tao Stream local de doc json
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            //Doc object da phan tich duoc tu json
            var data = (Root)serializer.ReadObject(ms);
            return data;
        }

    }
}
