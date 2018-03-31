using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisualizeHealthData.Web.Models.Domain.Model.Existio;
using VisualizeHealthData.Web.Models.Domain.Services;
using VisualizeHealthData.Web.Models.ViewModels.HealthDashboard;

namespace VisualizeHealthData.Web.Controllers
{
    public class HealthDashboardController : Controller
    {
        private ExistioService _existioService;

        public HealthDashboardController()
        {
            _existioService = new ExistioService();
        }

        // GET: HealthDashboard
        public ActionResult Index()
        {
            HealthDashboardOverviewViewModel vm = new HealthDashboardOverviewViewModel();
            foreach (var metricType in vm.Metrics)
            {
                var data = _existioService.Get(metricType.DataType);
                metricType.Data = data;
            }  

            return View(vm);
        }

       
    }
}