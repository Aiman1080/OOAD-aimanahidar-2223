using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public enum Status
    {
        InAanvraag,
        Goedgekeurd,
        Verworpen
    }
    internal class Ontlening
    {
        public int Id { get; set; }
        public DateTime Vanaf { get; set; }
        public DateTime Tot { get; set; }
        public string Bericht { get; set; }
        public Status Status { get; set; }
        public int Voertuig_Id { get; set; }
        public int Aanvragen_id { get; set; }

    }
}
