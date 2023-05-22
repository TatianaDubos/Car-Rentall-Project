namespace MultiLocations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Windows.Media.Media3D;

    public partial class Locations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Locations()
        {
            Paiements = new HashSet<Paiements>();
        }

        [Key]
        public int IDLocation { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateDebut { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateFin { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal MontantPaie { get; set; }

        public byte NombrePaie { get; set; }

        [Required]
        [StringLength(23)]
        public string NIV { get; set; }

        public int IDClient { get; set; }

        public int IDTerme { get; set; }

        public int KmDebut { get; set; }

        public int? KmFin { get; set; }

        [Column(TypeName = "money")]
        public decimal? PaieSurcharge { get; set; }

        public virtual Clients Clients { get; set; }

        public virtual Termes_Location Termes_Location { get; set; }

        public virtual Vehicules Vehicules { get; set; }

        public string Aff {
            get {
            return "Location #" + IDLocation ;
            } 
        }

        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paiements> Paiements { get; set; }
    
    
    }


}
