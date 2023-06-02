using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public class Foto
    {
        /*Alle proprieties*/
        public int Id { get; set; } 
        public byte[] Data { get; set; }
        public int Voertuig_id { get; set; }

        /*Alle constructoren*/
        private static Foto ReadFoto(SqlDataReader reader)
        {
            Foto foto = new Foto();
            foto.Id = Convert.ToInt32(reader["Id"]);
            foto.Data = (byte[])reader["Data"];
            foto.Voertuig_id = Convert.ToInt32(reader["Voertuig_id"]);
            return foto;
        }

        public static List<Foto> GetAllFotoByVoertuigId(int voertuigId)
        {
            List<Foto> fotos = new List<Foto>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("SELECT * FROM Foto WHERE Voertuig_id=@voertuigId", conn);
                comm.Parameters.AddWithValue("@voertuigId", voertuigId);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Foto foto = ReadFoto(reader);
                    fotos.Add(foto);
                }
            }
            return fotos;
        }
        public static void DeleteByVoertuigId(int voertuigId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("DELETE FROM Foto WHERE Voertuig_id=@voertuigId", conn);
                comm.Parameters.AddWithValue("@voertuigId", voertuigId);
                comm.ExecuteNonQuery();
            }
        }

        public void UploadImage()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("INSERT INTO Foto (Data, Voertuig_id) VALUES (@Data, @voertuigId)", conn);
                comm.Parameters.AddWithValue("@Data", this.Data);
                comm.Parameters.AddWithValue("@voertuigId", this.Voertuig_id);
                comm.ExecuteNonQuery();
            }
        }
    }
}
