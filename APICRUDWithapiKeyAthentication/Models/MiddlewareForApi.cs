namespace APICRUDWithapiKeyAthentication.Models
{
    public class MiddlewareForApi
    {
        private const string ApiKeyName = "MyKey";

        private readonly RequestDelegate _next;

        public MiddlewareForApi(RequestDelegate next)

        {
            _next = next;

        }



        public async Task InvokeAsync(HttpContext context)

        {

            if (!context.Request.Headers.TryGetValue(ApiKeyName, out var extractedApiKey))

            {

                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Api key was not provided");

                return;

            }

           
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = appSettings.GetValue<string>(ApiKeyName);
            //var apiKeyService = context.RequestServices.GetRequiredService<ApiKeyService>();

            if (extractedApiKey != appSettings["MyKey"])

            {

                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Unauthorized Client");

                return;

            }

            await _next(context);

            //if (appSettings["MyKey"] != null && extractedApiKey == appSettings["MyKey"])
            //{
            //    // Static API key is valid
            //    await _next(context);
            //    return;
            //}
                
            //if (!apiKeyService.ValidateApiKey(extractedApiKey))
            //{
            //    context.Response.StatusCode = 401;
            //    await context.Response.WriteAsync("Unauthorized client");
            //    return;
            //}

            //await _next(context);

        }
    }
}
