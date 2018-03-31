using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using VisualizeHealthData.Web.Models;
using VisualizeHealthData.Web.Models.ViewModels;

namespace VisualizeHealthData.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Home

        public ActionResult Index()
        {
            return RedirectToAction("Index", "HealthDashboard");
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/UserUploads/"), fileName);
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("Index");
        }


        #endregion

    }
}