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

        public List<tblCountry> GetAllCountry()
        {
            List<tblCountry> countryList = new List<tblCountry>();

            try
            {
                conn.Open();
                string query = "SELECT c_countryid,c_countryname FROM t_country";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new tblCountry
                        {
                            c_countryid = reader.GetInt32(0),
                            c_countryname = reader.GetString(1)
                        };
                        countryList.Add(country);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return countryList;
        }



        public List<tblState> GetState(string countryName)
        {
            List<tblState> stateList = new List<tblState>();

            try
            {
                conn.Open();
                string query = "SELECT c_stateid,c_statename,c_countryname FROM t_state WHERE c_countryname=@c_countryname";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@c_countryname", countryName);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var state = new tblState
                        {
                            c_stateid = reader.GetInt32(0),
                            c_statename = reader.GetString(1),
                            c_countryname = reader.GetString(2)
                        };
                        stateList.Add(state);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return stateList;
        }

        public List<tblCity> GetCity(string stateName)
        {
            List<tblCity> cityList = new List<tblCity>();

            try
            {
                conn.Open();
                string query = "SELECT c_cityid,c_cityname,c_statename FROM t_city WHERE c_statename=@c_statename";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@c_statename", stateName);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var city = new tblCity
                        {
                            c_cityid = reader.GetInt32(0),
                            c_cityname = reader.GetString(1),
                            c_statename = reader.GetString(2)
                        };
                        cityList.Add(city);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return cityList;
        }

    }
}