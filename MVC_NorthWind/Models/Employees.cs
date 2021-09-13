namespace MVC_NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            Employees1 = new HashSet<Employees>();
            Orders = new HashSet<Orders>();
            Territories = new HashSet<Territories>();
        }

        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Puesto")]
        public string Title { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Titulo de Cortesia")]
        public string TitleOfCourtesy { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "Fecha de Contratacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? HireDate { get; set; }

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
        [Display(Name = "Telefono del Hogar")]
        public string HomePhone { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Numero de Extension")]
        public string Extension { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Fotografia")]
        [DataType(DataType.Upload)]
        public byte[] Photo { get; set; }

        [Required]
        [Display(Name = "Notas")]
        public string Notes { get; set; }

        [Display(Name = "Superior")]
        public int? ReportsTo { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employees> Employees1 { get; set; }

        public virtual Employees Employees2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Territories> Territories { get; set; }
    }
}
