namespace RentalCar.Models
{
    public class CarCategory 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CarClass> CarClasses { get; } = new List<CarClass>();
    }
}
