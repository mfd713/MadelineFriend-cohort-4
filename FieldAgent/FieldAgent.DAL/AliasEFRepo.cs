using System;
using System.Collections.Generic;
using System.Linq;
using FieldAgent.Entities;
using FieldAgent.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL
{
   public class AliasEFRepo : IAliasRepository
    {
        private FieldAgentsDbContext context;

        public AliasEFRepo(FieldAgentsDbContext context)
        {
            this.context = context;
        }

        public Response Delete(int aliasId)
        {
            Response response = new Response();
            try
            {
                Alias alias = context.Alias.Find(aliasId);
                context.Alias.Remove(alias);
                response.Success = context.SaveChanges() == 1;
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = $"Could not delete Alias with ID {aliasId}";
                return response;
            }

        }

        public Response<Alias> Get(int aliasId)
        {
            Response<Alias> response = new Response<Alias>();
            try
            {
                response.Data = context.Alias.Find(aliasId);
                response.Success = response.Data != null;
                response.Message = response.Success ? "" : $"Alias with ID {aliasId} not found";
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = $"Alias with ID {aliasId} not found";
                return response;
            }
        }

        public Response<List<Alias>> GetByAgent(int agentId)
        {
            Response<List<Alias>> response = new Response<List<Alias>>();
            try
            {
                response.Data = context.Alias
                    .Where(a => a.AgentId == agentId)
                    .ToList();
                response.Success = response.Data.Count > 0;
                response.Message = response.Success ? "" : $"Aliases for Agent ID {agentId} not found";
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = $"Aliases for Agent ID {agentId} not found";
                return response;
            }

        }

        public Response<Alias> Insert(Alias alias)
        {
            Response<Alias> response = new Response<Alias>();

            try
            {
                context.Alias.Add(alias);
                response.Success = context.SaveChanges() == 1;
                response.Data = alias;
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = $"Could not add Alias";
                return response;
            }
        }

        public Response Update(Alias alias)
        {
            Response response = new Response();
            try
            {
                context.Alias.Update(alias);
                response.Success = context.SaveChanges() == 1;
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = $"Could not update Alias with ID {alias.AliasId}";
                return response;
            }
        }
    }
}
