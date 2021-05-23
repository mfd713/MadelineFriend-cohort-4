using FieldAgent;
using FieldAgent.DAL;
using FieldAgent.Entities;
using FieldAgent.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldAgentWeb.Controllers
{
    public class AgencyController : Controller
    {
        private IAgencyRepository agencyRepo;

        public AgencyController(IAgencyRepository agencyRepo)
        {
            this.agencyRepo = agencyRepo;
        }

        [Route("/agency")]
        public IActionResult Index()
        {
            var response = agencyRepo.GetAll();

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [Route("/agency/{id}")]
        public IActionResult Details(int id)
        {
            var response = agencyRepo.Get(id);

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [Route("/agency/add")]
        [HttpGet]
        public IActionResult Add()
        {
            var model = new Agency();
            return View(model);
        }

        [Route("/agency/add")]
        [HttpPost]
        public IActionResult Add(Agency agency)
        {
            var response = agencyRepo.Insert(agency);

            if (response.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(response.Message);

            }
        }

        [Route("/agency/edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = agencyRepo.Get((int)id);

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                return BadRequest(response.Message);

            }
        }

        [Route("/agency/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(int id, Agency agency)
        {
            if (id != agency.AgencyId)
            {
                return NotFound();
            }

            var response = agencyRepo.Update(agency);

            if (response.Success)
            {
                return RedirectToAction("Details", new { id = agency.AgencyId });
            }
            else
            {
                return BadRequest(response.Message);

            }

        }

        [Route("/agency/delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = agencyRepo.Get((int)id);

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                return BadRequest(response.Message);

            }
        }

        [Route("/agency/delete/{id}")]
        [HttpPost] 
        public IActionResult Delete(int id)
        {
            var response = agencyRepo.Delete(id);

            if (response.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(response.Message);

            }
        }

    }
}
