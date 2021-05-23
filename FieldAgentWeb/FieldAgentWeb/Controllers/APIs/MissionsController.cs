using FieldAgent.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldAgentWeb.Models;
using FieldAgent.Entities;
using Microsoft.AspNetCore.Authorization;

namespace FieldAgentWeb.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private IMissionRepository missionRepo;

        public MissionsController(IMissionRepository missionRepo)
        {
            this.missionRepo = missionRepo;
        }

        [Route("{id}", Name ="GetMission")]
        [HttpGet, Authorize]
        public IActionResult GetMission(int id)
        {
            var response = missionRepo.Get(id);

            if (response.Success)
            {
                return Ok(new
                {
                    MissionId = response.Data.MissionId,
                    CodeName = response.Data.CodeName,
                    StartDate = response.Data.StartDate,
                    ActualEndDate = response.Data.ActualEndDate,
                    ProjectedEndDate = response.Data.ProjectedEndDate,
                    OperationalCost = response.Data.OperationalCost,
                    Notes = response.Data.Notes
                });
            }

            else
            {
                return BadRequest();
            }
        }

        [Route("agencies/{id}")]
        [HttpGet, Authorize]
        public IActionResult GetByAgency(int id)
        {
            var response = missionRepo.GetByAgency(id);

            if (response.Success)
            {
                var missions = new List<AgencyMissionsModel>();

                foreach (var mission in response.Data)
                {
                    missions.Add(new AgencyMissionsModel
                    {
                        MissionId = mission.MissionId,
                        CodeName = mission.CodeName,
                        StartDate = mission.StartDate,
                        ActualEndDate = mission.ActualEndDate,
                        ProjectedEndDate = mission.ProjectedEndDate,
                        OperationalCost = mission.OperationalCost,
                        Notes = mission.Notes,
                        AgencyName = mission.Agency.ShortName
                    });
                }
                return Ok(missions);
            }

            else
            {
                return BadRequest();
            }
        }

        [Route("agents/{id}")]
        [HttpGet, Authorize]
        public IActionResult GetbyAgent(int id)
        {
            var response = missionRepo.GetByAgent(id);

            if (response.Success)
            {
                var missions = new List<AgentMissionsModel>();

                foreach (var mission in response.Data)
                {
                    missions.Add(new AgentMissionsModel
                    {
                        MissionId = mission.MissionId,
                        CodeName = mission.CodeName,
                        StartDate = mission.StartDate,
                        ActualEndDate = mission.ActualEndDate,
                        ProjectedEndDate = mission.ProjectedEndDate,
                        OperationalCost = mission.OperationalCost,
                        Notes = mission.Notes
                    });
                }
                return Ok(missions);
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpPost, Authorize]
        public IActionResult Create(Mission mission)
        {
            var response = missionRepo.Insert(mission);

            if (response.Success)
            {
                return CreatedAtRoute(nameof(GetMission), new { id = response.Data.MissionId }, mission);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut, Authorize]
        [Route("{id}")]
        public IActionResult Edit(Mission mission, int id)
        {
            if (id != mission.MissionId)
            {
                return BadRequest("Id must match route");
            }

            var getMissionResult = missionRepo.Get(mission.MissionId);

            if (!getMissionResult.Success)
            {
                return NotFound($"Mission {mission.MissionId} not found");
            }

            getMissionResult.Data.CodeName= mission.CodeName;
            getMissionResult.Data.StartDate = mission.StartDate;
            getMissionResult.Data.ProjectedEndDate = mission.ProjectedEndDate;
            getMissionResult.Data.ActualEndDate = mission.ActualEndDate;
            getMissionResult.Data.AgencyId = mission.AgencyId;
            getMissionResult.Data.Notes = mission.Notes;
            getMissionResult.Data.OperationalCost = mission.OperationalCost;

            var response = missionRepo.Update(getMissionResult.Data);

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
            var getMissionResponse = missionRepo.Get(id);

            if (!getMissionResponse.Success)
            {
                return NotFound($"Mission {id} not found");
            }

            var response = missionRepo.Delete(id);

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
