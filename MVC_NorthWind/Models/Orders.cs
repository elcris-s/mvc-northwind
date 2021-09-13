namespace MVC_NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        [Key]
        [Required]
        [Display(Name = "ID Orden")]
        public int OrderID { get; set; }

        [StringLength(5)]
        [Required]
        [Display(Name = "ID Cliente")]
        public string CustomerID { get; set; }

        [Required]
        [Display(Name = "ID Empleado")]
        public int? EmployeeID { get; set; }

        [Required]
        [Display(Name = "Fecha de la Orden")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OrderDate { get; set; }

        [Required]
        [Display(Name = "Fecha Requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? RequiredDate { get; set; }

        [Required]
        [Display(Name = "Fecha de Envio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ShippedDate { get; set; }

        [Display(Name = "Compañia Distribuidora")]
        public int? ShipVia { get; set; }

        [Column(TypeName = "money")]
        [Required]
        [Display(Name = "Costo de Transporte")]
        public decimal? Freight { get; set; }

        [StringLength(40)]
        [Required]
        [Display(Name = "Nombre de Envio")]
        public string ShipName { get; set; }

        [StringLength(60)]
        [Required]
        [Display(Name = "Direccion de Envio")]
        public string ShipAddress { get; set; }

        [StringLength(15)]
        [Required]
        [Display(Name = "Ciudad de Envio")]
        public string ShipCity { get; set; }

        [StringLength(15)]
        [Display(Name = "Region de Envio")]
        public string ShipRegion { get; set; }

        [StringLength(10)]
        [Required]
        [Display(Name = "Codigo Postal de Envio")]
        public string ShipPostalCode { get; set; }

        [StringLength(15)]
        [Required]
        [Display(Name = "Pais de Envio")]
        public string ShipCountry { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Employees Employees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }

        public virtual Shippers Shippers { get; set; }
    }
}
