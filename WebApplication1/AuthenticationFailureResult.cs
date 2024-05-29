using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApplication1 {
	internal sealed class AuthenticationFailureResult : IHttpActionResult {
		public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request) {
			ReasonPhrase = reasonPhrase;
			Request = request;
		}

		public string ReasonPhrase { get; private set; }

		public HttpRequestMessage Request { get; private set; }

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken) {
			return Task.FromResult(Execute());
		}

		private HttpResponseMessage Execute() {
			return new HttpResponseMessage(HttpStatusCode.Unauthorized) {
				RequestMessage = Request, 
				ReasonPhrase = ReasonPhrase
			};
		}
	}
}