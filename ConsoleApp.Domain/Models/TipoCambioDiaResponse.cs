using System.Xml.Serialization;

namespace ConsoleApp.Domain.Models
{
    [XmlRoot(ElementName = "TipoCambioDiaResponse", Namespace = "http://www.banguat.gob.gt/variables/ws/")]
    public class TipoCambioDiaResponse
    {
        [XmlElement("TipoCambioDiaResult")]
        public TipoCambioDiaResult TipoCambioDiaResult { get; set; }
    }
}
