using System.Threading.Tasks;

namespace Business
{
	public interface IOrderChildFactory
	{
		Lines CreateLines();
		Task<Line> CreateLineAsync();
	}
}