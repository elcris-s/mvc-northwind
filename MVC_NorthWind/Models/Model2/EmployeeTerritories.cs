namespace MVC_NorthWind.Models.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmployeeTerritories
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID Empleado")]
        public int EmployeeID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        [Display(Name = "ID Territorio")]
        public string TerritoryID { get; set; }
    }
}
