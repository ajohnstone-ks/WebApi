using System;
using Csla;

namespace Business {
	[Serializable]
	public class Session : ReadOnlyBase<Session>
	{
		private static readonly Random Rnd = new Random();

		public static readonly PropertyInfo<long> IdProperty = RegisterProperty<long>(nameof(Id));
		public long Id
		{
			get => GetProperty(IdProperty);
			private set => LoadProperty(IdProperty, value);
		}

		public static readonly PropertyInfo<SessionContext> ContextProperty = RegisterProperty<SessionContext>(nameof(Context));
		public SessionContext Context
		{
			get => GetProperty(ContextProperty);
			internal set => LoadProperty(ContextProperty, value);
		}

		[Create, RunLocal]
		private void Create()
		{
			var bytes = new byte[8];
			Rnd.NextBytes(bytes);
			Id = BitConverter.ToInt64(bytes, 0);
		}
	}
}
