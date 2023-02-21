using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeUI.Models
{
    public class FilterModel
    {
        public int? HouseholdID { get; set; }
        public int? SpendingsID { get; set; }
        public string Name { get; set; }
        public double?  Amount { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
