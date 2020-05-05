using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileStore.Models;

namespace MobileStore.Controllers
{
    public class HomeController : Controller
    {
        MobileContext db;
        public HomeController(MobileContext context)
        {
            db = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(db.Phones.ToList());
        }
        [Authorize]
        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.PhoneId = id;
            return View();
        }
        [Authorize]
        [HttpPost]
        public RedirectResult Buy(Order order)
        {
            db.Orders.Add(order);
            var phone = db.Phones.Where(u=>u.Id==2).FirstOrDefault();
            phone.Balance++;
            db.SaveChanges();
            return new RedirectResult("/Home/");
        }
        [Authorize]
        [HttpPost]
        public RedirectResult Exit()
        {
            return new RedirectResult("/Account/Login");
        }
    }
}
