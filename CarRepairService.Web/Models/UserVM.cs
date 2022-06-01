using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CarRepairService.Web.Models
{
    public class UserVM
    {
        public int ID { get; set; }
        [Display(Name = "Ad")]
        public string? firstName { get; set; }
        [Display(Name = "Soyad")]
        public string? lastName { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        public byte[]? passwordHash { get; set; }
        public byte[]? passwordSalt { get; set; }
        [Display(Name = "Rolü")]
        public int Role { get; set; }
        [Display(Name = "Aktif Mi?")]
        public bool isActive { get; set; }
        [Display(Name = "Arabası")]
        public int? carID { get; set; }
        [Display(Name = "Ad Soyad")]
        public string? fullName { get { return fullname(); } set { } }
        public string? fullname()
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
        [Display(Name = "Rolü")]
        public string? Rol { get { return rol(); } set { } }
        public string? rol()
        {
            string rol = Role.ToString();
            if (rol == 3.ToString())
            {
                rol = "Admin";
            }
            else if (rol == 2.ToString())
            {
                rol = "Teknisyen";
            }
            else if (rol == 1.ToString())
            {
                rol = "Müşteri";
            }
            return rol;
        }
        [Display(Name = "Aktif Mi?")]
        public string? Aktif { get { return aktif(); } set { } }
        public string? aktif()
        {
            string aktif = isActive.ToString();
            if (aktif == true.ToString())
            {
                aktif = "Aktif";
            }
            else
            {
                aktif = "Pasif";
            }
            return aktif;
        }
    }
}
