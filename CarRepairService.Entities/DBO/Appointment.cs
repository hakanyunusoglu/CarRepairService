using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.Entities.DBO
{
    public class Appointment
    {
        public int ID { get; set; }
        [Display(Name ="Açıklama")]
        public string Description { get; set; }
        [Display(Name ="Başlangıç Tarihi")]
        public DateTime? StartDate { get; set; }
        [Display(Name ="Bitiş Tarihi")]
        public DateTime? EndDate { get; set; }
        [ForeignKey("User")]
        public int? userID { get; set; }
        [Display(Name ="Araç Sahibi")]
        public User User { get; set; }
        [ForeignKey("Car")]
        public int? carID { get; set; }
        [Display(Name ="Araba")]
        public Car Car { get; set; }
        [Display(Name ="Teknisyen")]
        public string Technician { get; set; }
        [Display(Name ="Aktif")]
        public bool isActive { get; set; }
    }
}
