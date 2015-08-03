[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Store.Mvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Store.Mvc.App_Start.NinjectWebCommon), "Stop")]

namespace Store.Mvc.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Store.Mvc.Models;
    using System.Web.Mvc;
    using Store.Mvc.Controllers;
    using Store.Core.CustomControllerFactories;
    using Store.Business.Repository;
    using Store.Business.Entities;
    using Store.Business.Search.LuceneSearch;
    using Store.Business.Search;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
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

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ITestSwitcher>().To<TestController>();

            kernel.Bind<ISearchService<BookItem>>().To<LuceneBookSearcher>();

            kernel.Bind<IRepository<BookItem>>().To<BookXmlRepository>();
        }        
    }
}
