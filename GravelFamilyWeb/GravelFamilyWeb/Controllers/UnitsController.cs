using GravelFamily.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravelFamilyWeb.Controllers
{
    public class UnitsController : Controller
    {
        private GravelFamilyContext _context;

        public UnitsController(GravelFamilyContext context)
        {
            _context = context;
        }

        //GET Units
        public IActionResult Index()
        {
            return View(_context.Units.ToList());
        }
    }
}
