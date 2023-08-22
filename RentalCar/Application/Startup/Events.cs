using Coravel;
using Coravel.Events.Interfaces;
using RentalCar.Application.Events;
using RentalCar.Application.Events.Listeners;

namespace RentalCar.Application.Startup
{
    public static class Events
    {
        public static IServiceProvider RegisterEvents(this IServiceProvider services)
        {
            IEventRegistration registration = services.ConfigureEvents();

            // add events and listeners here
            registration
                .Register<UserCreated>()
                .Subscribe<EmailNewUser>();

            return services;
        }
    }
}
