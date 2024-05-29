using System;

namespace Business {
	public sealed class CurrentUser : ICurrentUser
	{
		private readonly ISessionFactory sessionFactory;
		private readonly ISessionLocator sessionLocator;

		public CurrentUser(ISessionFactory sessionFactory, ISessionLocator sessionLocator)
		{
			this.sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
			this.sessionLocator = sessionLocator ?? throw new ArgumentNullException(nameof(sessionLocator));
		}

		public Session Session
		{
			get => sessionLocator.GetSession();
			private set => sessionLocator.SetSession(value);
		}

		public void Login(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException(nameof(key));
			}

			Session = sessionFactory.Login(key);
		}
	}
}
