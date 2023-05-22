namespace MultiLocations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehicules
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicules()
        {
            Locations = new HashSet<Locations>();
        }

        [Key]
        [StringLength(23)]
        public string NIV { get; set; }

        [Required]
        [StringLength(15)]
        public string Model { get; set; }

        [Required]
        [StringLength(15)]
        public string Marque { get; set; }

        public byte IDType { get; set; }

        [Required]
        [StringLength(4)]
        public string IDCouleur { get; set; }

        [Required]
        [StringLength(4)]
        public string Annee { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal Valeur { get; set; }

        [Required]
        [StringLength(12)]
        public string Transmission { get; set; }

        public bool AirClim { get; set; }

        public bool AntiDemarreur { get; set; }

        public virtual Couleurs Couleurs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Locations> Locations { get; set; }

        public virtual Types Types { get; set; }

        public string Aff { 
            get { return NIV + "  (" + Model + ")"; }
        }
    }
}
