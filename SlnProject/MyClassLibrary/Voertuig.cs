using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public class Voertuig
    {
        /*Alle property*/
        public int Id { get; set; }
        public string Name { get; set; }
        public string Beschrijving { get; set; }
        public int Bouwjaar { get; set; }
        public string Merk { get; set; }
        public string Model { get; set; }
        public int Type { get; set; }
        public Gebruiker Eigenaar { get; set; }

        /*Alle constructoren*/
        public Voertuig() 
        { 
        }
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
        public static Voertuig GetVoertuigById(int voertuigId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Voertuig WHERE Id=@id", conn);
                comm.Parameters.AddWithValue("@id", voertuigId);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    int type = Convert.ToInt32(reader["Type"]);
                    if (type == 1)
                    {
                        return new MotorVoertuig(reader);
                    }
                    else if (type == 2)
                    {
                        return new GetrokkenVoertuig(reader);
                    }
                    else
                    {
                        return new Voertuig(reader);
                    }
                }
                return null;
            }
        }
        public static List<Voertuig> GetByGebruikerId(int gebruikerId)
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("SELECT * FROM Voertuig WHERE Eigenaar_Id=@eigenaar_id", conn);
                comm.Parameters.AddWithValue("@eigenaar_id", gebruikerId);

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

        public static int Create(Voertuig voertuig)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(
                    @"INSERT INTO Voertuig (Naam, Beschrijving, Bouwjaar, Merk, Model, Type, Transmissie, Brandstof, Gewicht, Maxbelasting, Afmetingen, Geremd, Eigenaar_Id) 
            VALUES (@name, @description, @bouwjaar, @merk, @model, @type, @transmissie, @brandstof, @gewicht, @maxbelasting, @afmetingen, @geremd, @owner);
            SELECT SCOPE_IDENTITY();", conn);
                comm.Parameters.AddWithValue("@name", voertuig.Name);
                comm.Parameters.AddWithValue("@description", voertuig.Beschrijving);
                comm.Parameters.AddWithValue("@bouwjaar", voertuig.Bouwjaar);
                comm.Parameters.AddWithValue("@merk", voertuig.Merk);
                comm.Parameters.AddWithValue("@model", voertuig.Model);
                comm.Parameters.AddWithValue("@type", voertuig.Type);
                comm.Parameters.AddWithValue("@transmissie", (voertuig as MotorVoertuig)?.Transmissie ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@brandstof", (voertuig as MotorVoertuig)?.Brandstof ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@gewicht", (voertuig as GetrokkenVoertuig)?.Gewicht ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@maxbelasting", (voertuig as GetrokkenVoertuig)?.Maxbelasting ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@afmetingen", (voertuig as GetrokkenVoertuig)?.Afmeting ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@geremd", (voertuig as GetrokkenVoertuig)?.Geremd ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@owner", voertuig.Eigenaar.Id);

                int newId = Convert.ToInt32(comm.ExecuteScalar());
                return newId;
            }
        }

        public static void Update(Voertuig voertuig)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(
                    @"UPDATE Voertuig 
            SET Naam=@name, Beschrijving=@description, Bouwjaar=@bouwjaar, Merk=@merk, Model=@model, Type=@type, Transmissie=@transmissie, Brandstof=@brandstof, Gewicht=@gewicht, Maxbelasting=@maxbelasting, Afmetingen=@afmetingen, Geremd=@geremd, Eigenaar_Id=@owner 
            WHERE Id=@id", conn);
                comm.Parameters.AddWithValue("@id", voertuig.Id);
                comm.Parameters.AddWithValue("@name", voertuig.Name);
                comm.Parameters.AddWithValue("@description", voertuig.Beschrijving);
                comm.Parameters.AddWithValue("@bouwjaar", voertuig.Bouwjaar);
                comm.Parameters.AddWithValue("@merk", voertuig.Merk);
                comm.Parameters.AddWithValue("@model", voertuig.Model);
                comm.Parameters.AddWithValue("@type", voertuig.Type);
                comm.Parameters.AddWithValue("@transmissie", (voertuig as MotorVoertuig)?.Transmissie ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@brandstof", (voertuig as MotorVoertuig)?.Brandstof ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@gewicht", (voertuig as GetrokkenVoertuig)?.Gewicht ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@maxbelasting", (voertuig as GetrokkenVoertuig)?.Maxbelasting ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@afmetingen", (voertuig as GetrokkenVoertuig)?.Afmeting ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@geremd", (voertuig as GetrokkenVoertuig)?.Geremd ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@owner", voertuig.Eigenaar.Id);

                comm.ExecuteNonQuery();
            }
        }

        public static void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("DELETE FROM Voertuig WHERE Id=@id", conn);
                comm.Parameters.AddWithValue("@id", id);
                comm.ExecuteNonQuery();
            }
        }
    }
}
