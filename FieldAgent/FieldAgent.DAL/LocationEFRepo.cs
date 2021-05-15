using System.Collections.Generic;
using System.Linq;
using FieldAgent.Entities;
using FieldAgent.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL
{
    public class LocationEFRepo : ILocationRepository
    {
        private FieldAgentsDbContext context;

        public LocationEFRepo(FieldAgentsDbContext context)
        {
            this.context = context;
        }

        public Response Delete(int locationId)
        {
            Response response = new Response();
            Location toDelete = context.Location.Find(locationId);
            try
            {
                context.Location.Remove(toDelete);
                response.Success = context.SaveChanges() == 1;
            }
            catch
            {
                response.Success = false;
                response.Message = $"Location with ID {locationId} not found";
                return response;
            }
            if (!response.Success)
            {
                response.Message = $"Location with ID {locationId} not found";
            }

            return response;
        }

        public Response<Location> Get(int locationId)
        {
            Response<Location> response = new Response<Location>();

            response.Data = context.Location.Find(locationId);

            response.Success = response.Data != null;
            if (!response.Success)
            {
                response.Message = $"Location with ID {locationId} not found";
            }
            return response;
        }

        public Response<List<Location>> GetByAgency(int agencyId)
        {
            Response<List<Location>> response = new Response<List<Location>>();

            response.Data = context.Location
                .Where(l => l.AgencyId == agencyId)
                .ToList();
            response.Success = response.Data.Count > 0;
            if (!response.Success)
            {
                response.Message = $"No Locations with Agency ID {agencyId} found";
            }
            return response;
        }

        public Response<Location> Insert(Location location)
        {
            Response<Location> response = new Response<Location>();

            try
            {
                context.Location.Add(location);
                response.Success = context.SaveChanges() == 1;
            }
            
            catch
            {
                response.Message = "Could not add Location";
                response.Success = false;
                return response;
            }
            response.Data = location;
            return response;
        }

        public Response Update(Location location)
        {
            Response response = new Response();
            try
            {
                context.Location.Update(location);
                response.Success = context.SaveChanges() == 1;
            }
            catch
            {
                response.Success = false;
                response.Message = $"Location with ID {location.LocationId} not found";
                return response;
            }
            if (!response.Success)
            {
                response.Message = $"Location with ID {location.LocationId} not found";
            }

            return response;
        }
    }
}
