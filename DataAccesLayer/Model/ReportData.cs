using System;

namespace DataAccesLayer
{

    /// <summary>
    /// Model About ReportData
    /// </summary>
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
