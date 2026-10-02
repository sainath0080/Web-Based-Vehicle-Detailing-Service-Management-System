using Microsoft.AspNetCore.Mvc;
using sparkelwash.Models;
using System.Linq;

namespace sparkelwash.Controllers
{
    public class UserController : Controller
    {
        public IActionResult BookService() => View();

        [HttpPost]
        public IActionResult SubmitBooking(Booking newBooking)
        {
            
            bool isSlotTaken = TempDatabase.BookingList.Any(b =>
                b.BookingDate != null &&
                b.BookingTime != null &&
                b.BookingDate.Trim() == newBooking.BookingDate?.Trim() &&
                b.BookingTime.Trim() == newBooking.BookingTime?.Trim());

            if (isSlotTaken)
            {
                TempData["Error"] = "Sorry, this Date and Time slot is already booked! Please select another time.";
                return RedirectToAction("BookService");
            }


            // ४. पॅकेजनुसार बेस प्राईस ठरवणे (नवीन PPF च्या किंमतीसह)
            int baseCost = 0;
            if (newBooking.Package != null)
            {
                if (newBooking.Package.Contains("5000")) baseCost = 5000;
                else if (newBooking.Package.Contains("2500")) baseCost = 2500;
                else if (newBooking.Package.Contains("1500")) baseCost = 1500;
                else if (newBooking.Package.Contains("1200")) baseCost = 1200;
                else if (newBooking.Package.Contains("800")) baseCost = 800;
                else if (newBooking.Package.Contains("500")) baseCost = 500;
                else if (newBooking.Package.Contains("450")) baseCost = 450;
                else if (newBooking.Package.Contains("400")) baseCost = 400;
                else if (newBooking.Package.Contains("200")) baseCost = 200;
                else baseCost = 300;
            }


            if (newBooking.ServiceType == "Home Service")
            {
                int deliveryCharge = newBooking.Distance * 20;
                newBooking.TotalCost = baseCost + deliveryCharge;
            }
            else
            {
                newBooking.TotalCost = baseCost;
                newBooking.Address = "N/A";
                newBooking.Distance = 0;
            }

            
            newBooking.Id = TempDatabase.BookingList.Count + 1;
            newBooking.Status = "Pending";
            TempDatabase.BookingList.Add(newBooking);

            TempData["Message"] = "Thank you! Your slot is booked successfully.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CheckStatus() => View();

        [HttpPost]
        public IActionResult CheckStatus(string mobileNumber)
        {
            var booking = TempDatabase.BookingList.LastOrDefault(b => b.MobileNumber == mobileNumber);
            if (booking == null)
            {
                TempData["Error"] = "No booking found with this mobile number.";
                return RedirectToAction("CheckStatus");
            }
            return View(booking);
        }
    }
}