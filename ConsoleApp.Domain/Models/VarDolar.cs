using System.Xml.Serialization;

namespace ConsoleApp.Domain.Models
{
    public class VarDolar
    {
        [XmlElement("fecha")]
        public string Fecha { get; set; }

        [XmlElement("referencia")]
        public double Referencia { get; set; }
    }
}
