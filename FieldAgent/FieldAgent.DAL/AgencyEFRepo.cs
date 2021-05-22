using System.Collections.Generic;
using System.Linq;
using FieldAgent.Entities;
using FieldAgent.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL
{
    public class IAgencyRepo : IAgencyRepository
    {
        private FieldAgentsDbContext context;

        public IAgencyRepo(FieldAgentsDbContext context)
        {
            this.context = context;
        }

        public Response Delete(int agencyId)
        {
            Response response = new Response();
            Agency toDelete = context.Agency.Find(agencyId);
            if(toDelete == null)
            {
                response.Message += $"Agency with ID {agencyId} not found";
                response.Success = false;
                return response;
            }
            context.Agency.Remove(toDelete);
            response.Success = context.SaveChanges() == 1;
            return response;
        }

        public Response<Agency> Get(int agencyId)
        {
            Response<Agency> response = new Response<Agency>();

            response.Data = context.Agency.Find(agencyId);
            response.Success = response.Data != null;
            if (!response.Success)
            {
                response.Message += $"Agency with ID {agencyId} not found";

            }

            return response;
        }

        public Response<List<Agency>> GetAll()
        {
            Response<List<Agency>> response = new Response<List<Agency>>();
            response.Data = context.Agency.AsNoTracking().ToList();
            response.Success = response.Data.Count > 0;
            if (!response.Success)
            {
                response.Message += "No rows found";
            }

            return response;
        }

        public Response<Agency> Insert(Agency agency)
        {
            Response<Agency> response = new Response<Agency>();
            context.Agency.Add(agency);
            response.Success = 1 == context.SaveChanges();
            if (!response.Success)
            {
                response.Message += "Could not add Agency";
                return response;
            }

            response.Data = agency;
            return response;
        }

        public Response Update(Agency agency)
        {
            Response response = new Response();

            try
            {
                context.Agency.Update(agency);
                response.Success = context.SaveChanges() == 1;

            }catch
            {
                response.Message += $"Agency with ID {agency.AgencyId} not found";
            }
            return response;


        }
    }
}
