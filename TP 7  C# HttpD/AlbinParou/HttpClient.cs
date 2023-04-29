using System;
using System.IO;
using System.Net.Http;

namespace HttpD
{
    public class HttpClientStream
    {
        private readonly HttpClient _httpClient;

        private HttpMethod _method;
        // TODO: getter for _httpClient named `httpClient`
        public HttpClient httpClient => _httpClient;
        public HttpMethod Method => _method;

        public HttpClientStream(HttpMethod method, string url="http://127.0.0.1:8000")
        {
            // TODO: initialisation of _httpClient
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Clear();
            _method = method;
        }

        public StreamReader SendMessage(string message="")
        {
            // TODO: send a message and return the stream associated to the response
            var request = new HttpRequestMessage(_method, httpClient.BaseAddress);
            request.Content = new StringContent(message);
            var response = httpClient.Send(request);

            return new StreamReader(response.Content.ReadAsStream());
        }

        public string ResponseFromStream(StreamReader stream)
        {
            string line;
            var res = "";
            while((line = stream.ReadLine()) != null)
                res += line;
            stream.Close();
            return res;
        }
    }
}
