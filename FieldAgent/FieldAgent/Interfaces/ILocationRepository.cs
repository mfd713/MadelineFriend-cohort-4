using System.Collections.Generic;
using FieldAgent.Entities;

namespace FieldAgent.Interfaces
{
    public interface ILocationRepository
    {
        Response<Location> Insert(Location location);
        Response Update(Location location);
        Response Delete(int locationId);
        Response<Location> Get(int locationId);
        Response<List<Location>> GetByAgency(int agencyId);
    }
}
