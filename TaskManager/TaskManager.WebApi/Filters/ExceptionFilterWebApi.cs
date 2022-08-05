using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.DTO;
using TaskManager.Exceptions.AuthenticationExceptions;
using TaskManager.Exceptions.TodoTaskExceptions;
using TaskManager.Exceptions.UsersExceptions;

namespace TaskManager.WebApi.Filters;

public class ExceptionFilterWebApi : Attribute, IExceptionFilter
{

    public void OnException(ExceptionContext context)
    {

        int dtoCode = 0;
        int httpCode = 0;

        switch(context.Exception)
        {
            case InvalidTokenException:

                dtoCode = 8000;
                httpCode = 404;
                break;

            case PasswordMatchesUserRealPasswordException:

                dtoCode = 8001;
                httpCode = 409;
                break;

            case UserIsLoggedException:

                dtoCode = 8002;
                httpCode = 409;
                break;

            case UserIsNotLoggedException:

                dtoCode = 8003;
                httpCode = 401;
                break;

            case TodoTaskDateException:

                dtoCode = 8004;
                httpCode = 409;
                break;

            case TodoTaskIsNullException:

                dtoCode = 8005;
                httpCode = 404;
                break;

            case TodoTaskNameException:

                dtoCode = 8006;
                httpCode = 404;
                break;

            case TodoTaskIsRepeatedException:

                dtoCode = 8007;
                httpCode = 409;
                break;

            case UserIsNullException:

                dtoCode = 8008;
                httpCode = 404;
                break;

            case UserMailIsInvalidException:

                dtoCode = 8009;
                httpCode = 409;
                break;

            case UserPasswordIsInvalidException:

                dtoCode = 8010;
                httpCode = 409;
                break;

            case UserIsRepeatedException:

                dtoCode = 8011;
                httpCode = 409;
                break;

            case NullReferenceException:

                dtoCode = 8012;
                httpCode = 404;
                break;

            case ArgumentException:

                dtoCode = 8013;
                httpCode = 409;
                break;

            case InvalidCastException:

                dtoCode = 8014;
                httpCode = 409;
                break;

            default:

                dtoCode = 8015;
                httpCode = 409;
                break;
        }

        ResponseDTO response = new ResponseDTO()
        {
            Code = dtoCode,
            ErrorMessage = context.Exception.Message
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = httpCode
        };

    }

}