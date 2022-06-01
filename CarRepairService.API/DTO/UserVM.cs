namespace CarRepairService.API.DTO
{
    public class UserVM
    {
        public int ID { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Description { get; set; }
        public byte[]? passwordHash { get; set; }
        public byte[]? passwordSalt { get; set; }
        public int Role { get; set; }
        public bool isActive { get; set; }
        public int? carID { get; set; }
    }
}
