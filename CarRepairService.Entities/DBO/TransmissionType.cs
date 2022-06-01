using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.Entities.DBO
{
    public class TransmissionType
    {
        public int ID { get; set; }
        [Display(Name = "Vites Tipi")]
        public string Type { get; set; }
    }
}
