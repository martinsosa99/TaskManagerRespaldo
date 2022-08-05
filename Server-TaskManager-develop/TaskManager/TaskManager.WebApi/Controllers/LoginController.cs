using TaskManager.IBusinessLogic;
using TaskManager.Domain;
using TaskManager.DTO;
using Microsoft.AspNetCore.Mvc;
using TaskManager.WebApi.Filters;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[ExceptionFilterWebApi]
[Route("api/[controller]")]
public class LoginController : Controller
{

    public readonly ILoginService LoginService;

    public LoginController(ILoginService loginService)
    {
        this.LoginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(string email, string password)
    {
        SessionDTO userSessionDTO = null;

        await Task.Run(() =>
        {
            Session userSession = this.LoginService.Login(email, password);

            userSessionDTO = SessionDTO.SessionToSessionDTO(userSession);    
        });

        return Ok(userSessionDTO);
    }

}


