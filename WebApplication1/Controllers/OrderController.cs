using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Business;

namespace WebApplication1.Controllers {
    [ApiKeyAuthentication]
	public class OrderController : ApiController
	{
		private readonly IOrderFactory orderFactory;

		public OrderController(IOrderFactory orderFactory) => this.orderFactory =
			orderFactory ?? throw new ArgumentNullException(nameof(orderFactory));

		[Route("orders")]
	    public async Task<HttpResponseMessage> Post(HttpRequestMessage message)
	    {
		    var order = await orderFactory.CreateAsync(132);

		    return Request.CreateResponse(HttpStatusCode.Accepted);
	    }
    }
}