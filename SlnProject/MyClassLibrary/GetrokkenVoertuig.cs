using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public class GetrokkenVoertuig : Voertuig
    {
        /*Alle property*/
        public int? Gewicht { get; set; }
        public int? Maxbelasting { get; set; }
        public string Afmeting { get; set; }
        public bool? Geremd { get; set; }

        /*Alle constructoren*/
        public GetrokkenVoertuig()
        {
        }
        public GetrokkenVoertuig(SqlDataReader reader) : base(reader)
        {
            this.Gewicht = reader["Gewicht"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["Gewicht"]);
            this.Maxbelasting = reader["Maxbelasting"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["Maxbelasting"]);
            this.Afmeting = Convert.ToString(reader["Afmetingen"]);
            this.Geremd = reader["Geremd"] == DBNull.Value ? null : (bool?)Convert.ToBoolean(reader["Geremd"]);
        }
    }
}
