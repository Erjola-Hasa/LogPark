using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class ReportData
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double MinimumStayTime { get; set; }
        public double MaximumStayTime { get; set; }
        public int TotalCarsInParking { get; set; }
        public int AverageStay { get; set; }

    }
}
