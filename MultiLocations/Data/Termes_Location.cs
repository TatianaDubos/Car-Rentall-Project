namespace MultiLocations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Termes_Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Termes_Location()
        {
            Locations = new HashSet<Locations>();
        }

        [Key]
        public int IDTerme { get; set; }

        public byte NbrAnnees { get; set; }

        public int KmMax { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal Surprime { get; set; }

        public string Aff
        {
            get
            {
                return "Termes de location #" + IDTerme.ToString();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Locations> Locations { get; set; }
    }
}
