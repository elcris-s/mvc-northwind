namespace MVC_NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Territories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Territories()
        {
            Employees = new HashSet<Employees>();
        }

        [Key]
        [StringLength(20)]
        [Display(Name = "ID Territorio")]
        public string TerritoryID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Descripcion del Territorio")]
        public string TerritoryDescription { get; set; }

        [Display(Name = "ID Region")]
        public int RegionID { get; set; }

        public virtual Region Region { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
