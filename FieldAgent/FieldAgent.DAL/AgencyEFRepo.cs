using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.Entities;
using FieldAgent.Interfaces;

namespace FieldAgent.DAL
{
    public class AgencyEFRepo : IAgencyRepository
    {
        private FieldAgentsDbContext context;

        public AgencyEFRepo(FieldAgentsDbContext context)
        {
            this.context = context;
        }

        public Response Delete(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Response<Agency> Get(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Response<List<Agency>> GetAll()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
