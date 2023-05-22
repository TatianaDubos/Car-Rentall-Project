namespace MultiLocations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utilisateur")]
    public partial class Utilisateur
    {
        [Key]
        [StringLength(30)]
        public string NomUtilisateur { get; set; }

        [Required]
        [StringLength(30)]
        public string MotDePasse { get; set; }

        [Required]
        [StringLength(2)]
        public string Poste { get; set; }

        public string NomPoste { 
            get 
            {
                string np = "Indéfini";

                switch(Poste.ToUpper().Trim() )
                {
                    case "A": np = "Administrateur";
                        break;
                    case "GC": np = "Gérant des comptes";
                        break;
                    case "AV":  np = "Associés aux ventes";
                        break;
                }

                return np;
            } 
        }
    }
}
