namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.LanguageModels;

    public interface ILanguageService : IService
    {
        bool AddUpdateUserLanguage(string userId, string twoLetterCode);
        Language GetLanguageByTwoLetterCode(string twoLetterCode);
        string GetLanguageValueByKey(string langKey, string langCode);
        IEnumerable<LanguageValue> GetAllResources();
    }
}
