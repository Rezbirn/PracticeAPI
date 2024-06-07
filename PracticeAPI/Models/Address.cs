using PracticeAPI.Enums;

namespace PracticeAPI.Models
{
    public class Address : AddressCreateModel
    {
        public int Id { get; private set; }



        public Address(Country country, string city, string street, string houseNumber, int? apartmentNumber)
            : base(country, city, street, houseNumber, apartmentNumber) { }

        public Address(int id, Country country, string city, string street, string houseNumber, int? apartmentNumber) : this(country, city, street, houseNumber, apartmentNumber)
        {
            Id = id;
        }

        //for EF
        private Address() { }

    }
}
