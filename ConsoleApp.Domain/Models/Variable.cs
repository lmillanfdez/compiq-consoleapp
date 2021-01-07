using System.Xml.Serialization;

namespace ConsoleApp.Domain.Models
{
    public class Variable
    {
        [XmlElement("moneda")]
        public int Moneda { get; set; }

        [XmlElement("descripcion")]
        public string Descripcion { get; set; }
    }
}
