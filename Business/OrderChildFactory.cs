using System;
using System.Threading.Tasks;
using Csla;

namespace Business {
	public sealed class OrderChildFactory : IOrderChildFactory
	{
		private readonly IChildDataPortal<Lines> linesDataPortal;
		private readonly IChildDataPortal<Line> lineDataPortal;

		public OrderChildFactory(IChildDataPortal<Lines> linesDataPortal, IChildDataPortal<Line> lineDataPortal)
		{
			this.linesDataPortal = linesDataPortal ?? throw new ArgumentNullException(nameof(linesDataPortal));
			this.lineDataPortal = lineDataPortal ?? throw new ArgumentNullException(nameof(lineDataPortal));
		}

		public Lines CreateLines() => linesDataPortal.CreateChild();

		public Task<Line> CreateLineAsync() => lineDataPortal.CreateChildAsync();
	}
}
