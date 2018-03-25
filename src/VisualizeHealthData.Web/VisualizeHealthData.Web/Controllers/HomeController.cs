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

        public ActionResult About()
        {

            XDocument doc = XDocument.Load(Server.MapPath("~/UserUploads/eksporter.xml"));

            var elements = doc.Root.Elements();

            if (!elements.Any())
            {
                return null;
            }

            List<Record> rec = new List<Record>();
            foreach (var element in elements.Where(c=>c.Name.LocalName == "Record").ToList())
            {
                var record = new Record(element);
                rec.Add(record);
            }

            RecordOverviewViewModel vm = new RecordOverviewViewModel();


            var filteredRecs = rec.Where(c => c.RecordType.HasValue && c.RecordSource.HasValue).ToList();
            foreach (var record in filteredRecs)
            {
                var typeKey = record.RecordType.Value.ToString();
                var sourceKey = record.RecordSource.Value.ToString();

                if (vm.Records.Records.ContainsKey(typeKey))
                {
                    if (vm.Records.Records[typeKey].ContainsKey(sourceKey))
                    {
                        vm.Records.Records[typeKey][sourceKey].Add(record);
                    }
                    else
                    {
                        vm.Records.Records[typeKey].Add(sourceKey,new List<Record>() {record});
                    }
                }
                else
                {
                    var dicValue = new Dictionary<string, List<Record>>();
                    dicValue.Add(sourceKey,new List<Record>(){record});
                    vm.Records.Records.Add(typeKey, dicValue);
                }
            }

            ViewBag.Rec = rec;
            ViewBag.Record = rec.Count;



            return View(vm);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}