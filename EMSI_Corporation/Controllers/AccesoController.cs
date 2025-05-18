using EMSI_Corporation.Data;
using EMSI_Corporation.Models;
using EMSI_Corporation.ViewModels;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;

namespace EMSI_Corporation.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDBContext _appDBContext;

        public AccesoController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
