namespace MVC_NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customers()
        {
            Orders = new HashSet<Orders>();
            CustomerDemographics = new HashSet<CustomerDemographics>();
        }

        [Key]
        [Display(Name = "ID Cliente")]
        [StringLength(5)]
        public string CustomerID { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Nombre de la Compañia")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nombre del Cliente")]
        public string ContactName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Titulo del Cliente")]
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

        [Required]
        [StringLength(24)]
        [Display(Name = "FAX")]
        public string Fax { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDemographics> CustomerDemographics { get; set; }
    }
}
