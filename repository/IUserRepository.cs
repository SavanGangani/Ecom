using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Models;

namespace Ecom.repository
{
    public interface IUserRepository
    {
        public void Register(tblUser user);
      List<tblUser> Login(tblUser user);
      List<tblCountry> GetAllCountry();
      List<tblState> GetState(int countryid);
      List<tblCity> GetCity(int stateid);
    }
}