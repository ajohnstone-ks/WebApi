using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Business;

namespace WebApplication1 {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = ShouldAllowMultiple, Inherited = true)]
	public sealed class ApiKeyAuthenticationAttribute : Attribute, IAuthenticationFilter {
		private const bool ShouldAllowMultiple = false;
		private const string ApiKeyScheme = "APIKEY";

		public bool AllowMultiple => ShouldAllowMultiple;

		public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken) {
			ProcessRequest(context);
			return Task.CompletedTask;
		}

		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken) {
			context.Result = new AddChallengeOnUnauthorizedResult(new AuthenticationHeaderValue(ApiKeyScheme), context.Result);
			return Task.CompletedTask;
		}

		private static void ProcessRequest(HttpAuthenticationContext actionContext) {
			if (actionContext.Request.Headers.Authorization != null && SchemeApplies(actionContext)) {
				try {
					var apiKey = ParseKey(actionContext);

					using var scope = actionContext.ActionContext.ControllerContext.Configuration.DependencyResolver.BeginScope();

					if (!string.IsNullOrWhiteSpace(apiKey)) {
						var currentUser =
							(ICurrentUser)actionContext
								.ActionContext
								.RequestContext
								.Configuration
								.DependencyResolver
								.GetService(typeof(ICurrentUser));

						currentUser.Login(apiKey);
					}
				} catch (Exception ex) {
					actionContext.ErrorResult = new InternalServerErrorResult(actionContext.Request);
				}
			}
		}

		private static bool SchemeApplies(HttpAuthenticationContext actionContext) {
			// Don't process unless the authorization is the scheme we want
			var scheme = actionContext.Request.Headers.Authorization.Scheme;
			return string.Compare(scheme, ApiKeyScheme, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		private static string ParseKey(HttpAuthenticationContext actionContext) {
			const string ParameterKeyName = "key";

			var result = string.Empty;

			// Don't have a value, don't proceed
			var authValue = actionContext.Request.Headers.Authorization.Parameter;
			if (string.IsNullOrWhiteSpace(authValue)) {
				actionContext.ErrorResult = new AuthenticationFailureResult("Missing 'key=\"apikey\"' parameter", actionContext.Request);
			} else {
				// the Parameter should be in the format key="API-key"  Only break on the first equals character
				var parts = authValue.Split(new[] {'='}, 2, StringSplitOptions.None);

				// Ensure the first (and only )parameter is named correctly
				if (string.Compare(parts[0], ParameterKeyName, StringComparison.InvariantCultureIgnoreCase) != 0) {
					actionContext.ErrorResult = new AuthenticationFailureResult(
						$"Expected parameter name '{ParameterKeyName}' but was '{parts[0]}'", actionContext.Request);
				} else {
					// The parameter value should start and end with quotes, and must have a value contained within
					if (!parts[1].StartsWith("\"", StringComparison.InvariantCultureIgnoreCase) ||
						!parts[1].EndsWith("\"", StringComparison.InvariantCultureIgnoreCase) ||
						parts[1].Length < 3
					) {
						actionContext.ErrorResult =
							new AuthenticationFailureResult($"Invalid parameter value '{parts[1]}'", actionContext.Request);
					} else {
						result = parts[1].Substring(1, parts[1].Length - 2);
					}
				}
			}

			return result;
		}
	}
}