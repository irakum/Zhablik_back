using Microsoft.AspNetCore.Mvc;
using System;
using Zhablik.Managers;
using Zhablik.Models;

namespace Zhablik.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoinsController : ControllerBase
{
    private readonly CoinsManager _coinsManager;

    public CoinsController(CoinsManager coinsManager)
    {
        _coinsManager = coinsManager;
    }
    
    [HttpPatch("{userId}/buy")]
    public IActionResult BuyFrog([FromRoute] string userId, [FromBody] FrogInfo frogInfo)
    {
        try
        {
            _coinsManager.BuyFrog(userId, frogInfo);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{userId}/upgrade")]
    public IActionResult UpgradeFrog([FromRoute] string userId, [FromBody] UserFrog frog)
    {
        try
        {
            _coinsManager.UpgradeFrog(userId, frog);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
