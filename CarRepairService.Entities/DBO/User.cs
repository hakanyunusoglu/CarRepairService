using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairService.Entities.DBO
{
    public class User
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
        [NotMapped]
        [Display(Name = "Araç Sahibi")]
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
