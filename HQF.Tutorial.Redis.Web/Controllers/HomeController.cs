using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HQF.Tutorial.Redis.Web.Models;

namespace HQF.Tutorial.Redis.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult GetSysInfo()
        {
            //Make sure <trust level="Full" /> is in <system.Web>
            SysInfo info = new SysInfo(System.Web.HttpContext.Current);
            SysInfoViewModel model = new SysInfoViewModel(info.GetData());
            return View(model);
        }
    }
}