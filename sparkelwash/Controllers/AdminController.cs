using Microsoft.AspNetCore.Mvc;
using sparkelwash.Models;
using System.Linq;

namespace sparkelwash.Controllers
{
    public class AdminController : Controller
    {
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

      
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "Sparkel80070" && password == "Admin@1012")
            {
                
                return RedirectToAction("Dashboard");
            }

            TempData["Error"] = "Invalid Username or Password!";
            return RedirectToAction("Login");
        }

        
        [HttpGet]
        public IActionResult Dashboard()
        {
           
            return View(TempDatabase.BookingList);
        }

       
        [HttpPost]
        public IActionResult UpdateStatus(int id, string newStatus)
        {
            var booking = TempDatabase.BookingList.FirstOrDefault(b => b.Id == id);
            if (booking != null)
            {
                booking.Status = newStatus;
            }
            return RedirectToAction("Dashboard");
        }

       
        [HttpGet]
        public IActionResult Logout()
        {
           
            return RedirectToAction("Index", "Home");
        }
    }
}