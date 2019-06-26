using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using ExpedienteClinicoMSF.Controllers;

namespace ExpedienteClinicoMSF.Models
{
    public class UserDataAccessLayer
    {
        

        public static IConfiguration Configuration { get; set; }

        //To Read ConnectionString from appsettings.json file
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            //Configure connection in this place the name of appseting.json for local BD or AWS instance
            string connectionString = Configuration["ConnectionStrings:localDR"];

            return connectionString;

        }

        string connectionString = GetConnectionString();

        //To Validate the login
        public string ValidateLogin(Usuarios user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spValidateUsersLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LoginEmail", user.Email);

                //string PassHash=HomeController.EncryptPassword(user.Pass);
                //cmd.Parameters.AddWithValue("@LoginPassword", PassHash);
                cmd.Parameters.AddWithValue("@LoginPassword", user.Pass);

                con.Open();
                string result = cmd.ExecuteScalar().ToString();
                con.Close();

                return result;
            }
        }

        //To Validate the Role

         public  string RoleUsers(string Email )
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
