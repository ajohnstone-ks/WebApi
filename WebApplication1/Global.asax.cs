using System;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using Business;
using Csla;
using Csla.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace WebApplication1
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static IServiceProvider serviceProvider;

		protected void Application_Start()
		{
			var container = new UnityContainer();
			container.RegisterType<IContextFactory, ContextFactory>();
			container.RegisterType<ISessionFactory, SessionFactory>();
			container.RegisterType<ICurrentUser, CurrentUser>();
			container.RegisterType<ISessionLocator, SessionLocator>();
			container.RegisterType<IOrderChildFactory, OrderChildFactory>();
			container.RegisterType<IOrderFactory, OrderFactory>();

			var services = new ServiceCollection();
			services
				.AddCsla(x =>
					x
						.RegisterContextManager<Csla.Web.Mvc.ApplicationContextManager>()
						.Data(
							y =>
								y
									.DefaultTransactionIsolationLevel(TransactionIsolationLevel.ReadCommitted)
									.DefaultTransactionTimeoutInSeconds(1600)
									.DefaultTransactionAsyncFlowOption(System.Transactions
										.TransactionScopeAsyncFlowOption.Enabled)
						)
				);

			serviceProvider = container.BuildServiceProvider(services);

			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(x => WebApiConfig.Register(x, serviceProvider));

			GlobalConfiguration.Configuration.Services.Replace(
				typeof(IHttpControllerActivator),
				new InjectionHttpControllerActivator(serviceProvider));

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}
	}
}