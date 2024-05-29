using System.Threading.Tasks;

namespace Business
{
	public interface IOrderFactory
	{
		Task<Order> CreateAsync(int customerId);
	}
}