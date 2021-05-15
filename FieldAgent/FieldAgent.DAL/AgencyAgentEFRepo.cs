using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.Entities;
using FieldAgent.Interfaces;

namespace FieldAgent.DAL
{
    public class AgencyAgentEFRepo : IAgencyAgentRepository
    {
        private FieldAgentsDbContext context;

        public AgencyAgentEFRepo(FieldAgentsDbContext context)
        {
            this.context = context;
        }

        public Response Delete(int agencyid, int agentid)
        {
            Response response = new Response();
            AgencyAgent toDelete = context.AgencyAgent.Find(agencyid, agentid);
            if (toDelete == null)
            {
                response.Message += $"AgencyAgent with ID {agencyid}, {agentid} not found";
                response.Success = false;
                return response;
            }
            context.AgencyAgent.Remove(toDelete);
            response.Success = context.SaveChanges() == 1;
            return response;
        }

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {
            Response<AgencyAgent> response = new Response<AgencyAgent>();
            response.Data = context.AgencyAgent.Find(agencyid, agentid);
            response.Success = response.Data != null;
            if (!response.Success)
            {
                response.Message += $"Could not find AgencyAgent with IDs {agencyid}, {agentid}";
            }
            return response;
        }

        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();
            response.Data = context.AgencyAgent
                .Where(aa => aa.AgencyId == agencyId)
                .ToList();
            response.Success = response.Data.Count > 0;
            if (!response.Success)
            {
                response.Message = $"Could not find AgencyAgents with AgencyID {agencyId}";
            }
            return response;
        }

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();
            response.Data = context.AgencyAgent
                .Where(aa => aa.AgentId == agentId)
                .ToList();
            response.Success = response.Data.Count > 0;
            if (!response.Success)
            {
                response.Message = $"Could not find AgencyAgents with AgentID {agentId}";
            }
            return response;
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            Response<AgencyAgent> response = new Response<AgencyAgent>();
            response.Data = agencyAgent;
            context.AgencyAgent.Add(agencyAgent);
            response.Success = context.SaveChanges() == 1;
            if (!response.Success)
            {
                response.Message += "Could not add AgencyAgent";
            }
            return response;
        }

        public Response Update(AgencyAgent agencyAgent)
        {
            Response response = new Response();

            try
            {
                context.AgencyAgent.Update(agencyAgent);
                response.Success = context.SaveChanges() == 1;
            }
            catch
            {
                response.Message += $"AgencyAgent not found";
            }
            return response;
        }
    }
}
