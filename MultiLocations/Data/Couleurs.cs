namespace MultiLocations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Couleurs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Couleurs()
        {
            Vehicules = new HashSet<Vehicules>();
        }

        [Key]
        [StringLength(4)]
        public string IDCouleur { get; set; }

        [Required]
        [StringLength(15)]
        public string NomCouleur { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicules> Vehicules { get; set; }
    }
}
