namespace CarRepairReport.Services.Interfaces
{
    using CarRepairReport.Models.Models;

    public interface ILanguageService
    {
        bool AddUpdateUserLanguage(string userId, string twoLetterCode);
        Language GetLanguageByTwoLetterCode(string twoLetterCode);
    }
}
