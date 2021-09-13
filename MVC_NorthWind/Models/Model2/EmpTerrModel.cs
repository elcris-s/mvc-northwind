using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MVC_NorthWind.Models.Model2
{
    public partial class EmpTerrModel : DbContext
    {
        public EmpTerrModel()
            : base("name=EmpTerrModel")
        {
        }

        public virtual DbSet<EmployeeTerritories> EmployeeTerritories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
