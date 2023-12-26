namespace BookingSystem.ApiGateways.Middleware
{
    public class JWTAuthenticationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage();

            var authorize = request.Headers.Authorization;
            if (authorize != null)
            {
                if (authorize.Scheme != "Bearer")
                {
                    responseMessage.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    return responseMessage;
                }

                if (authorize.Parameter == "")
                {
                    responseMessage.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    return responseMessage;
                }
            }
            else
            {
                responseMessage.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                return responseMessage;
            }

            //do stuff and optionally call the base handler..
            return await base.SendAsync(request, cancellationToken);
        }
    }
}