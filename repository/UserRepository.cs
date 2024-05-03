using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Models;
using MVc.repository;
using Npgsql;

namespace Ecom.repository
{
    public class UserRepository : CommonRepo, IUserRepository
    {
        public IHttpContextAccessor _httpContextAccessor;
        public UserRepository(IHttpContextAccessor httpContextAccessory)
        {
            _httpContextAccessor = httpContextAccessory;
        }
        public void Register(tblUser user)
        {
            try
            {
                conn.Open();
                string query = "INSERT INTO public.t_user(c_username, c_email, c_password, c_name, c_gender, c_countryname, c_statename, c_cityname) VALUES (@c_username, @c_email, @c_password, @c_name, @c_gender, @c_countryname, @c_statename, @c_cityname)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@c_username", user.c_username);
                cmd.Parameters.AddWithValue("@c_email", user.c_email);
                cmd.Parameters.AddWithValue("@c_password", user.c_password);
                cmd.Parameters.AddWithValue("@c_name", user.c_name);
                cmd.Parameters.AddWithValue("@c_gender", user.c_gender);
                cmd.Parameters.AddWithValue("@c_countryname", user.c_countryname);
                cmd.Parameters.AddWithValue("@c_statename", user.c_statename);
                cmd.Parameters.AddWithValue("@c_cityname", user.c_cityname);
                cmd.Parameters.AddWithValue("@c_role", user.c_role);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }




        public List<tblUser> Login(tblUser user)
        {
            List<tblUser> result = new List<tblUser>();
            try
            {
                conn.Open();
                string query = "SELECT c_username, c_email, c_password, c_name, c_gender, c_countryname, c_statename, c_cityname,c_role FROM public.t_user WHERE c_email = @c_email AND c_password = @c_password";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@c_email", user.c_email);
                cmd.Parameters.AddWithValue("@c_password", user.c_password);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    tblUser loggedInUser = new tblUser();
                    loggedInUser.c_username = dr["c_username"].ToString();
                    loggedInUser.c_email = dr["c_email"].ToString();
                    loggedInUser.c_password = dr["c_password"].ToString();
                    loggedInUser.c_name = dr["c_name"].ToString();
                    loggedInUser.c_gender = dr["c_gender"].ToString();
                    loggedInUser.c_countryname = dr["c_countryname"].ToString();
                    // Assuming you want to retrieve all the user data, you can populate other properties here
                    result.Add(loggedInUser);

                    _httpContextAccessor.HttpContext.Session.SetString("c_email", loggedInUser.c_email);
                    _httpContextAccessor.HttpContext.Session.SetString("c_role", loggedInUser.c_role);

                }
                else
                {
                    // User not found, you might want to handle this case
                    Console.WriteLine("User not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }



    }
}