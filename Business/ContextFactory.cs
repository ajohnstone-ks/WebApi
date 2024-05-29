using System;
using Csla;

namespace Business {
	public sealed class ContextFactory : IContextFactory
	{
		private readonly IDataPortal<SessionContext> dataPortal;

		public ContextFactory(IDataPortal<SessionContext> dataPortal) =>
			this.dataPortal = dataPortal ?? throw new ArgumentNullException(nameof(dataPortal));

		public SessionContext LoadContext(long sessionId) => dataPortal.Fetch(sessionId);
	}
}
