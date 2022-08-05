using TaskManager.IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.DTO;

namespace TaskManager.WebApi.Filters
{
    public class AccessAuthorizationToACertainEndpointFilter : Attribute, IAuthorizationFilter
    {

        public ILoginService LoginService { get; set; }


        public AccessAuthorizationToACertainEndpointFilter()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            this.LoginService = context.HttpContext.RequestServices.GetService<ILoginService>();

            string userToken = context.HttpContext.Request.Headers["token"];

            if (userToken == null)
                this.ContextWithResponseMessage(context, 8000, 404, "Login token absent.");
            else
                if (!this.LoginService.UserIsLogged(userToken))
                    this.ContextWithResponseMessage(context, 8003, 401, "User is not logged.");
        }

        private AuthorizationFilterContext ContextWithResponseMessage(AuthorizationFilterContext context,
            int customErrorCode, int httpStatusCode, string errorMessage)
        {
            ResponseDTO response = new ResponseDTO()
            {
                Code = customErrorCode,
                ErrorMessage = errorMessage
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = httpStatusCode
            };

            return context;
        }

    }

}