using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace CarRepairService.API.DTO
{
    public class AppointmentVM
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [ForeignKey("Userss")]
        public int? userID { get; set; }
        public Userss User { get; set; }
        [ForeignKey("Carss")]
        public int? carID { get; set; }
        public Carss Car { get; set; }
        public string Technician { get; set; }
        public bool isActive { get; set; }

    }
    public class Carss
    {
        public int ID { get; set; }
        public string carBrand { get; set; }
        public string modelYear { get; set; }
        public string lastMaintenanceDate { get; set; }
        public string carImage { get; set; }
        [ForeignKey("FuelTypess")]
        public int fuelTypeID { get; set; }
        public FuelTypess FuelType { get; set; }
        [ForeignKey("TransmissionTypess")]
        public int transmissionTypeID { get; set; }
        public TransmissionTypess TransmissionType { get; set; }
        [ForeignKey("Userss")]
        public int userID { get; set; }
        public Userss User { get; set; }
        public bool isDeleted { get; set; }

    }
    public class FuelTypess
    {
        public int ID { get; set; }
        public string Type { get; set; }

    }
    public class TransmissionTypess
    {
        public int ID { get; set; }
        public string Type { get; set; }
    }
    public class Userss
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public int Role { get; set; }
        public bool isActive { get; set; }
        public string FullName { get { return fullName(); } set { } }
        public string fullName()
        {
            string fullName = "";
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(firstName);
                lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lastName);
                return fullName = string.Concat(firstName + " " + lastName);
            }
            else
            {
                return fullName;
            }
        }
    }
}
