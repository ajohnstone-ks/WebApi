using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1 {
	public sealed class InjectionHttpControllerActivator : IHttpControllerActivator {
		private readonly IServiceProvider provider;

		public InjectionHttpControllerActivator(IServiceProvider provider) => this.provider = provider ?? throw new ArgumentNullException(nameof(provider));

		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor d, Type controllerType) {
			var scope = provider.CreateScope();

			request.RegisterForDispose(scope); // disposes scope when request ends
			return (IHttpController)scope.ServiceProvider.GetRequiredService(controllerType);
		}
	}
}