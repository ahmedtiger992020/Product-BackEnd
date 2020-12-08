
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Sample.SharedKernal.Exceptions;

namespace Sample.Web
{
    public class ExceptionMiddleware
    {
        private RequestDelegate Next { get; }
        public ILogger _logger { get; set; }
        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            Next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            int.TryParse(context.User.FindFirstValue(ClaimTypes.Sid), out int _currentUserId);
            string requestBody = string.Empty;
            try
            {
                await Next(context);
            }
            catch (Exception exception)
            {
                StringBuilder logMessage = new StringBuilder($"UserId: { _currentUserId }, ErrorMessage: { exception.Message}");
                #region Set Error Codes
                if (exception is ArgumentException || exception is ArgumentNullException || exception is ArgumentOutOfRangeException)
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                else if (exception is ValidationsException)
                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity; //https://www.bennadel.com/blog/2434-http-status-codes-for-invalid-data-400-vs-422.htm https://softwareengineering.stackexchange.com/questions/329229/should-i-return-an-http-400-bad-request-status-if-a-parameter-is-syntactically
                else if (exception is BusinessRuleException)
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                else if (exception is EntityNotFoundException)
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                else if (exception is PermissionException)
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    logMessage.Append($", Stack Trace: {exception.StackTrace}");
                }
                #endregion
                _logger.Error(logMessage.ToString());
                #region Write To Response
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    statusCode = context.Response.StatusCode,
                    isSuccess = false,
                    message = exception.Message,
                }));
                #endregion
            }
        }
    }
}
