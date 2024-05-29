using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1 {
	public sealed class DependencyResolver : IDependencyResolver {
		private readonly IServiceProvider provider;
		private readonly IServiceScope scope;

		public DependencyResolver(IServiceProvider provider) => this.provider = provider;

		private DependencyResolver(IServiceScope scope) {
			provider = scope.ServiceProvider;
			this.scope = scope;
		}

		public IDependencyScope BeginScope() => new DependencyResolver(provider.CreateScope());

		public object GetService(Type serviceType) => provider.GetService(serviceType);

		public IEnumerable<object> GetServices(Type type) => provider.GetServices(type);

		public void Dispose() => scope?.Dispose();
	}
}