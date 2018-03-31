using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualizeHealthData.Web.Models.Domain.Model.Existio
{
    public class ExistioResultSetDTO
    {
        public int count { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
        public List<ExistioResult> results { get; set; }
    }
    
    public class ExistioResult
    {
        public decimal? value { get; set; }
        public DateTime date { get; set; }
    }

    public class ExistioToken
    {
        public string Token { get; set; }
    }
}