namespace CarRepairReport
{
    using System.Reflection;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using CarRepairReport.Data;
    using CarRepairReport.Managers;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Services;
    using CarRepairReport.Services.Interfaces;

    public static class DependencyConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            //builder.RegisterType<CustomerService>().InstancePerRequest();
            builder.RegisterType<CarRepairReportData>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<Service>().InstancePerRequest();
            
            builder.RegisterType<IUserService>().As<UserService>().InstancePerLifetimeScope();
            builder.RegisterType<ILanguageService>().As<LanguageService>().InstancePerLifetimeScope();

            builder.RegisterType<ILanguageManager>().As<LanguageManager>().InstancePerLifetimeScope();
            builder.RegisterType<IMyUserManager>().As<MyUserManager>().InstancePerLifetimeScope();
            

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            //builder.Register(x => new CarRepairReportData()).As<ICarRepairReportData>().InstancePerRequest();

            //builder.Register(x => new LanguageService(new CarRepairReportData())).As<ILanguageService>().InstancePerRequest();
            //builder.Register(x => new UserService(new CarRepairReportData())).As<IUserService>().InstancePerRequest();

            //builder.Register(x => new CarDealerContext())
            //    .As<DbContext>()
            //    .InstancePerRequest();

            //builder.Register(x => new HttpCacheService())
            //    .As<ICacheService>()
            //    .InstancePerRequest();
            //builder.Register(x => new IdentifierProvider())
            //    .As<IIdentifierProvider>()
            //    .InstancePerRequest();

            //builder.RegisterGeneric(typeof(DbRepository<>))
            //    .As(typeof(IDbRepository<>))
            //    .InstancePerRequest();

            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}