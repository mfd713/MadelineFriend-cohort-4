using FieldAgent.Entities;
using FieldAgent.Interfaces;
using FieldAgentWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    public class AgentsController : ControllerBase
    {
        private IAgentRepository agentRepository;

        public AgentsController(IAgentRepository agentRepository)
        {
            this.agentRepository = agentRepository;
        }

        [HttpGet]
        [Route("{id}", Name = "GetAgent")]
        public IActionResult GetAgent(int id)
        {
            var response = agentRepository.Get(id);

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllAgents()
        {
            var response = agentRepository.GetAll();

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost, Authorize]
        public IActionResult Create(Agent agent)
        {
            var response = agentRepository.Insert(agent);

            if (response.Success)
            {
                return CreatedAtRoute(nameof(GetAgent), new { id = response.Data.AgentId }, agent);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("{id}/missions")]
        public IActionResult GetMissions(int id)
        {
            var response = agentRepository.GetMissions(id);

            if (response.Success)
            {
                List<AgentMissionsModel> missions = new List<AgentMissionsModel>();

                foreach (Mission m in response.Data)
                {

                    missions.Add(new AgentMissionsModel
                    {
                        MissionId = m.MissionId,
                        CodeName = m.CodeName,
                        StartDate = m.StartDate,
                        ActualEndDate = m.ActualEndDate,
                        ProjectedEndDate = m.ProjectedEndDate,
                        OperationalCost = m.OperationalCost,
                        Notes = m.Notes,
                    });
                }
                return Ok(missions);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut, Authorize]
        [Route("{id}")]
        public IActionResult Edit(Agent agent, int id)
        {
            if(id != agent.AgentId)
            {
                return BadRequest("Id must match route");
            }

            var getAgentResult = agentRepository.Get(agent.AgentId);

            if (!getAgentResult.Success)
            {
                return NotFound($"Agent {agent.AgentId} not found");
            }

            getAgentResult.Data.DateOfBirth = agent.DateOfBirth;
            getAgentResult.Data.FirstName = agent.FirstName;
            getAgentResult.Data.LastName = agent.LastName;
            getAgentResult.Data.Height = agent.Height;

            var response = agentRepository.Update(getAgentResult.Data);

            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpDelete, Authorize]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var getAgentResult = agentRepository.Get(id);

            if (!getAgentResult.Success)
            {
                return NotFound($"Agent {id} not found");
            }

            var response = agentRepository.Delete(id);

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
