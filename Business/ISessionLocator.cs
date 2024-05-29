namespace Business {
	public interface ISessionLocator {
		Session GetSession();
		void SetSession(Session session);
	}
}
