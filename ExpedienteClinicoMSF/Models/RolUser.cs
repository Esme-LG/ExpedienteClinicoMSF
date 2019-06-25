using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExpedienteClinicoMSF.Models
{
    public   class RolUser
   
    {
        public static IConfiguration Configuration { get; set; }

        //To Read ConnectionString from appsettings.json file
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            //Configure connection in this place the name of appseting.json
            string connectionString = Configuration["ConnectionStrings:localDR"];

            return connectionString;

        }

          string connectionString = GetConnectionString();

        //string RoleUser = RoleUsers("fer@gmail.com", GetConnectionString());


        public  string RoleUsers(string Email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUserRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", Email);
                con.Open();
                string result = cmd.ExecuteScalar().ToString();
                con.Close();
                if (result == null || result == "")
                    result = "Vacio";
                return result;
            }
        }




    }




}

