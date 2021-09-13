namespace MVC_NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerCustomerDemo")]
    public partial class CustomerCustomerDemo
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        [Display(Name = "ID Cliente")]
        public string CustomerID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        [Display(Name = "ID Tipo de Cliente")]
        public string CustomerTypeID { get; set; }
    }
}
