﻿using System;
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

                new MetricTypeViewModel("Weight", ExistioDataType.weight, "This is my weight, based on the wi-fi connected Withings scale.", showLatest:true),
                new MetricTypeViewModel("Steps", ExistioDataType.steps, "This is the amount of daily steps", showLatest:false),
                new MetricTypeViewModel("Meditation", ExistioDataType.meditation_min, "This is how many minutes I meditate pr day, based on the Headspace app."),
                new MetricTypeViewModel("Mood", ExistioDataType.mood, "This is a 1-5 rating of my mood a given day."),
                new MetricTypeViewModel("Energy", ExistioDataType.energy, "This is my total energi consumption on a given day."),

            };
        }
    }

    public class MetricTypeViewModel
    {
        public string Headline { get; set; }
        public string Description { get; set; }
        public ExistioDataType DataType { get; set; }
        public List<ExistioDataPoint> Data { get; set; }
        public bool ShowLatest { get; set; }

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

                    if (value > 0)
                    {
                     

                        graph.Add(new Tuple<string, decimal>(
                            day.ToString("dd/MM", CultureInfo.InvariantCulture),
                            value));

                    }
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
                    case ExistioDataType.energy:
                        return "kcal";
                    case ExistioDataType.steps:
                        return "steps";


                }
                return string.Empty;
            }
        }

        public MetricTypeViewModel(string headline,
            ExistioDataType dataType,
            string description = "",
            bool showLatest = false)
        {
            this.Headline = headline;
            this.DataType = dataType;
            this.Description = description;
            this.ShowLatest = showLatest;
            this.Data = new List<ExistioDataPoint>();
        }
    }
}