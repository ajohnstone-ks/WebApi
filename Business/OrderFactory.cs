using System;
using System.Threading.Tasks;
using Csla;

namespace Business {
	public sealed class OrderFactory : IOrderFactory
	{
		private readonly IDataPortal<Order> dataPortal;

		public OrderFactory(IDataPortal<Order> dataPortal) =>
			this.dataPortal = dataPortal ?? throw new ArgumentNullException(nameof(dataPortal));

		public async Task<Order> CreateAsync(int customerId) => await dataPortal.CreateAsync(customerId);
	}
}
