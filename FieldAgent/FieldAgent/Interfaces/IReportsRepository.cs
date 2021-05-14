using System.Collections.Generic;
using FieldAgent.DTOs;

namespace FieldAgent.Interfaces
{
    public interface IReportsRepository
    {
        Response<List<TopAgentListItem>> GetTopAgents();
        Response<List<PensionListItem>> GetPensionList(int agencyId);
        Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId);
    }
}
