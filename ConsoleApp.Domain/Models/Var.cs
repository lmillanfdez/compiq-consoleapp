using System.Xml.Serialization;

namespace ConsoleApp.Domain.Models
{
    public class Var
    {
        [XmlElement("moneda")]
        public int Moneda { get; set; }
        [XmlElement("fecha")]
        public string Fecha { get; set; }
        [XmlElement("venta")]
        public double Venta { get; set; }
        [XmlElement("compra")]
        public double Compra { get; set; }
    }
}
