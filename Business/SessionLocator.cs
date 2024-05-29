using System;
using Csla;

namespace Business {
	public sealed class SessionLocator : ISessionLocator {
		private const string SessionKey = "Session";

		private readonly ApplicationContext applicationContext;

		public SessionLocator(ApplicationContext applicationContext) =>
			this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

		public Session GetSession() => (Session)applicationContext.LocalContext[SessionKey];

		public void SetSession(Session session) =>
			applicationContext.LocalContext[SessionKey] = session;
	}
}
