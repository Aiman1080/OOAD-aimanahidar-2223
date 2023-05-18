using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public class Voertuig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Beschrijving { get; set; }
        public int Bouwjaar { get; set; }
        public string Merk { get; set; }
        public string Model { get; set; }
        public int Type { get; set; }
        public int Eigenaar_Id { get; set; }

        public static List<Voertuig> GetAll()
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("SELECT * FROM Voertuig", conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Voertuig voertuig = ReadVoertuig(reader);
                    voertuigen.Add(voertuig);
                }
            }
            return voertuigen;
        }

        private static Voertuig ReadVoertuig(SqlDataReader reader)
        {
            int type = Convert.ToInt32(reader["Type"]);
            if (type == 1)
            {
                MotorVoertuig motorVoertuig = new MotorVoertuig();
                motorVoertuig.Id = Convert.ToInt32(reader["Id"]);
                motorVoertuig.Name = Convert.ToString(reader["Naam"]);
                motorVoertuig.Beschrijving = Convert.ToString(reader["Beschrijving"]);
                motorVoertuig.Bouwjaar = Convert.ToInt32(reader["Bouwjaar"]);
                motorVoertuig.Merk = Convert.ToString(reader["Merk"]);
                motorVoertuig.Model = Convert.ToString(reader["Model"]);
                motorVoertuig.Type = Convert.ToInt32(reader["Type"]);
                motorVoertuig.Eigenaar_Id = Convert.ToInt32(reader["Eigenaar_Id"]);
                motorVoertuig.Transmissie = (Transmissie?)Convert.ToInt32(reader["Transmissie"]);
                motorVoertuig.Brandstof = (Brandstof?)Convert.ToInt32(reader["Brandstof"]);
                return motorVoertuig;
            }
            else if (type == 2)
            {
                GetrokkenVoertuig getrokkenVoertuig = new GetrokkenVoertuig();
                getrokkenVoertuig.Id = Convert.ToInt32(reader["Id"]);
                getrokkenVoertuig.Name = Convert.ToString(reader["Naam"]);
                getrokkenVoertuig.Beschrijving = Convert.ToString(reader["Beschrijving"]);
                getrokkenVoertuig.Bouwjaar = Convert.ToInt32(reader["Bouwjaar"]);
                getrokkenVoertuig.Merk = Convert.ToString(reader["Merk"]);
                getrokkenVoertuig.Model = Convert.ToString(reader["Model"]);
                getrokkenVoertuig.Type = Convert.ToInt32(reader["Type"]);
                getrokkenVoertuig.Eigenaar_Id = Convert.ToInt32(reader["Eigenaar_Id"]);
                getrokkenVoertuig.Gewicht = reader["Gewicht"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["Gewicht"]);
                getrokkenVoertuig.Maxbelasting = reader["Maxbelasting"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["Maxbelasting"]);
                getrokkenVoertuig.Afmeting = Convert.ToString(reader["Afmetingen"]);
                getrokkenVoertuig.Geremd = reader["Geremd"] == DBNull.Value ? null : (bool?)Convert.ToBoolean(reader["Geremd"]);
                return getrokkenVoertuig;
            }
            else
            {
                Voertuig voertuig = new Voertuig();
                voertuig.Id = Convert.ToInt32(reader["Id"]);
                voertuig.Name = Convert.ToString(reader["Name"]);
                voertuig.Beschrijving = Convert.ToString(reader["Beschrijving"]);
                voertuig.Bouwjaar = Convert.ToInt32(reader["Bouwjaar"]);
                voertuig.Merk = Convert.ToString(reader["Merk"]);
                voertuig.Model = Convert.ToString(reader["Model"]);
                voertuig.Type = Convert.ToInt32(reader["Type"]);
                voertuig.Eigenaar_Id = Convert.ToInt32(reader["Eigenaar_Id"]);
                return voertuig;
            }
        }
    }
}
