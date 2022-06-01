using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.Entities.DBO
{
    public class FuelType
    {
        public int ID { get; set; }
        [Display(Name = "Yakıt Türü")]
        public string Type { get; set; }

    }
}
