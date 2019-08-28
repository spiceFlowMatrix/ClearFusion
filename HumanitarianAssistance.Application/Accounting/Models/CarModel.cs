using System;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class CarModel
    {
        public string NameOfCar { get; set; }
        public int NumberOfDoors { get; set; }
        public DateTime FirstRegistration { get; set; }
        public double MaxSpeed { get; set; }
    }
}