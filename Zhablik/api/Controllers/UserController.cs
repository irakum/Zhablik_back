using Microsoft.AspNetCore.Mvc;
using Zhablik.Models;
using System;
using System.Collections.Generic;
using Zhablik.Managers;

namespace Zhablik.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager _userManager;
    private readonly AuthenticationManager _authenticationManager;

    public UserController(UserManager userManager, AuthenticationManager authenticationManager)
    {
        _userManager = userManager;
        _authenticationManager = authenticationManager;
    }
    
    [HttpPost("create")]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            var user = _userManager.CreateUser(request.Username, request.Email, request.Password);
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
    [HttpPost("login")]
    public IActionResult Login(string username, string password)
    {
        try
        {
            var user = _authenticationManager.Login(username, password);

            if (user is null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(new { Message = "Login successful" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("{username}")]
    public IActionResult GetUser(string username)
    {
        try
        {
            var user = _userManager.GetUser(username);
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpPut("{username}")]
    public IActionResult UpdateUser(string username, [FromBody] User updatedUser)
    {
        try
        {
            _userManager.UpdateUser(username, updatedUser);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpDelete("{username}")]
    public IActionResult DeleteUser(string username)
    {
        try
        {
            _userManager.DeleteUser(username);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
    
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}