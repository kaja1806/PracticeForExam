using Microsoft.EntityFrameworkCore;
using Shared;

namespace WebAPI.Data;

public class DataAccess : IDataAccess
{
    private readonly HouseDbContext _dbContext;

    public DataAccess(HouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<House> CreateHouse(House house)
    {
        //this is to that the id in database won't be the same as the last
         int houseId = 1;
         if (_dbContext.House.Any())
         {
             houseId = _dbContext.House.Max(h => h.HouseId);
             houseId++;
         }
        
         house.HouseId = houseId;

        // adding a new student to database
        _dbContext.House.Add(house);
        _dbContext.SaveChanges();
        return Task.FromResult(house);
    }
    
    public Task<List<House>> GetAllHouses()
    {
        // tolistasync gives you a Task list of students
        // to have just a list, use await, but then also in the function
        //the return type can't be Task<List<..>> it needs to be without Task
        var listOfHouses = _dbContext.House.ToListAsync();
        return listOfHouses;
    }
    
    
    public Task<Owner> CreateOwner(Owner owner)
    {
        //this is to that the id in database won't be the same as the last
        int ownerId = 1;
        if (_dbContext.Owner.Any())
        {
            ownerId = _dbContext.Owner.Max(o => o.OwnerId);
            ownerId++;
        }

        owner.OwnerId = ownerId;

        // adding a new student to database
        _dbContext.Owner.Add(owner);
        _dbContext.SaveChanges();
        return Task.FromResult(owner);
    }
    
    public Task<List<Owner>> GetAllOwners()
    {
        // tolistasync gives you a Task list of students
        // to have just a list, use await, but then also in the function
        //the return type can't be Task<List<..>> it needs to be without Task
        var listOfOwners = _dbContext.Owner.ToListAsync();
        return listOfOwners;
    }
    
    public async Task AddHouseToOwner(House house, int ownerId)
    {
        // checking if student exits, redundant but just in case.
        // redundant since we are loading a list in the UI that has all the users
        // so theoretically can't chose a student that does not exists in db

        //any will give you a boolean response, if that condition is true, but it does a db call to make that decision
        var checkOwner = _dbContext.Owner.Any(o => o.OwnerId == ownerId);
        if (checkOwner)
        {
            house.OwnerId = ownerId;

            _dbContext.House.Add(house);
            

            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("House not found");
        }
    }

    public async Task<List<House>> GetHousesOfOwners(int ownerId)
    {
        return await _dbContext.House.Where(t => t.OwnerId == ownerId).ToListAsync();
    }
    
    

    public async Task UpdateHouseSold(int houseId, bool UpdatedSold)
    {
        var House = _dbContext.House.FirstOrDefault(o => o.HouseId == houseId);

        if (House != null)
        {
            // Step 2: Update the boolean field
            House.Sold = UpdatedSold;

            // Step 3: Save changes to the database
            _dbContext.SaveChanges();
        }
        else
        {
            // Handle case where object with specified ID is not found
            throw new InvalidOperationException("Object not found.");
        }
    }
    public async Task<List<House>> GetHousesByCountry(string country)
    {
        return await _dbContext.House.Where(h => h.Country == country).ToListAsync();
    }
    }

    
    
