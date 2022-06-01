using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.Entities.DBO
{
    public class Car
    {
        public int ID { get; set; }
        [Display(Name ="Marka")]
        [Required]
        public string carBrand { get; set; }
        [Display(Name = "Model Yılı")]
        [Required]
        public string modelYear { get; set; }
        [Display(Name = "Son Bakım Tarihi")]
        [Required]
        public string lastMaintenanceDate { get; set; }
        [Display(Name = "Resim")]
        public string carImage { get; set; }
        [ForeignKey("FuelType")]
        public int fuelTypeID { get; set; }
        [Display(Name = "Yakıt Türü")]
        public FuelType FuelType { get; set; }
        [ForeignKey("TransmissionType")]
        public int transmissionTypeID { get; set; }
        [Display(Name = "Vites Tipi")]
        public TransmissionType TransmissionType { get; set; }
        [ForeignKey("User")]
        public int userID { get; set; }
        [Display(Name = "Sahibi")]
        public User User { get; set; }
        [Display(Name = "Silindi Mi?")]
        [Required]
        public bool isDeleted { get; set; }

    }
}
