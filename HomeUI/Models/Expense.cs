using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeUI.Models
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime DateofEntry { get; set; }
        public int HouseholdId { get; set; }
        public int SpendingId { get; set; }
    }
}
