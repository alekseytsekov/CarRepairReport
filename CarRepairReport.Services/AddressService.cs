namespace CarRepairReport.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.AddressModels;
    using CarRepairReport.Services.Interfaces;
    public class AddressService : Service, IAddressService
    {
        public AddressService(ICarRepairReportData context) : base(context)
        {
        }

        public Address GenerateAddressToUser(string bmCountry, string bmCity, string bmNeighborhood, string bmStreetName, string appUserId, bool isPrimary)
        {
            
            if (string.IsNullOrWhiteSpace(bmCountry) || string.IsNullOrWhiteSpace(bmCity))
            {
                return null;
            }

            var user = this.context.MyUsers.FirstOrDefault(x => x.ApplicationUserId == appUserId);

            bmCountry = bmCountry.ToLower();

            var country = this.context.Countries.FirstOrDefault(x => x.Name == bmCountry);

            if (country == null)
            {
                country = new Country() {Name = bmCountry};
                this.context.Countries.Add(country);
                //this.context.Commit();
            }

            bmCity = bmCity.ToLower();
            var city = this.context.Cities.FirstOrDefault(x => x.Name == bmCity);

            if (city == null)
            {
                city = new City()
                {
                    Name = bmCity,
                    CountryId = country.Id,
                    Country = country
                };

                country.Cities.Add(city);
                this.context.Cities.Add(city);
                //this.context.Commit();
            }

            //bmNeighborhood = bmNeighborhood.ToLower();
            //bmStreetName = bmStreetName.ToLower();

            //var address =
            //    this.context.Addresses.FirstOrDefault(x => x.User.ApplicationUserId == appUserId && x.IsPrimary);

            var address =
                this.context.Addresses.FirstOrDefault(a => a.IsPrimary && a.City.Id == city.Id);

            if (address == null)
            {
                address = new Address()
                {
                    StreetName = city.Name,
                    Neighborhood = country.Name,
                    CityId = city.Id,
                    City = city,
                    IsPrimary = isPrimary
                };
                
                city.Addresses.Add(address);
                this.context.Addresses.Add(address);
                //this.context.Commit();
            }
            //else
            //{
            //    address.City = city;
            //    address.CityId = city.Id;
            //    this.context.Commit();
            //}



            //address.UserId = user.Id;
            //address.User = user;

            address.ModifiedOn = DateTime.UtcNow;
            user.Addresses.Add(address);
            //address.Users.Add(user);

            user.ModifiedOn = DateTime.UtcNow;
            this.context.MyUsers.Update(user);
            //this.context.Addresses.Update(address);
            
            

            this.context.Commit();

            return address;
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return this.context.Addresses.GetAll();
        }

        
    }
}
