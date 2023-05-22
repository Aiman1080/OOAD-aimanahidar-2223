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
        public Gebruiker Eigenaar { get; set; }

        public Voertuig(SqlDataReader reader)
        {
            this.Id = Convert.ToInt32(reader["Id"]);
            this.Name = Convert.ToString(reader["Naam"]);
            this.Beschrijving = Convert.ToString(reader["Beschrijving"]);
            this.Bouwjaar = Convert.ToInt32(reader["Bouwjaar"]);
            this.Merk = Convert.ToString(reader["Merk"]);
            this.Model = Convert.ToString(reader["Model"]);
            this.Type = Convert.ToInt32(reader["Type"]);
            this.Eigenaar = Gebruiker.GetById(Convert.ToInt32(reader["Eigenaar_Id"]));
        }

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
                    int type = Convert.ToInt32(reader["Type"]);
                    if (type == 1)
                    {
                        voertuigen.Add(new MotorVoertuig(reader));
                    }
                    else if (type == 2)
                    {
                        voertuigen.Add(new GetrokkenVoertuig(reader));
                    }
                    else
                    {
                        voertuigen.Add(new Voertuig(reader));
                    }
                }
            }
            return voertuigen;
        }
    }
}
