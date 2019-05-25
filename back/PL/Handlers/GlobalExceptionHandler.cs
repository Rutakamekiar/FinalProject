using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

public class GlobalExceptionHandler : ExceptionHandler
{
    public override void Handle(ExceptionHandlerContext context)
    {
        context.Result = new ResponseMessageResult(
            context.Request.CreateResponse(
                HttpStatusCode.InternalServerError, 
                context.Exception.Message));
    }
}