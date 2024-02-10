using Shared;

namespace WebAPI.Data;

public interface IDataAccess
{
     Task<List<House>> GetAllHouses();
     Task<House> CreateHouse(House house);
     Task<Owner> CreateOwner(Owner owner);
     Task<List<Owner>> GetAllOwners();
     Task AddHouseToOwner(House house, int ownerId);
     Task<List<House>> GetHousesOfOwners(int ownerId);

     Task UpdateHouseSold(int houseId, bool UpdatedSold);

     Task<List<House>> GetHousesByCountry(string country);


}