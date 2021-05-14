using System.Collections.Generic;
using FieldAgent.Entities;

namespace FieldAgent.Interfaces
{
    public interface ISecurityClearanceRepository
    {
        Response<SecurityClearance> Get(int securityClearanceId);
        Response<List<SecurityClearance>> GetAll();
    }
}
