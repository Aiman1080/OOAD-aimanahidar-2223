using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public enum Transmissie
    {
        Manueel = 1,
        Automatisch = 2,
    }

    public enum Brandstof
    {
        Benzine = 1,
        Diesel = 2,
        LPG = 3,
    }
    public class MotorVoertuig : Voertuig
    {
        /*Alle property*/
        public Transmissie? Transmissie { get; set; }
        public Brandstof? Brandstof { get; set; }

        /*Alle constructoren*/
        public MotorVoertuig()
        {
        }
        public MotorVoertuig(SqlDataReader reader) : base(reader)
        {
            if (reader["Transmissie"] != DBNull.Value)
            {
                this.Transmissie = (Transmissie)Convert.ToInt32(reader["Transmissie"]);
            }

            if (reader["Brandstof"] != DBNull.Value)
            {
                this.Brandstof = (Brandstof)Convert.ToInt32(reader["Brandstof"]);
            }
        }
    }

}
