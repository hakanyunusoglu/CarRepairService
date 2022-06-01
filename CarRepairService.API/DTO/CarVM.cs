using System.ComponentModel.DataAnnotations.Schema;

namespace CarRepairService.API.DTO
{
    public class CarVM
    {
        public int ID { get; set; }
        public string carBrand { get; set; }
        public string modelYear { get; set; }
        public string lastMaintenanceDate { get; set; }
        public string? carImage { get; set; }
        [ForeignKey("FuelTypes")]
        public int fuelTypeID { get; set; }
        public FuelTypes FuelType { get; set; }
        [ForeignKey("TransmissionTypes")]
        public int transmissionTypeID { get; set; }
        public TransmissionTypes TransmissionType { get; set; }
        [ForeignKey("Users")]
        public int userID { get; set; }
        public Users User { get; set; }
        public bool isDeleted { get; set; }
    }
    public class FuelTypes
    {
        public int? ID { get; set; }
        public string? Type { get; set; }
    }
    public class TransmissionTypes
    {
        public int? ID { get; set; }
        public string? Type { get; set; }
    }
    public class Users
    {
        public int? ID { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? Username { get; set; }
        public string? Description { get; set; }
        public byte[]? passwordHash { get; set; }
        public byte[]? passwordSalt { get; set; }
        public int? Role { get; set; }
        public bool? isActive { get; set; }
    }
}
