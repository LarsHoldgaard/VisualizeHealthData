using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using VisualizeHealthData.Web.Models.Domain.Model.Existio;

namespace VisualizeHealthData.Web.Models.ViewModels.HealthDashboard
{
    public class HealthDashboardOverviewViewModel
    {
        public List<MetricTypeViewModel> Metrics { get; set; }

        public HealthDashboardOverviewViewModel()
        {
            this.Metrics = new List<MetricTypeViewModel>()
            {
                new MetricTypeViewModel("Weight", ExistioDataType.weight, "This is my weight, based on the wi-fi connected Withings scale."),
                new MetricTypeViewModel("Meditation", ExistioDataType.meditation_min, "This is how many minutes I meditate pr day, based on the Headspace app."),
            };
        }
    }

    public class MetricTypeViewModel
    {
        public string Headline { get; set; }
        public string Description { get; set; }
        public ExistioDataType DataType { get; set; }
        public List<ExistioDataPoint> Data { get; set; }

        public List<Tuple<string, decimal>> GraphData
        {
            get
            {
                if (!Data.Any())
                {
                    return new List<Tuple<string, decimal>>();
                }

                List<Tuple<string, decimal>> graph = new List<Tuple<string, decimal>>();

                DateTime start = DateTime.Now.Date.AddDays(-7);
                DateTime end = DateTime.Now.Date;
                
                var lastMonth = Enumerable.Range(0, 1 + end.Subtract(start).Days)
                    .Select(offset => start.AddDays(offset))
                    .ToArray();

                foreach (var day in lastMonth)
                {
                    var existingDataPoint = Data.FirstOrDefault(c => c.Date.Date == day.Date);
                    decimal value = 0.0m;

                    if (existingDataPoint != null &&
                        existingDataPoint.Value.HasValue)
                    {
                        value = existingDataPoint.Value.Value;
                    }


                    graph.Add(new Tuple<string, decimal>(
                        day.ToString("dd/MM", CultureInfo.InvariantCulture),
                        value));
                }

                return graph;
            }
        }

        public string Unit
        {
            get
            {
                switch (DataType)
                {
                    case ExistioDataType.weight:
                        return "kg";
                    case ExistioDataType.meditation_min:
                        return "min";

                }
                return string.Empty;
            }
        }

        public MetricTypeViewModel(string headline, ExistioDataType dataType, string description = "")
        {
            this.Headline = headline;
            this.DataType = dataType;
            this.Description = description;
            this.Data = new List<ExistioDataPoint>();
        }
    }
}