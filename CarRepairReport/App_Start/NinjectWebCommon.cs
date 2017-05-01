using CarRepairReport;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace CarRepairReport
{
    using System;
    using System.Web;
    using CarRepairReport.Areas.Forum.Managers;
    using CarRepairReport.Data;
    using CarRepairReport.Managers;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Services;
    using CarRepairReport.Services.Interfaces;
    using CloudStorageApi;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            kernel.Bind<CarRepairReportData>().ToSelf().InRequestScope();
            kernel.Bind<ICarRepairReportData>().To<CarRepairReportData>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<ILanguageService>().To<LanguageService>().InRequestScope();
            kernel.Bind<ILanguageManager>().To<LanguageManager>().InRequestScope();
            kernel.Bind<IMyUserManager>().To<MyUserManager>().InRequestScope();
            kernel.Bind<IAddressService>().To<AddressService>().InRequestScope();
            kernel.Bind<ICarManager>().To<CarManager>().InRequestScope();
            kernel.Bind<ICarService>().To<CarService>().InRequestScope();
            kernel.Bind<IVehicleServiceService>().To<VehicleServiceService>().InRequestScope();
            kernel.Bind<IVehicleServiceManager>().To<VehicleServiceManager>().InRequestScope();
            kernel.Bind<IManufacturerManager>().To<ManufacturerManager>().InRequestScope();
            kernel.Bind<IManufacturerService>().To<ManufacturerService>().InRequestScope();
            kernel.Bind<ICommonService>().To<CommonService>().InRequestScope();
            kernel.Bind<ICloudStorage>().To<GoogleDrive>().InRequestScope();
            //kernel.Bind<ICloudStorage>().To<DropboxApi>().InRequestScope();
            kernel.Bind<ICacheManager>().To<CacheManager>().InRequestScope();
            kernel.Bind<IForumManager>().To<ForumManager>().InRequestScope();
            kernel.Bind<IForumService>().To<ForumService>().InRequestScope();
        }
    }
}
