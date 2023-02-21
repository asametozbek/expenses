using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Role:IEntity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string  RoleName { get; set; }
        [StringLength(30)]
        public string  Description { get; set; }
        public ICollection<Household> Household { get; set; }

    }
}
