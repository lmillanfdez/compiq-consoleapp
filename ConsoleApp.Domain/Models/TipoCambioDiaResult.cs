using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConsoleApp.Domain.Models
{
    public class TipoCambioDiaResult
    {
        [XmlArray("Variables")]
        public List<Variable> Variables { get; set; }

        [XmlArray("CambioDia")]
        public List<Var> CambioDia { get; set; }

        [XmlArray("CambioDolar")]
        public List<VarDolar> CambioDolar { get; set; }

        public int TotalItems { get; set; }
    }
}
