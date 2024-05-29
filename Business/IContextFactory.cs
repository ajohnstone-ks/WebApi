namespace Business
{
	public interface IContextFactory
	{
		SessionContext LoadContext(long sessionId);
	}
}