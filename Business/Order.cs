using System;
using Csla;

namespace Business {
	[Serializable]
	public sealed class Order : BusinessBase<Order>
	{
		public static readonly PropertyInfo<Lines> LinesProperty = RegisterProperty<Lines>(nameof(Lines));
		public Lines Lines
		{
			get => GetProperty(LinesProperty);
			private set => LoadProperty(LinesProperty, value);
		}

		[Create, RunLocal]
		private void Create(int customerId, [Inject] IOrderChildFactory childFactory) => Lines = childFactory.CreateLines();

	}
}
