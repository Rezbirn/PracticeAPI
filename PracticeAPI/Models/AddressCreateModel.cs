using PracticeAPI.Enums;
using PracticeAPI.Interfaces;

namespace PracticeAPI.Models
{
    public class AddressCreateModel : IValid
    {
        public Country Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public int? ApartmentNumber { get; set; }

        public AddressCreateModel(Country country, string city, string street, string houseNumber, int? apartmentNumber)
        {
            Country = country;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
        }

        //for EF
        protected AddressCreateModel() { }
        
        public bool IsValid()
        {
            if (!Enum.IsDefined<Country>(Country))
                return false;

            if (string.IsNullOrEmpty(City))
                return false;

            if (string.IsNullOrEmpty(Street))
                return false;

            if (string.IsNullOrEmpty(HouseNumber))
                return false;

            if (ApartmentNumber is not null && ApartmentNumber < 0)
                return false;

            return true;
        }

        public Address ToAddress()
        {
            return new Address(Country, City, Street, HouseNumber, ApartmentNumber);
        }

    }
}
