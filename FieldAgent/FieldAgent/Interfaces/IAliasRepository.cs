using System.Collections.Generic;
using FieldAgent.Entities;

namespace FieldAgent.Interfaces
{
    public interface IAliasRepository
    {
        Response<Alias> Insert(Alias alias);
        Response Update(Alias alias);
        Response Delete(int aliasId);
        Response<Alias> Get(int aliasId);
        Response<List<Alias>> GetByAgent(int agentId);
    }
}
