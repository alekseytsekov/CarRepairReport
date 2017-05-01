namespace CarRepairReport.Managers.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.BindingModels.ForumBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.Models.LanguageModels;
    using CarRepairReport.Models.ViewModels.ForumVm;

    public interface ICacheManager
    {
        void LoadLanguageResources();
        IEnumerable<LanguageDto> GetLanguageResources(string languageCode);
        IEnumerable<LanguageDto> GetLanguageResources();
        string GetCssColor();
    }
}