namespace MVC_NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        [Key]
        [Display(Name = "ID Producto")]

        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Nombre Producto")]

        public string ProductName { get; set; }

        [Display(Name = "ID Suplidor")]
        public int? SupplierID { get; set; }

        [Display(Name = "ID Categoria")]
        public int? CategoryID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Cantidad por Unidad")]
        public string QuantityPerUnit { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Precio Unitario")]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Display(Name = "Unidades en Stock")]
        public short? UnitsInStock { get; set; }

        [Required]
        [Display(Name = "Unidades en Ordenes")]
        public short? UnitsOnOrder { get; set; }

        [Required]
        [Display(Name = "Nivel de Reordenamiento")]
        public short? ReorderLevel { get; set; }

        [Required]
        [Display(Name = "¿Esta Descontinuado?")]
        public bool Discontinued { get; set; }

        public virtual Categories Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }

        public virtual Suppliers Suppliers { get; set; }
    }
}
