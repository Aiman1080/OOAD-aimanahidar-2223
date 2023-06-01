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
    public class Ontlening
    {
        /*Alle property*/
        public int Id { get; set; }
        public DateTime Vanaf { get; set; }
        public DateTime Tot { get; set; }
        public string Bericht { get; set; }
        public Status Status { get; set; }
        public int Voertuig_Id { get; set; } 
        public int Aanvragen_Id { get; set; }

        /*Alle constructoren*/
        public Ontlening() { }

        public Ontlening(SqlDataReader reader)
        {
            this.Id = Convert.ToInt32(reader["id"]);
            this.Vanaf = Convert.ToDateTime(reader["vanaf"]);
            this.Tot = Convert.ToDateTime(reader["tot"]);
            this.Bericht = Convert.ToString(reader["bericht"]);
            this.Status = (Status)Convert.ToByte(reader["status"]);
            this.Voertuig_Id = Convert.ToInt32(reader["voertuig_Id"]);
            this.Aanvragen_Id = Convert.ToInt32(reader["aanvrager_id"]);
        }

        public static List<Ontlening> GetAll(int gebruikerId)
        {
            List<Ontlening> ontleningen = new List<Ontlening>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Ontlening WHERE aanvrager_id = @gebruikerId", conn);
                comm.Parameters.AddWithValue("@gebruikerId", gebruikerId);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Ontlening ontlening = new Ontlening(reader);
                    ontleningen.Add(ontlening);
                }
            }
            return ontleningen;
        }
        public static void DeleteByVoertuigId(int voertuigId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("DELETE FROM Ontlening WHERE voertuig_Id = @voertuig_Id", conn);
                comm.Parameters.AddWithValue("@voertuig_Id", voertuigId);
                comm.ExecuteNonQuery();
            }
        }

        public static void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("DELETE FROM Ontlening WHERE id=@id", conn);
                comm.Parameters.AddWithValue("@id", id);
                comm.ExecuteNonQuery();
            }
        }

        public static void Insert(Ontlening ontlening)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(
                    "INSERT INTO Ontlening (vanaf, tot, bericht, status, voertuig_id, aanvrager_id) VALUES (@vanaf, @tot, @bericht, @status, @voertuig_id, @aanvrager_id)",
                    conn);
                comm.Parameters.AddWithValue("@vanaf", ontlening.Vanaf);
                comm.Parameters.AddWithValue("@tot", ontlening.Tot);
                comm.Parameters.AddWithValue("@bericht", ontlening.Bericht);
                comm.Parameters.AddWithValue("@status", (int)ontlening.Status);
                comm.Parameters.AddWithValue("@voertuig_id", ontlening.Voertuig_Id);
                comm.Parameters.AddWithValue("@aanvrager_id", ontlening.Aanvragen_Id);
                comm.ExecuteNonQuery();
            }
        }

        public static void Update(Ontlening ontlening)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(
                    "UPDATE Ontlening SET vanaf=@vanaf, tot=@tot, bericht=@bericht, status=@status, voertuig_id=@voertuig_id, aanvrager_id=@aanvrager_id WHERE id=@id",
                    conn);
                comm.Parameters.AddWithValue("@vanaf", ontlening.Vanaf);
                comm.Parameters.AddWithValue("@tot", ontlening.Tot);
                comm.Parameters.AddWithValue("@bericht", ontlening.Bericht);
                comm.Parameters.AddWithValue("@status", (int)ontlening.Status);
                comm.Parameters.AddWithValue("@voertuig_id", ontlening.Voertuig_Id);
                comm.Parameters.AddWithValue("@aanvrager_id", ontlening.Aanvragen_Id);
                comm.Parameters.AddWithValue("@id", ontlening.Id);
                comm.ExecuteNonQuery();
            }
        }

        public static List<Ontlening> GetAllByVoertuigOwnerId(int eigenaarId)
        {
            List<Ontlening> ontleningen = new List<Ontlening>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Ontlening WHERE voertuig_id IN (SELECT Id FROM Voertuig WHERE Eigenaar_Id = @eigenaarId)", conn);
                comm.Parameters.AddWithValue("@eigenaarId", eigenaarId);
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
