using tweeter_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using tweeter_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace tweeter_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase 
{
    private readonly ILogger<UsersController> _logger;
    private readonly IAuthService _authService;

    public UsersController(ILogger<UsersController> logger, IAuthService service)
    {
        _logger = logger;
        _authService = service;
    }

    [HttpPost]
    [Route("register")]
    public ActionResult CreateUser(User user) 
    {
        if (user == null || !ModelState.IsValid) {
            return BadRequest();
        }
        _authService.CreateUser(user);
        return NoContent();
    }

    [HttpGet]
    [Route("login")]
    public ActionResult<string> SignIn(string username, string password) 
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return BadRequest();
        }

        var token = _authService.SignIn(username, password);

        if (string.IsNullOrWhiteSpace(token)) {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpGet]
    [Route("current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> GetCurrentUser() 
    {
        if (HttpContext.User == null) {
            return Unauthorized();
        }
        
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Tweeter_UserID");
        var userId = Int32.Parse(userIdClaim.Value);

        var user = _authService.GetUserById(userId);

        if (user == null) {
            return Unauthorized();
        }

        return Ok(user);
    }

    [HttpGet]
    [Route("search/{searchText}")]
    public ActionResult<IEnumerable<User>> SearchUsers(string searchText) 
    {
        var users = _authService.SearchUsers(searchText);

        return Ok(users);
    }

    [HttpGet]
    [Route("{userId:int}")]
    public ActionResult<User> GetUserById(int userId) 
    {
        var user = _authService.GetUserById(userId);

        if (user == null) {
            return NotFound();
        }

        return Ok(user);
    }
}