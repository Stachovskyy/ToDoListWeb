using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoListWeb.Exceptions;

namespace ToDoListWeb.Filters
{
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute  //dziedzicze zeby onexception bylo dostepne
    {
        public override void OnException(ExceptionContext context)
        {

            if (context.Exception is NotFoundException myException)
            {
                context.Result = new ObjectResult(new { Message = myException.Message }) { StatusCode = 404 };
            }
            else
            {
                context.Result = new ObjectResult(new { Message = "Server Error" }) { StatusCode = 500 };
            }

            context.ExceptionHandled = true;
            base.OnException(context);  //dlatego tutaj zeby wykonal sie na koncu a nie przed calym kodem
        }
    }
}
