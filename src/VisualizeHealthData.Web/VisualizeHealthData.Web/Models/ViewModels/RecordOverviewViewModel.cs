using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualizeHealthData.Web.Models.ViewModels
{
    public class RecordOverviewViewModel
    {
        public RecordsViewModel Records { get; set; }


        public RecordOverviewViewModel()
        {
            Records = new RecordsViewModel();      
        }
    }

    public class RecordsViewModel
    {
        public Dictionary<string,Dictionary<string,List<Record>>> Records = new Dictionary<string,Dictionary<string, List<Record>>>();
        
        public RecordsViewModel()
        {
            //Records.Add("BodyMassIndex", new Dictionary<string, List<Record>>());
            //Records.Add("BodyMass", new Dictionary<string, List<Record>>());
            //Records.Add("FlightsClimbed", new Dictionary<string, List<Record>>());
            //Records.Add("Steps", new Dictionary<string, List<Record>>());
            //Records.Add("SleepAnalysis", new Dictionary<string, List<Record>>());
            //Records.Add("DistanceCycling", new Dictionary<string, List<Record>>());
            //Records.Add("Meditation", new Dictionary<string, List<Record>>());
            //Records.Add("DistanceWalkCycling", new Dictionary<string, List<Record>>());

        }
    }
}