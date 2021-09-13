namespace MVC_NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Suppliers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Suppliers()
        {
            Products = new HashSet<Products>();
        }

        [Key]
        [Display(Name = "ID Suplidor")]
        public int SupplierID { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Nombre de la Compañia")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nombre de Contacto")]
        public string ContactName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Titulo de Contacto")]
        public string ContactTitle { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Direccion")]
        public string Address { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [StringLength(15)]
        [Display(Name = "Region")]
        public string Region { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Codigo Postal")]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Required]
        [StringLength(24)]
        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Display(Name = "Pagina Principal")]
        public string HomePage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
