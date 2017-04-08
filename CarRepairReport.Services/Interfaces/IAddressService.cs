namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.AddressModels;

    public interface IAddressService
    {
        Address GenerateAddressToUser(string bmCountry, string bmCity, string bmNeighborhood, string bmStreetName, string appUserId, bool isPrimary);
        IEnumerable<Address> GetAllAddresses();
    }
}
