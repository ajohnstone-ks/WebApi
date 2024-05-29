using System.Runtime.Serialization;

namespace WebApplication1.Controllers {
	[DataContract]
	public class OrderRequest {
		[DataMember(Name = "customerId")]
		public int CustomerId { get; set; }
	}
}