namespace RentalCar.Models
{
    public class CarDivision
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Car> Cars { get; } = new List<Car>();
    }
}
