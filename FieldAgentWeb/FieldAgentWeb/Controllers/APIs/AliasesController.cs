using FieldAgent.Entities;
using FieldAgent.Interfaces;
using FieldAgentWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldAgentWeb.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliasesController : ControllerBase
    {
        private IAliasRepository aliasRepo;

        public AliasesController(IAliasRepository aliasRepo)
        {
            this.aliasRepo = aliasRepo;
        }

        [HttpGet]
        [Route("{id}", Name = "GetAlias")]
        public IActionResult GetAlias(int id)
        {
            var response = aliasRepo.Get(id);

            if (response.Success)
            {
                return Ok(new {
                    response.Data.AliasId,
                    response.Data.AliasName,
                    response.Data.InterpolId,
                    response.Data.Persona,
                    response.Data.AgentId
                });
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet]
        [Route("agents/{id}")]
        public IActionResult GetByAgent(int id)
        {
            var response = aliasRepo.GetByAgent(id);

            if (response.Success)
            {
                var aliases = new List<AgentAliasesModel>();
                foreach (var alias in response.Data)
                {
                    aliases.Add(new AgentAliasesModel
                    {
                        AgentId = alias.AgentId,
                        AgentName = $"{alias.Agent.LastName}, {alias.Agent.FirstName}",
                        AliasId = alias.AliasId,
                        AliasName = alias.AliasName,
                        InterpolId = alias.InterpolId,
                        Persona = alias.Persona
                    });
                }
                return Ok(aliases);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Alias alias)
        {
            var response = aliasRepo.Insert(alias);

            if (response.Success)
            {
                return CreatedAtRoute(nameof(GetAlias), new { id = response.Data.AliasId }, alias);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(Alias alias, int id)
        {
            if(alias.AliasId != id)
            {
                return BadRequest("Id must match route");
            }

            var getAliasResult = aliasRepo.Get(id);

            if (!getAliasResult.Success)
            {
                return NotFound($"Alias {id} not found");
            }

            getAliasResult.Data.AgentId = alias.AgentId;
            getAliasResult.Data.AliasName = alias.AliasName;
            getAliasResult.Data.InterpolId = alias.InterpolId;
            getAliasResult.Data.Persona = alias.Persona;

            var response = aliasRepo.Update(getAliasResult.Data);

            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var getAliasResponse = aliasRepo.Get(id);

            if (!getAliasResponse.Success)
            {
                return NotFound($"Alias {id} not found");
            }

            var response = aliasRepo.Delete(id);

            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
