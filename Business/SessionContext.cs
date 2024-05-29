using System;
using Csla;

namespace Business {
	[Serializable]
	public sealed class SessionContext : ReadOnlyBase<SessionContext> {
		public static readonly PropertyInfo<long> SessionIdProperty = RegisterProperty<long>(nameof(SessionId));
		public long SessionId
		{
			get => GetProperty(SessionIdProperty);
			private set => LoadProperty(SessionIdProperty, value);
		}

		[Fetch]
		private void Fetch(long sessionId) => SessionId = sessionId;
	}
}
