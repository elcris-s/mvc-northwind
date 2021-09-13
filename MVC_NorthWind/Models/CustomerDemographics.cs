namespace MVC_NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CustomerDemographics
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerDemographics()
        {
            Customers = new HashSet<Customers>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "Tipo Cliente ID")]
        public string CustomerTypeID { get; set; }

        [Display(Name = "Descripcion Cliente")]
        public string CustomerDesc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customers> Customers { get; set; }
    }
}
