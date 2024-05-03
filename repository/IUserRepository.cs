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
      List<tblUser> GetAllCountry();
      List<tblUser> GetState(int countryid);
      List<tblUser> GetCity(int stateid);
    }
}