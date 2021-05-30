using System;

namespace GSTFunction.Model
{
    public class expense
    {
        public string cost_centre { get; set; } = "Unknown";
        public string total { get; set; }
        public string payment_method { get; set; }
        public Double totalGSTamount { get; set; }
        public Double totalGSTexclusionamount { get; set; }
    }
}