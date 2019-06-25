using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;

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
            //Configure connection in this place the name of appseting.json
            string connectionString = Configuration["ConnectionStrings:localDR"];

            return connectionString;

        }

        string connectionString = GetConnectionString();

        //To Register a new user 
        public string RegisterUser(UserDetails user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRegisterUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@UserPassword", user.Password);


                con.Open();
                string result = cmd.ExecuteScalar().ToString();
                con.Close();

                return result;
            }
        }

        //To Validate the login
        public string ValidateLogin(UserDetails user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spValidateUserLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LoginID", user.UserID);
                cmd.Parameters.AddWithValue("@LoginPassword", user.Password);

                con.Open();
                string result = cmd.ExecuteScalar().ToString();
                con.Close();

                return result;
            }
        }

        //To Validate the login

        string RoleUser = RoleUsers("fer@gmail.com", GetConnectionString());


        public static string RoleUsers(string Email, string connectionString)
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
