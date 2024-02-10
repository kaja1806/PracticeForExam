using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;
using WebAPI.Data;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/[action]")]
public class OwnerController : ControllerBase
{
    private readonly IDataAccess _dataAccess;

    public OwnerController(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    [HttpPost]
    public async Task<ActionResult<Owner>> CreateOwner(Owner owner)
    {
        // Just a check to see if the same student does not exist
        var existing = await _dataAccess.GetAllOwners();
        if (existing.Any(o => o.OwnerId == owner.OwnerId))
        {
            return new BadRequestObjectResult("Owner already exists");
        }

        var result = await _dataAccess.CreateOwner(owner);
        return Ok(result);
    }   
    
    // 2) Add house to the owner
    [HttpPost("{ownerId}")]
    public async Task AddHouseToOwner(House house,
        [FromRoute] int ownerId)
    {
        await _dataAccess.AddHouseToOwner(house, ownerId);
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<House>>> GetHousesOfOwners(int ownerId)
    {
        var result = await _dataAccess.GetHousesOfOwners(ownerId);
        return Ok(result);
    }
}