using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DictionaryGuy_GUI
{
    public static class ApiHelper
    {
        public static async Task<string> SentenceSearch(string keyword)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://twinword-word-graph-dictionary.p.rapidapi.com/example/?entry=" + keyword),
                Headers =
                {
                    { "x-rapidapi-key", ApiSecrets.ApiKey },
                    { "x-rapidapi-host", "twinword-word-graph-dictionary.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public static async Task<string> SynonimSearch(string keyword)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://twinword-word-graph-dictionary.p.rapidapi.com/association/?entry=" + keyword),
                Headers =
                {
                    { "x-rapidapi-key", ApiSecrets.ApiKey },
                    { "x-rapidapi-host", "twinword-word-graph-dictionary.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public static async Task<string> RhymeSearch(string keyword)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.datamuse.com/words?rel_rhy=" + keyword)
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public static async Task<string> WordBeginSearch(string keyword)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.datamuse.com/words?sp=" + keyword + "*")
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public static async Task<string> WordEndSearch(string keyword)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.datamuse.com/words?sp=*" + keyword)
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public static async Task<string> DefinitionSearch(string keyword)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://twinword-word-graph-dictionary.p.rapidapi.com/definition/?entry=" + keyword),
                Headers =
                {
                    { "x-rapidapi-key", ApiSecrets.ApiKey },
                    { "x-rapidapi-host", "twinword-word-graph-dictionary.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }
    }
}
