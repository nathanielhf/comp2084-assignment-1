namespace nathanielhf_comp2084_assignment_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [Key]
        public int item_id { get; set; }

        public int department_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        public decimal price { get; set; }

        public virtual Department Department { get; set; }
    }
}
