using System.Collections.Generic;
using System.Linq;
using FieldAgent.Entities;
using FieldAgent.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL
{
    
   public class AgentEFRepo : IAgentRepository
    {
        private FieldAgentsDbContext context;

        public AgentEFRepo(FieldAgentsDbContext context)
        {
            this.context = context;
        }

        public Response Delete(int agentId)
        {
            Response response = new Response();
            Agent toDelete = context.Agent.Find(agentId);
            if (toDelete == null)
            {
                response.Message += $"Agent with ID {agentId} not found";
                response.Success = false;
                return response;
            }
            context.Agent.Remove(toDelete);
            response.Success = context.SaveChanges() == 1;
            return response;
        }

        public Response<Agent> Get(int agentId)
        {
            Response<Agent> response = new Response<Agent>();
            response.Data = context.Agent.Find(agentId);
            response.Success = response.Data != null;
            if (!response.Success)
            {
                response.Message += $"Agent with ID {agentId} not found";

            }

            return response;
        }

        public Response<List<Mission>> GetMissions(int agentId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();

            response.Data = context.Agent
                .Find(agentId).Missions;

            response.Success = response.Data.Count > 0;
            if (!response.Success)
            {
                response.Message += "No Missions found";
                return response;
            }
            return response;
        }

        public Response<Agent> Insert(Agent agent)
        {
            Response<Agent> response = new Response<Agent>();
            context.Agent.Add(agent);
            response.Success = 1 == context.SaveChanges();
            if (!response.Success)
            {
                response.Message += "Could not add Agent";
                return response;
            }

            response.Data = agent;
            return response;
        }

        public Response Update(Agent agent)
        {
            Response response = new Response();

            try
            {
                context.Agent.Update(agent);
                response.Success = context.SaveChanges() == 1;

            }
            catch
            {
                response.Message += $"Agent with ID {agent.AgentId} not found";
            }
            return response;
        }
    }
}
