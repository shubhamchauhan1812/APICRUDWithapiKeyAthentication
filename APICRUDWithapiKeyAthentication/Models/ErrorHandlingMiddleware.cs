using System.Net;
using System;
using Newtonsoft.Json;

namespace APICRUDWithapiKeyAthentication.Models
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;



        public ErrorHandlingMiddleware(RequestDelegate _next)

        {

            next = _next;



        }



        public async Task Invoke(HttpContext context /* other dependencies */)

        {

            try

            {

                await next(context);

            }



            catch (Exception ex)

            {

                await HandleExceptionAsync(context, ex);

            }

        }



        private async Task HandleExceptionAsync(HttpContext context, Exception ex)

        {

            var code = HttpStatusCode.InternalServerError; // 500 if unexpected



            var result = new tblErrorDTO<string>()

            {

                ErrorCode = (int)HttpStatusCode.InternalServerError,

                ErrorMessage = ex.Message,

                Succeed = false,

                CreatedDate = DateTime.UtcNow

            };

            // Resolve AppDbContext from HttpContext

            var appDbContext = context.RequestServices.GetRequiredService<ApplicationContext>();

            appDbContext.tblErrorDTO.Add(result);

            appDbContext.SaveChanges();



            var jsonResult = JsonConvert.SerializeObject(result);

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(jsonResult);

            return;

        }

    
}
}
