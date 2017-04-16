namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using CarRepairReport.Models;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.AddressModels;

    public interface IAddressService
    {
        Address GenerateAddress(string bmCountry, string bmCity, string bmNeighborhood, string bmStreetName,
            string appUserId, bool isPrimary, AddressType addressType);
        IQueryable<Address> GetAllAddresses();
    }
}
