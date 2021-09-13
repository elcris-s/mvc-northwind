namespace MVC_NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order Details")]
    public partial class Order_Details
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "ID Orden")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "ID Producto")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Column(TypeName = "money")]
        [Required]
        [Display(Name = "Precio Unitario")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public short Quantity { get; set; }

        [Required]
        [Display(Name = "Descuento")]
        [Range(0,1, ErrorMessage = "El descuento debe estar entre 0 y 1")]
        public float Discount { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Products Products { get; set; }
    }
}
