using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace MVc.repository
{
    public class CommonRepo
    {
        public readonly NpgsqlConnection conn;
         public CommonRepo()
            {
                IConfiguration myConfig = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                                        
                conn = new NpgsqlConnection(myConfig.GetConnectionString("MyConnection"));
        }
    }
}