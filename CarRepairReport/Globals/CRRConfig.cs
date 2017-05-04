namespace CarRepairReport.Globals
{
    public static class CRRConfig
    {
        public static string LangCookieKey = "CRRLang";
        public static string DefaultLanguageName = "English";
        public static string DefaultLanguageTwoLetterCode = "en";

        public static string GoogleDownloadLink = "https://drive.google.com/uc?export=download&id=";
        public static string CarRepaitReportJson = "~/Resources/carRepairReport.json";

        public static int NumberOfTopVehicleServiceInHomeView = 5;
        public static int NumberOfTopManufacturersInHomeView = 5;
        public static int VehicleServiceVotesOnPage = 20;
        public static int MaxImageSize = 1024*1024*1;
        public static int NumberOfLastAddedCarParts = 100;
        public static int ListOfServicedCarPartOnMainPage = 25;
        public static int NumberOfForumPostsPerPage = 10;
        

        public static string LanguageResourcesCacheCollection = "languageResourcesCrr";
        public static string CurrentForumPage = "currentForumPage";
        public static string ForumFilter = "currentForumFilter";
        public static string MaxForumPage = "maxForumPage";

        public static string DateFormat = "dd-MMM-yy HH:mm";

    }
}