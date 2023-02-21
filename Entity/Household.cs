using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Household:IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
        [StringLength(30)]
        public string Password { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string SessionLogin { get; set; }
        public virtual List<Expense> Expenses { get; set; }

    }
}
