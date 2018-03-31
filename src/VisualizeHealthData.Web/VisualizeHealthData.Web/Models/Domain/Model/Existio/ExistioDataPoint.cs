using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualizeHealthData.Web.Models.Domain.Model.Existio
{
    public class ExistioDataPoint
    {
        public DateTime Date { get; set; }
        public decimal? Value { get; set; }

        public ExistioDataPoint()
        {
            
        }

        public ExistioDataPoint(ExistioResult dto)
        {
            Date = dto.date;
            Value = dto.value;
        }
    }
}