namespace CarRepairReport.Services.Interfaces
{
    using CarRepairReport.Models.Models;

    public interface IAddressService
    {
        Address GenerateAddressToUser(string bmCountry, string bmCity, string bmNeighborhood, string bmStreetName, string appUserId, bool isPrimary);
    }
}
