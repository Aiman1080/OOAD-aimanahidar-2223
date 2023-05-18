using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace MyClassLibrary
{
    public enum Status
    {
        InAanvraag = 1,
        Goedgekeurd = 2,
        Verworpen = 3
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

        public Ontlening(SqlDataReader reader)
        {
            this.Id = Convert.ToInt32(reader["id"]);
            this.Vanaf = Convert.ToDateTime(reader["vanaf"]);
            this.Tot = Convert.ToDateTime(reader["tot"]);
            this.Bericht = Convert.ToString(reader["bericht"]);
            this.Status = (Status)Convert.ToByte(reader["status"]);
            this.Voertuig_Id = Convert.ToInt32(reader["voertuig_id"]);
            this.Aanvragen_id = Convert.ToInt32(reader["aanvrager_id"]);
        }

        public static List<Ontlening> GetAll()
        {
            List<Ontlening> ontleningen = new List<Ontlening>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Ontlening", conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Ontlening ontlening = new Ontlening(reader);
                    ontleningen.Add(ontlening);
                }
            }
            return ontleningen;
        }
    }
}
