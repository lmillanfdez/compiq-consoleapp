using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using ConsoleApp.Domain.Models;

namespace ConsoleApp.Client
{
    public static class Helpers
    {
        public static string GetXmlDocument()
        {
            return @"<?xml version='1.0' encoding='utf-8'?> 
                    <soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
                        <soap:Body>
                        <TipoCambioDia xmlns='http://www.banguat.gob.gt/variables/ws/' />
                        </soap:Body>
                    </soap:Envelope>";
        }

        public static async Task<TipoCambioDiaResponse> GetDeserializedResponse(HttpResponseMessage httpResponseMessage)
        {
            var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var streamReader = new StreamReader(stream);
            var xmlResponse = XDocument.Load(streamReader);

            var elements = from item in xmlResponse.Descendants()
                           select item;

            var responseNode = elements.FirstOrDefault(item => item.Name.LocalName == "TipoCambioDiaResponse");
            var result = Deserialize<TipoCambioDiaResponse>(responseNode.ToString());

            return result;
        }

        private static T Deserialize<T>(string xmlEncoding)
        {
            var serializer = new XmlSerializer(typeof(T));

            T result;

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlEncoding)))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
