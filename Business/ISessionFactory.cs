namespace Business
{
	public interface ISessionFactory
	{
		Session Login(string key);
	}
}