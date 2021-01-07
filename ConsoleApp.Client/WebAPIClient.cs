using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Timers;

using Newtonsoft.Json;
using ConsoleApp.Domain.Models;

namespace ConsoleApp.Client
{
    public class WebAPIClient : IDisposable
    {
        private string _xmlDocument;
        private HttpClient _httpClient;
        private HttpClient _apiHttpClient;
        private HttpRequestMessage _request;
        private HttpRequestMessage _apiRequest;

        public WebAPIClient()
        {
            Initializer();
        }

        public void PerformCalls(Object source, ElapsedEventArgs e)
        {
            try
            {
                _request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("http://www.banguat.gob.gt/variables/ws/TipoCambio.asmx"),
                    Method = HttpMethod.Post
                };

                _request.Headers.Add("SOAPAction", "http://www.banguat.gob.gt/variables/ws/TipoCambioDia");
                _request.Content = new StringContent(_xmlDocument, Encoding.UTF8, "text/xml");
                _request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");

                var response = _httpClient.SendAsync(_request).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseObject = Helpers.GetDeserializedResponse(response).Result;
                    var dataSourceItem = responseObject.TipoCambioDiaResult.CambioDolar.FirstOrDefault();

                    if (dataSourceItem != null)
                    {
                        var exchangeRate = new ExchangeRate()
                        {
                            Date = DateTime.Parse(dataSourceItem.Fecha),
                            Rate = dataSourceItem.Referencia,
                            WhenObtained = DateTime.UtcNow
                        };

                        var requestPayload = JsonConvert.SerializeObject(exchangeRate);

                        _apiRequest = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(WebAPIUrl),
                            Method = HttpMethod.Post
                        };
                        _apiRequest.Content = new StringContent(requestPayload);
                        _apiRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var apiResponse = _apiHttpClient.SendAsync(_apiRequest).Result;
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void Initializer()
        {
            _xmlDocument = Helpers.GetXmlDocument();

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

            _apiHttpClient = new HttpClient();
            _apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string WebAPIUrl { get; set; }

        public void Dispose()
        {
            _httpClient.Dispose();
            _apiHttpClient.Dispose();
        }
    }
}
