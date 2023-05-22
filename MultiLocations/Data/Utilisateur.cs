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
                string np = "Ind�fini";

                switch(Poste.ToUpper().Trim() )
                {
                    case "A": np = "Administrateur";
                        break;
                    case "GC": np = "G�rant des comptes";
                        break;
                    case "AV":  np = "Associ�s aux ventes";
                        break;
                }

                return np;
            } 
        }
    }
}
