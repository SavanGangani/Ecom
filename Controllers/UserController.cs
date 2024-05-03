using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Models;
using Ecom.repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecom.Controllers
{
    // [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(ILogger<UserController> logger,IUserRepository userRepository,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _contextAccessor= httpContextAccessor;
            _userRepository= userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(tblUser user){
           _userRepository.Register(user);
           return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public IActionResult Login(tblUser u){
            _userRepository.Login(u);
            var session=_contextAccessor.HttpContext.Session;
            var email=session.GetString("c_email");
            var role=session.GetString("c_role");

            if(role=="Admin"){
                // return RedirectToAction("Index","Trip");
                return Ok("Admin Login Successfull");
            }
            else if(role=="User"){
                // return RedirectToAction("Index","Booking");
                return Ok("User Login Successfull");
            }
            else{
                return RedirectToAction("Login");
            }

        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = _userRepository.GetAllCountry();
            return Json(countries);
        }

        [HttpGet]
        public IActionResult GetStates(int countryId)
        {
            var states = _userRepository.GetState(countryId);
            return Json(states);
        }

        [HttpGet]
        public IActionResult GetCities(int stateId)
        {
            var cities = _userRepository.GetCity(stateId);
            return Json(cities);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}