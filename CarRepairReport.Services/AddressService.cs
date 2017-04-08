namespace CarRepairReport.Services
{
    using CarRepairReport.Models.Models;
    using CarRepairReport.Services.Interfaces;
    public class AddressService : Service, IAddressService
    {
        public Address GenerateAddressToUser(string bmCountry, string bmCity, string bmNeighborhood, string bmStreetName, string appUserId, bool isPrimary)
        {
            if (string.IsNullOrWhiteSpace(bmCountry) || string.IsNullOrWhiteSpace(bmCity))
            {
                return null;
            }

            bmCountry = bmCountry.ToLower();
            var country = this.context.Countries.FirstOrDefault(x => x.Name == bmCountry);

            if (country == null)
            {
                country = new Country() {Name = bmCountry};
                this.context.Countries.Add(country);
                this.context.Commit();
            }

            bmCity = bmCity.ToLower();
            var city = this.context.Cities.FirstOrDefault(x => x.Name == bmCity);

            if (city == null)
            {
                city = new City()
                {
                    Name = bmCity,
                    CountryId = country.Id
                };

                this.context.Cities.Add(city);
                this.context.Commit();
            }

            //bmNeighborhood = bmNeighborhood.ToLower();
            //bmStreetName = bmStreetName.ToLower();

            var address =
                this.context.Addresses.FirstOrDefault(
                    x => x.Neighborhood == bmNeighborhood && x.StreetName == bmStreetName);

            if (address == null)
            {
                address = new Address()
                {
                    StreetName = bmStreetName,
                    Neighborhood = bmNeighborhood,
                    CityId = city.Id,
                    IsPrimary = isPrimary
                };
            }

            var user = this.context.MyUsers.FirstOrDefault(x => x.ApplicationUserId == appUserId);

            address.UserId = user.Id;

            //user.Addresses.Add(address);

            this.context.Addresses.Add(address);
            this.context.Commit();

            return address;
        }
    }
}
