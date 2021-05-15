using System.Collections.Generic;
using System.Linq;
using FieldAgent.Entities;
using FieldAgent.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL
{
   public class MissionEFRepo : IMissionRepository
    {
        private FieldAgentsDbContext context;

        public MissionEFRepo(FieldAgentsDbContext context)
        {
            this.context = context;
        }

        public Response Delete(int missionId)
        {
            Response response = new Response();
            Mission toDelete = context.Mission.Find(missionId);
            
            if(toDelete == null)
            {
                response.Message = $"Mission with ID {missionId} not found";
                response.Success = false;
                return response;
            }
            context.Mission.Remove(toDelete);
            response.Success = context.SaveChanges() == 1;
            return response;
        }

        public Response<Mission> Get(int missionId)
        {
            Response<Mission> response = new Response<Mission>();

            response.Data = context.Mission.Find(missionId);
            response.Success = response.Data != null;
            if (!response.Success)
            {
                response.Message += $"No Mission with ID {missionId} found";
            }
            return response;
        }

        public Response<List<Mission>> GetByAgency(int agencyId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();

            response.Data = context.Mission
                .Where(m => m.AgencyId == agencyId)
                .ToList();

            response.Success = response.Data.Count > 0;
            if (!response.Success)
            {
                response.Message += $"No Missions found for agency ID {agencyId}";
            }
            return response;
        }

        public Response<List<Mission>> GetByAgent(int agentId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();
            response.Data = new List<Mission>();
            try
            {
                response.Data = context.Agent
                .Find(agentId)
                .Missions;
            }
            catch
            {
                response.Message = $"No Missions found for Agent ID {agentId}";
                response.Success = false;
                return response;
            }

            response.Success = response.Data.Count > 0;
            if (!response.Success)
            {
                response.Message = $"No Missions found for Agent ID {agentId}";
            }

            return response;
        }

        public Response<Mission> Insert(Mission mission)
        {
            Response<Mission> response = new Response<Mission>();
            context.Mission.Add(mission);
            response.Success = 1 == context.SaveChanges();
            if (!response.Success)
            {
                response.Message += "Could not add Mission";
                return response;
            }

            response.Data = mission;
            return response;
        }

        public Response Update(Mission mission)
        {
            Response response = new Response();

            try
            {
                context.Mission.Update(mission);
                response.Success = context.SaveChanges() == 1;
        }
            catch
            {
                response.Success = false;
                response.Message = $"No Mission with ID {mission.MissionId} found";
                return response;
            }
            if (!response.Success)
            {
                response.Message = $"No Mission with ID {mission.MissionId} found";
            }

            return response;
        }
    }
}
