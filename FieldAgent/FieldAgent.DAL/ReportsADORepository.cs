using System.Collections.Generic;
using System.Linq;
using FieldAgent.Entities;
using FieldAgent.Interfaces;
using FieldAgent.DTOs;

namespace FieldAgent.DAL
{
    public class ReportsADORepository : IReportsRepository
    {
        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            throw new System.NotImplementedException();
        }

        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            throw new System.NotImplementedException();
        }

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            throw new System.NotImplementedException();
        }
    }
}
