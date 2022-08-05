using Microsoft.AspNetCore.Mvc;
using TaskManager.IBusinessLogic;
using TaskManager.WebApi.Filters;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[ExceptionFilterWebApi]
[Route("api/[controller]")]
public class LogoutController : Controller
{

    public readonly ILogoutService LogoutService;

    public LogoutController(ILogoutService logoutService)
    {
        this.LogoutService = logoutService;
    }

    [HttpDelete]
    public async Task<IActionResult> LogoutAsync([FromHeader(Name = "token")] string userToken)
    {

        await Task.Run(() =>
        {
            this.LogoutService.Logout(userToken);
        });

        return Ok();
    }

}

