using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace ApiTest
{
    class Program
    {
        static void Main()
        {
            var apiCallTask = ApiHelper.ApiCall("a8N9RYvKvHtG8rQCQlv2jCJCqNvEbptD");
            var result = apiCallTask.Result;
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
            List<Article> articleList = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["results"].ToString());

            foreach (Article article in articleList)
            {
                Console.WriteLine($"Section: {article.Section}");
                Console.WriteLine($"Title: {article.Title}");
                Console.WriteLine($"Abstract: {article.Abstract}");
                Console.WriteLine($"Url: {article.Url}");
                Console.WriteLine($"Byline: {article.Byline}");
            }
        }
    }

    class ApiHelper
    {
        public static async Task<string> ApiCall(string apiKey)
        {
            RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
            RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            return response.Content;
        }
    }

    // you could just as easily call the class below by the name "JsonResponse"
    // in practice, we will review the JSON response to build this class
    // each property should match the name and tier (one level? two?) found therein
    public class Article
    {
        public string Section { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Url { get; set; }
        public string Byline { get; set; }
    }
}
