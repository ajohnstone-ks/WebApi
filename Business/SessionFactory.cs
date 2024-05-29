using System;
using Csla;

namespace Business {
	public sealed class SessionFactory : ISessionFactory
	{
		private readonly IDataPortal<Session> dataPortal;
		private readonly IContextFactory contextFactory;

		public SessionFactory(IDataPortal<Session> dataPortal, IContextFactory contextFactory)
		{
			this.dataPortal = dataPortal ?? throw new ArgumentNullException(nameof(dataPortal));
			this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
		}

		public Session Login(string key)
		{
			var session = dataPortal.Create();

			session.Context = contextFactory.LoadContext(session.Id);

			return session;
		}
	}
}
