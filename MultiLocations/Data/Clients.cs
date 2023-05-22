namespace MultiLocations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            Locations = new HashSet<Locations>();
        }

        [Key]
        public int IDClient { get; set; }

        [Required]
        [StringLength(30)]
        public string Nom { get; set; }

        [Required]
        [StringLength(30)]
        public string Adresse { get; set; }

        [Required]
        [StringLength(15)]
        public string Ville { get; set; }

        [Required]
        [StringLength(15)]
        public string Province { get; set; }

        [Required]
        [StringLength(7)]
        public string CodePostal { get; set; }

        [Required]
        [StringLength(50)]
        public string Courriel { get; set; }

        [Required]
        [StringLength(12)]
        public string Telephone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Locations> Locations { get; set; }
    }
}
