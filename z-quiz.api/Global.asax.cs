using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using z_quiz.api.Models;
using z_quiz.api.Services;
using System.Web.Http.Dependencies;
using SimpleInjector.Lifestyles;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace z_quiz.api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            //container.Register<IZQuizService, ZQuizService>(Lifestyle.Scoped);
            container.Register<IZQuizService, ZQuizService>(Lifestyle.Scoped);
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);

        }

        public class SimpleInjectorDependencyResolver : System.Web.Mvc.IDependencyResolver,
            System.Web.Http.Dependencies.IDependencyResolver,
            System.Web.Http.Dependencies.IDependencyScope
        {
            public SimpleInjectorDependencyResolver(Container container)
            {
                if(container == null)
                {
                    throw new ArgumentNullException("container");
                }
                this.container = container;
            }

            public Container container { get; set; }

            public IDependencyScope BeginScope()
            {
                return this;
            }

            public object GetService(Type serviceType)
            {
                return ((IServiceProvider)this.container).GetService(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return this.GetAllInstances(serviceType);
            }
            IEnumerable<object> IDependencyScope.GetServices(Type serviceType)
            {
                IServiceProvider provider = container;
                Type collectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);
                var services = (IEnumerable<object>)provider.GetService(collectionType);
                return services ?? Enumerable.Empty<object>();
            }
            protected IEnumerable<object> GetAllInstances(Type service)
            {
                IServiceProvider provider = this.container;
                Type collectionType = typeof(IEnumerable<>).MakeGenericType(service);
                var services = (IEnumerable<object>)provider.GetService(collectionType);
                return services ?? Enumerable.Empty<object>();
            }

            #region IDisposable Support
            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects).
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                    // TODO: set large fields to null.

                    disposedValue = true;
                }
            }

            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~SimpleInjectorDependencyResolver() {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            //   Dispose(false);
            // }

            // This code added to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
                // TODO: uncomment the following line if the finalizer is overridden above.
                // GC.SuppressFinalize(this);
            }
            #endregion
        }
    }
}
