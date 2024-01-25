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
    
    [HttpPatch("buy-frog")]
    public IActionResult BuyFrog([FromBody] User user, [FromBody] FrogInfo frogInfo)
    {
        try
        {
            _coinsManager.BuyFrog(user, frogInfo);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("upgrade-frog")]
    public IActionResult UpgradeFrog([FromBody] User user, [FromBody] UserFrog frog)
    {
        try
        {
            _coinsManager.UpgradeFrog(user, frog);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
