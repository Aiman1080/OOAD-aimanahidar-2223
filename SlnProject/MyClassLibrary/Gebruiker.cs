using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyClassLibrary
{
    public enum Geslacht
    {
        Man = 0,
        Vrouw = 1,
    }
    public class Gebruiker
    {
        /*Alle property*/
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Paswoord { get; set; }
        public DateTime Aanmaakdatum { get; set; }
        public byte[] Profielfoto { get; set; }
        public Geslacht Geslacht { get; set; }

        /*Alle constructoren*/
        public Gebruiker()
        {
        }
        public Gebruiker(SqlDataReader reader)
        {
            this.Id = Convert.ToInt32(reader["id"]);
            this.Voornaam = Convert.ToString(reader["voornaam"]);
            this.Achternaam = Convert.ToString(reader["achternaam"]);
            this.Email = Convert.ToString(reader["email"]);
            this.Paswoord = Convert.ToString(reader["paswoord"]);
            this.Aanmaakdatum = Convert.ToDateTime(reader["aanmaakdatum"]);
            this.Profielfoto = reader["profielfoto"] as byte[];
            this.Geslacht = (Geslacht)Convert.ToByte(reader["geslacht"]);
        }

        public static Gebruiker GetLogin(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Gebruiker WHERE email = @email AND paswoord = @password", conn);
                comm.Parameters.AddWithValue("email", email);
                comm.Parameters.AddWithValue("password", password);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    return new Gebruiker(reader);
                }
                return null;
            }
        }
        public static Gebruiker GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Gebruiker WHERE id = @id", conn);
                comm.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    return new Gebruiker(reader);
                }
                return null;
            }
        }
    }

}
