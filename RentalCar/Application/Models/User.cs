using Spark.Library.Database;

namespace RentalCar.Application.Models
{
    public class User : BaseModel
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string LoginId { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
