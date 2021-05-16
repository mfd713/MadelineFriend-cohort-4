using System;
using System.Collections.Generic;
using System.Linq;
using FieldAgent.Entities;
using FieldAgent.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL
{
    public class SecurityClearanceEFRepo : ISecurityClearanceRepository
    {
        private FieldAgentsDbContext context;

        public SecurityClearanceEFRepo(FieldAgentsDbContext context)
        {
            this.context = context;
        }

        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            Response<SecurityClearance> response = new Response<SecurityClearance>();
            try
            {
                response.Data = context.SecurityClearance.Find(securityClearanceId);
                response.Success = response.Data != null;
                response.Message = response.Success ? "" : $"Clearance with ID {securityClearanceId} not found";
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = $"Clearance with ID {securityClearanceId} not found";
                return response;
            }
        }

        public Response<List<SecurityClearance>> GetAll()
        {
           Response<List<SecurityClearance>> response = new Response<List<SecurityClearance>>();

            try
            {
                response.Data = context.SecurityClearance.ToList();
                response.Success = response.Data.Count > 0;
                response.Message = response.Success ? "" : "No Security Clearances found";
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = "No Security Clearances found";
                return response;
            }

        }
    }
}
