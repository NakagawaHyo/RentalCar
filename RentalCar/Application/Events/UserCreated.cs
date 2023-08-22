using Coravel.Events.Interfaces;
using RentalCar.Application.Models;

namespace RentalCar.Application.Events
{
    public class UserCreated : IEvent
    {
        public User User { get; set; }

        public UserCreated(User user)
        {
            this.User = user;
        }
    }
}
