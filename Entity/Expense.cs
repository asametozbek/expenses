using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Expense
    {
        public int Id { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage ="Boş geçilemez")]
        public string Description { get; set; }
        [Required]
        [Range(0.1, Double.MaxValue, ErrorMessage = "Bu alan {0} arasında bir değer olmalıdır. {1}.")]
        public double Amount { get; set; }
        public DateTime DateofEntry { get; set; }

        public virtual Household Household { get; set; }
        public virtual Spending  Spending{ get; set; }
        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "bu alan {0} must be greater than {1}.")]

        public int HouseholdId { get; set; }
        [Required]
        public int SpendingId { get; set; }
        public bool? IsPersonal { get; set; }
    }
}
