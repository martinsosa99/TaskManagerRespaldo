using TaskManager.IBusinessLogic;
using TaskManager.Domain;
using TaskManager.DTO;
using Microsoft.AspNetCore.Mvc;
using TaskManager.WebApi.Filters;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[ExceptionFilterWebApi]
[Route("api/[controller]")]
public class UsersController : Controller
{

    public readonly IUserService UserService;

    public UsersController(IUserService userService)
    {
        this.UserService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync(UserDTO userDTO)
    {
        await Task.Run(() =>
        {
            User user = UserDTO.UserDTOToUser(userDTO);
            this.UserService.AddUser(user);
        });

        return Ok();
    }

}