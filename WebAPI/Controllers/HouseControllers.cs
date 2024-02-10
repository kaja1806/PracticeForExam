using Microsoft.AspNetCore.Mvc;
using Shared;
using WebAPI.Data;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/[action]")]
public class HouseControllers :ControllerBase
{
    private readonly IDataAccess _dataAccess;

    public HouseControllers(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    [HttpPost]
    public async Task<ActionResult<House>> CreateHouse(House house)
    {
        // Just a check to see if the same student does not exist
        var existing = await _dataAccess.GetAllHouses();
        if (existing.Any(s => s.HouseId == house.HouseId))
        {
            return new BadRequestObjectResult("House already exists");
        }

        var result = await _dataAccess.CreateHouse(house);
        return Ok(result);
    }

    [HttpPatch("{houseId}")]
    public async Task<ActionResult<House>> UpdateHouseSold(int houseId, bool UpdatedSold)
    {
        try
        {
           await _dataAccess.UpdateHouseSold(houseId, UpdatedSold);
            return Ok("Boolean field updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("{country}")]
    public async Task<IActionResult> GetHouseByCountry(string country)
    {
        try
        {
            var houses = await _dataAccess.GetHousesByCountry(country);
            return Ok(houses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}