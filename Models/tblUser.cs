using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Models
{
    public class tblUser
    {
        public int c_uid{ get; set; }
        public string c_username{ get; set; }
        public string c_password{ get; set; }
        public string c_email{ get; set; }
        public string c_name{ get; set; }
        public string c_gender{ get; set; }
        public string c_countryname{ get; set; }
        public string c_statename{ get; set; }
        public string c_cityname{ get; set; }
        public string c_role{ get; set; }
    }
}