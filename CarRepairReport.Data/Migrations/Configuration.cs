namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CarRepairReport.Models;
    using CarRepairReport.Models.Enums;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.AddressModels;
    using CarRepairReport.Models.Models.LanguageModels;

    internal sealed class Configuration : DbMigrationsConfiguration<CarRepairReport.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "CarRepairReport.Data.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Languages.Any())
            {
                var enLang = new Language()
                {
                    Name = "English",
                    TwoLetterCode = "en",
                    CreatedOn = DateTime.UtcNow,
                    IsSystemLanguage = true
                };

                var bgLang = new Language()
                {
                    Name = "Български",
                    TwoLetterCode = "бг",
                    CreatedOn = DateTime.UtcNow,
                    IsSystemLanguage = true
                };

                var ruLang = new Language()
                {
                    Name = "Русский",
                    TwoLetterCode = "ру",
                    CreatedOn = DateTime.UtcNow,
                    IsSystemLanguage = true
                };

                context.Languages.Add(enLang);
                context.Languages.Add(bgLang);
                context.Languages.Add(ruLang);

                context.SaveChanges();
            }

            if (!context.Countries.Any())
            {
                var country = new Country()
                {
                    Name = "My Country.",
                    CreatedOn = DateTime.UtcNow,
                    CountryCode = "MC"
                };

                var city = new City()
                {
                    Name = "My City",
                    Country = country,
                    CreatedOn = DateTime.UtcNow
                };

                country.Cities.Add(city);
                context.Cities.Add(city);

                var address = new Address()
                {
                    CreatedOn = DateTime.UtcNow,
                    City = city,
                    AddressType = AddressType.Home,
                    IsPrimary = true,
                    StreetName = "My Street"
                };

                city.Addresses.Add(address);
                context.Addresses.Add(address);

                context.SaveChanges();
            }

            if (context.LanguageValues.Any())
            {
                var resources = new List<string>()
                {
                    // key      // value     // code   // type   // is deleted
                    "system.common.currentlanguage;EN;en;5;0",
                    "system.common.currentlanguage;БГ;bg;5;0",
                    "system.common.currentlanguage;РУ;ru;5;0",

                    "system.forum.category.engine;engine;en;5;0",
                    "system.forum.category.engine;двигател;bg;5;0",

                    "system.common.city; city ; en ; 5; 0",
                    "system.common.city;град ; bg; 5 ; 0",
                    "system.common.city; город ; ru ; 5; 0",

                    "system.common.edit;	edit; en; 5;0",
                    "system.common.edit;  редактирай; bg ; 5 ; 0 ",

                    "system.forum.category.engine  ;  engine   ;   en; 5;0  ",
                    "system.forum.category.engine  ;  двигател   ;  bg ; 5 ; 0 ",

                    "system.forum.category.all  ;  all categories   ;   en; 5;0   ",
                    "system.forum.category.all  ;  всички категории   ; bg ; 5 ; 0 ",

                    "system.main-nav.profile  ;  profile   ;   en ; 5 ; 0 ",
                    "system.main-nav.profile  ;  профил   ;  bg ; 5 ; 0  ",

                    "system.main-nav.garage  ;  garage;   en ; 5 ; 0  ",
                    "system.main-nav.garage  ; гараж; bg ; 5 ; 0 ",

                    "system.main-nav.forum  ; forum ;    en ; 5 ; 0  ",
                    "system.main-nav.forum  ;     форум      ; bg ; 5 ; 0 ",

                    "system.main-nav.login  ;   log in    ;    en ; 5 ; 0   ",
                    "system.main-nav.login  ;   влез   ;  bg ; 5 ; 0 ",

                    "system.main-nav.logout  ;    log out    ;    en ; 5 ; 0   ",
                    "system.main-nav.logout  ;    излез   ;  bg ; 5 ; 0  ",

                    "system.left.aside.investment  ;       investment  ;    en ; 5 ; 0    ",
                    "system.left.aside.investment  ;      инвестиция   ;  bg ; 5 ; 0  ",

                    "system.left.aside.investmentname  ;   item/article  ;    en ; 5 ; 0   ",
                    "system.left.aside.investmentname  ;    име/услуга  ; bg ; 5 ; 0 ",

                    "system.common.price  ;  price     ;      en ; 5 ; 0   ",
                    "system.common.price  ;  цена     ;  bg ; 5 ; 0  ",

                    "system.common.serialnumber  ;       part number    ;    en ; 5 ; 0   ",
                    "system.common.serialnumber  ;       парт номер   ; bg ; 5 ; 0 ",

                    "system.common.partname  ; name   ;      en ; 5 ; 0   ",
                    "system.common.partname  ;  име   ;  bg ; 5 ; 0  ",

                    "system.common.quantity  ;quantity     ;      en ; 5 ; 0   ",
                    "system.common.quantity  ;количество   ; bg ; 5 ; 0  ",

                    "system.common.manufacturername  ;     manufacturer    ;     en ; 5 ; 0    ",
                    "system.common.manufacturername  ;     производител    ; bg ; 5 ; 0 ",

                    "system.car.mountedon.km  ;           mounted on (KM)    ;     en ; 5 ; 0   ",
                    "system.car.mountedon.km  ;     монтиран на (КМ)      ;  bg ; 5 ; 0 ",

                    "system.car.mountedon.mi  ;    mounted (MI)    ;      en ; 5 ; 0   ",
                    "system.car.mountedon.mi  ;   монтиран на(МИЛИ)   ;  bg ; 5 ; 0  ",

                    "system.left.aside.newpart  ;    new part    ;     en ; 5 ; 0   ",
                    "system.left.aside.newpart  ;   нова част   ;  bg ; 5 ; 0  ",

                    "system.left.aside.carrunningdistance  ;    car running distance   ;     en ; 5 ; 0  ",
                    "system.left.aside.carrunningdistance  ;   пробег    ;  bg ; 5 ; 0  ",

                    "system.common.serviceby  ;     serviced by  ;    en ; 5 ; 0  ",
                    "system.common.serviceby  ;   обслужена от    ;bg ; 5 ; 0 ",

                    "system.common.add  ;       add    ;    en ; 5 ; 0  ",
                    "system.common.add  ;  добави   ;  bg ; 5 ; 0  ",

                    "system.right.aside.carservices  ;      car services   ;     en ; 5 ; 0   ",
                    "system.right.aside.carservices  ;  сервиз/дилър    ; bg ; 5 ; 0 ",

                    "system.common.repairedparts  ;repaired parts     ;     en ; 5 ; 0   ",
                    "system.common.repairedparts  ; поправена/сменена част   ; bg ; 5 ; 0 ",

                    "system.common.rating  ;   rating   ;     en ; 5 ; 0     ",
                    "system.common.rating  ;  рейтинг     ;  bg ; 5 ; 0 ",

                    "system.common.manufacturers  ;   manufacturers   ;   en ; 5 ; 0   ",
                    "system.common.manufacturers  ;   производители   ;  bg ; 5 ; 0 ",

                    "system.common.edit  ;  edit   ;      en ; 5 ; 0   ",
                    "system.common.edit  ;     редактирай   ;  bg ; 5 ; 0  ",

                    "system.common.cars  ;   cars  ;       en ; 5 ; 0    ",
                    "system.common.cars  ;   автомобили    ;  bg ; 5 ; 0 ",

                    "system.common.remove  ;    remove  ;      en ; 5 ; 0    ",
                    "system.common.remove  ;    премахни    ;   bg ; 5 ; 0  ",

                    "system.car.model  ;  model       ;     en ; 5 ; 0  ",
                    "system.car.model  ;   модел    ; bg ; 5 ; 0  ",

                    "system.car.make  ;     make    ;      en ; 5 ; 0   ",
                    "system.car.make  ;  марка   ;  bg ; 5 ; 0  ",

                    "system.engine.fueltype  ;    fuel type   ;     en ; 5 ; 0  ",
                    "system.engine.fueltype  ;   горивна система     ;  bg ; 5 ; 0  ",

                    "system.common.numberofservices  ;    number of services   ;      en ; 5 ; 0    ",
                    "system.common.numberofservices  ;  брой обслужвания      ;  bg ; 5 ; 0  ",

                    "system.common.numberofinvestments  ;  number od investments       ;     en ; 5 ; 0    ",
                    "system.common.numberofinvestments  ;    брой инвестиции    ;  bg ; 5 ; 0 ",

                    "system.common.totalspent  ; total spent    ;     en ; 5 ; 0  ",
                    "system.common.totalspent  ; общо похарчени    ; bg ; 5 ; 0  ",

                    "system.user.firstname  ; first name    ;       en ; 5 ; 0  ",
                    "system.user.firstname  ;име   ; bg ; 5 ; 0 ",

                    "system.user.lastname  ;  last name    ;     en ; 5 ; 0  ",
                    "system.user.lastname  ; фамилия  ; bg ; 5 ; 0  ",

                    "system.common.country  ;  country   ;      en ; 5 ; 0  ",
                    "system.common.country  ; държава ; bg ; 5 ; 0  ",

                    "system.common.image  ;  image   ;    en ; 5 ; 0   ",
                    "system.common.image  ; снимка  ; bg ; 5 ; 0  ",

                    "system.common.save  ;   save  ;     en ; 5 ; 0    ",
                    "system.common.save  ;  запази ;  bg ; 5 ; 0  ",

                    "system.common.backtoprofile  ;  back to profile   ;      en ; 5 ; 0    ",
                    "system.common.backtoprofile  ; обратно в профила   ;  bg ; 5 ; 0 ",

                    "system.car.vin  ;  vin   ;     en ; 5 ; 0  ",
                    "system.car.vin  ; номер на рама   ;bg ; 5 ; 0  ",

                    "system.common.firstregistration  ; first registration    ;     en ; 5 ; 0   ",
                    "system.common.firstregistration  ; първа регистрация    ;   bg ; 5 ; 0     ",

                    "system.engine.petrol  ;   petrol  ;     en ; 5 ; 0   ",
                    "system.engine.petrol  ;   бензин  ; bg ; 5 ; 0    ",

                    "system.engine.petrollpg  ; petrol-lpg    ;   en ; 5 ; 0  ",
                    "system.engine.petrollpg  ; бензин-газ   ; bg ; 5 ; 0   ",

                    "system.engine.electric  ;  electric   ;       en ; 5 ; 0    ",
                    "system.engine.electric  ;  електрически  ; bg ; 5 ; 0  ",

                    "system.engine.other  ;  other   ;      en ; 5 ; 0   ",
                    "system.engine.other  ;  друг   ; bg ; 5 ; 0  ",

                    "system.engine.petrolgnv  ; petrol-gnv    ;      en ; 5 ; 0   ",
                    "system.engine.petrolgnv  ; метан    ; bg ; 5 ; 0  ",

                    "system.engine.diesel  ;  diesel   ;     en ; 5 ; 0  ",
                    "system.engine.diesel  ;  дизел   ;  bg ; 5 ; 0  ",

                    "system.engine.hybrid  ;  hybrid   ;    en ; 5 ; 0  ",
                    "system.engine.hybrid  ;  хибрид   ; bg ; 5 ; 0   ",

                    "system.engine.enginesize  ;  engine size   ;       en ; 5 ; 0    ",
                    "system.engine.enginesize  ;обем двигател  ; bg ; 5 ; 0   ",

                    "system.engine.power.kw  ;  KW   ;      en ; 5 ; 0   ",
                    "system.engine.power.kw  ;  KW (КВ)  ; bg ; 5 ; 0  ",

                    "system.engine.power.hp  ;  HP   ;     en ; 5 ; 0    ",
                    "system.engine.power.hp  ;  HP (К.С.)  ;   bg ; 5 ; 0    ",

                    "system.gearbox.manual  ;  manual   ;     en ; 5 ; 0   ",
                    "system.gearbox.manual  ;  ръчни   ;  bg ; 5 ; 0  ",

                    "system.gearbox.automatic  ;  automatic   ;     en ; 5 ; 0  ",
                    "system.gearbox.automatic  ;  автоматик   ; bg ; 5 ; 0  ",

                    "system.gearbox.semiautomatic  ;   semi-automatic  ;    en ; 5 ; 0   ",
                    "system.gearbox.semiautomatic  ;   полу-автоматик ;  bg ; 5 ; 0   ",

                    "system.gearbox.numberofgears  ;  number of gears   ;     en ; 5 ; 0    ",
                    "system.gearbox.numberofgears  ;  брой скорости   ;   bg ; 5 ; 0   ",

                    "system.common.create  ;   create  ;    en ; 5 ; 0   ",
                    "system.common.create  ;   създай  ;   bg ; 5 ; 0  ",

                    "system.common.selecetacar  ; select a car    ;    en ; 5 ; 0    ",
                    "system.common.selecetacar  ; избери МПС    ; bg ; 5 ; 0  ",

                    "system.common.select  ;  select   ;    en ; 5 ; 0   ",
                    "system.common.select  ;  избери   ; bg ; 5 ; 0  ",

                    "system.message.emptycar  ;   There are no parts and costs/investments on this car!  ;   en ; 5 ; 0   ",
                    "system.message.emptycar  ;   Автомобила няма сменени/обслужени части и добавени   ;  bg ; 5 ; 0 ",

                    "system.car.nickname  ;  plate/nickname   ;    en ; 5 ; 0   ",
                    "system.car.nickname  ;  рег.номер/прякор   ; bg ; 5 ; 0  ",

                    "system.car.engine  ;   engine  ;     en ; 5 ; 0   ",
                    "system.car.engine  ;   двигател  ;  bg ; 5 ; 0 ",

                    "system.car.gearbox  ;  gearbox   ;       en ; 5 ; 0    ",
                    "system.car.gearbox  ;  скоростна кутия   ; bg ; 5 ; 0  ",

                    "system.car.spendoncar  ;  spend on car   ;      en ; 5 ; 0    ",
                    "system.car.spendoncar  ;  похарчени по МПС   ;  bg ; 5 ; 0  ",

                    "system.car.spendonparts  ;  spend on parts   ;     en ; 5 ; 0   ",
                    "system.car.spendonparts  ;  похарчени по части   ; bg ; 5 ; 0  ",

                    "system.car.partsquantity  ; parts quantity    ;     en ; 5 ; 0    ",
                    "system.car.partsquantity  ; брой части    ; bg ; 5 ; 0  ",

                    "system.car.spendoninvestments  ;   spend on investments  ;     en ; 5 ; 0   ",
                    "system.car.spendoninvestments  ;   похарчени в инвестиции  ;  bg ; 5 ; 0    ",

                    "system.car.investmentsquantity  ; investments quantity    ;     en ; 5 ; 0     ",
                    "system.car.investmentsquantity  ; брой инвестиции   ;bg ; 5 ; 0  ",

                    "system.forum.title  ;  title   ;      en ; 5 ; 0     ",
                    "system.forum.title  ;  заглавие   ;  bg ; 5 ; 0    ",

                    "system.forum.content  ; content    ;       en ; 5 ; 0    ",
                    "system.forum.content  ; съдържание    ;bg ; 5 ; 0  ",

                    "system.common.category  ;  category   ;     en ; 5 ; 0    ",
                    "system.common.category  ;  категория   ;   bg ; 5 ; 0 ",

                    "system.forum.tags  ;  tags   ;     en ; 5 ; 0    ",
                    "system.forum.tags  ;  тагове   ;  bg ; 5 ; 0   ",

                    "system.common.filter  ;  filter   ;     en ; 5 ; 0     ",
                    "system.common.filter  ;  филтрирй   ; bg ; 5 ; 0   ",

                    "system.common.createdon  ;  created on   ;     en ; 5 ; 0    ",
                    "system.common.createdon  ;  създадено на   ;  bg ; 5 ; 0   ",

                    "system.common.modifiedon  ;  modified on   ;     en ; 5 ; 0   ",
                    "system.common.modifiedon  ;  редактирано на   ;   bg ; 5 ; 0  ",

                    "system.forum.read  ;  read   ;     en ; 5 ; 0     ",
                    "system.forum.read  ;  прочети   ;  bg ; 5 ; 0   ",

                    "system.forum.comment  ;  comment   ;      en ; 5 ; 0      ",
                    "system.forum.comment  ;  коментирай   ; bg ; 5 ; 0  ",

                    "system.forum.category.accessories  ;  accessories   ;   en ; 1 ; 0      ",
                    "system.forum.category.accessories  ;  аксесоари   ; bg ; 1 ; 0  ",

                    "system.forum.category.performancetunning  ; performance & tuning    ;   en ; 1 ; 0       ",
                    "system.forum.category.performancetunning  ; мощност и подобрения   ;  bg ; 1 ; 0 ",

                    "system.forum.category.cosmetics  ;  cosmetics   ;    en ; 1 ; 0      ",
                    "system.forum.category.cosmetics  ;  козметика   ; bg ; 1 ; 0 ",

                    "system.forum.category.maintenancerepairs  ;  maintenance & repairs   ;  en ; 1 ; 0       ",
                    "system.forum.category.maintenancerepairs  ;  поддръжка и ремонт  ;  bg ; 1 ; 0 ",

                    "system.forum.category.tiresrims  ; tires and rims    ;    en ; 1 ; 0       ",
                    "system.forum.category.tiresrims  ; гуми и джанти    ; bg ; 1 ; 0 ",

                    "system.forum.category.common  ; common    ;   en ; 1 ; 0     ",
                    "system.forum.category.common  ; общи    ;  bg ; 1 ; 0 ",

                    //////
                    //"system.common.createpost",
                    "system.message.noposts ; No posts ; en ; 5 ; 0",
                    "system.message.noposts ; Няма въпроси ; bg ; 5 ; 0",

                    "system.common.count ; count ; en ; 5 ; 0 ",
                    "system.common.count ; брой ; bg ; 5 ; 0 ",

                    "system.common.no ; no ; en ; 5; 0",
                    "system.common.no ; не ; bg ; 5; 0",

                    "system.common.total ; total ; en ; 5; 0",
                    "system.common.total ; общо ; bg ; 5; 0",

                    "system.common.street ; street adr ; en ; 5 ; 0",
                    "system.common.street ; улица ; bg ; 5 ; 0",

                    "system.common.description ; description ; en ; 5; 0",
                    "system.common.description ; описание ; bg ; 5; 0",

                    "system.common.workingtime ; working time ; en ;5 ; 0 ",
                    "system.common.workingtime ; работно време ; bg ;5 ; 0 ",

                    "system.vs.registercarservice ; register car service ; en ; 5 ; 0",
                    "system.vs.registercarservice ; Създай сервиз ; bg ; 5 ; 0",
                };

                // key      // value     // code   // type   // is deleted

                foreach (var resource in resources)
                {
                    var tokens = resource.Trim().Split(';').Select(s => s.Trim()).ToArray();

                    if (tokens.Length != 5)
                    {
                        continue;
                    }

                    var key = tokens[0];
                    var value = tokens[1];
                    var code = tokens[2];
                    var type = tokens[3];

                    var langValue = new LanguageValue()
                    {
                        CreatedOn = DateTime.UtcNow,
                        IsDeleted = false,
                        Key = key,
                        Value = value,
                        LangTwoLetterCode = code,
                        Type = (BelongTo) Enum.Parse(typeof(BelongTo), type)
                    };

                    context.LanguageValues.Add(langValue);
                    context.SaveChanges();
                }

            }
        }
    }
}
