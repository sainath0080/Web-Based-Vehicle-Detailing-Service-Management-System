namespace sparkelwash.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string VehicleType { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleNumber { get; set; }
        public string Package { get; set; }
        public string BookingDate { get; set; }
        public string BookingTime { get; set; }
        public string Status { get; set; }


        public string ServiceType { get; set; }
        public string Address { get; set; }
        public int Distance { get; set; }
        public int TotalCost { get; set; }
        public string PaymentMethod { get; set; }
    }
}