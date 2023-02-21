using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Spending
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Boş geçmeyiniz")]
        public string SpendingArea { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        public ICollection<Expense> Expense { get; set; }

    }
}
