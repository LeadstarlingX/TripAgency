using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;

namespace Application.DTOs.Actions
{
    public class ErrorActionResult(string jsonString, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : ActionResult
    {
        private readonly string _jsonString = jsonString;
        private readonly HttpStatusCode _statusCode = statusCode;

        public override void ExecuteResult(ActionContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var response = context.HttpContext.Response;
            if (!context.HttpContext.Response.HasStarted)
            {
                response.StatusCode = (int)_statusCode;
                response.ContentType = "application/json";
                using (TextWriter writer = new HttpResponseStreamWriter(response.Body, Encoding.UTF8))
                {
                    writer.Write(_jsonString);
                }
            }
        }
    }
}