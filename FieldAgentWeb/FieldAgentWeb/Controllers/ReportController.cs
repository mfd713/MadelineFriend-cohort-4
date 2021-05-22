using FieldAgent.Entities;
using FieldAgent.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldAgentWeb.Controllers
{
    public class ReportController : Controller
    {
        private IReportsRepository reportRepo;
        private IAgencyRepository agencyRepository;
        private ISecurityClearanceRepository securityRepo;

        public ReportController(IReportsRepository reportRepo, IAgencyRepository agencyRepo, ISecurityClearanceRepository securityRepo)
        {
            this.reportRepo = reportRepo;
            agencyRepository = agencyRepo;
            this.securityRepo = securityRepo;
        }

        [Route("/report")]
        public IActionResult Index()
        {
            List<Agency> agencies = agencyRepository.GetAll().Data;
            List<SecurityClearance> clearances = securityRepo.GetAll().Data;

            ViewBag.ListOfAgencies = agencies;
            ViewBag.ListOfClearances = clearances;

            return View();
        }

        [Route("/report/topagents")]
        public IActionResult ViewTopAgents()
        {
            var response = reportRepo.GetTopAgents();

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        [Route("/report/pensionlist/{id}")]
        public IActionResult ViewPensionList(int id)
        {
            var response = reportRepo.GetPensionList(id);

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                ModelState.AddModelError("Error", response.Message);
                return RedirectToAction("Index");
            }
        }

        [Route("/report/securityaudit/{agencyId}/{clearanceId}")]
        public IActionResult ViewSecurityAudit(int agencyId, int clearanceId)
        {
            var response = reportRepo.AuditClearance(clearanceId, agencyId);

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                ModelState.AddModelError("Error", response.Message);
                return RedirectToAction("Index");
            }
        }

    }
}
