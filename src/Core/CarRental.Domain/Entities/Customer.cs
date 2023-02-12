namespace CarRental.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Renting = new HashSet<Renting>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Renting> Renting { get; set; }
    }
}
