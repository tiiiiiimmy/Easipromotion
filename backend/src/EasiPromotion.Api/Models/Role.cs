namespace EasiPromotion.Api.Models;

public class Role
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();

    public static class Names
    {
        public const string NonMember = "Non-Member";
        public const string Member = "Member";
        public const string Admin = "Admin";
    }
} 