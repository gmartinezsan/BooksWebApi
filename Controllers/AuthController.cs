using System;
using System.Threading.Tasks;
using BooksWebApi.Data;
using BooksWebApi.Entities;
using BooksWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BooksWebApi.Controllers
{
  public class AuthController : Controller
  {
    private BooksCatalogDbContext _context;
    private SignInManager<User> _signInMgr;
    private ILogger<AuthController> _logger;

    public AuthController(BooksCatalogDbContext context, SignInManager<User> signInMgr, ILogger<AuthController> logger)
    {
	    _context = context;
	    _signInMgr = signInMgr;
	    _logger = logger;
    }

    [HttpPost("api/Auth/login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
	    try
	    {
		    var result = await _signInMgr.PasswordSignInAsync(model.UserName, model.Password, false, false);
		    if (result.Succeeded)
		    {
			    return Ok();
		    }
	   }
     catch (Exception ex)
	   {
       _logger.LogError($"an Exception is thrown while logging in {ex}");
	   }
	   return BadRequest("Failed to login");
  }
 }
}