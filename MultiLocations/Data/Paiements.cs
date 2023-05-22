namespace MultiLocations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Paiements
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smallmoney")]
        public decimal Montant { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDLocation { get; set; }

        public virtual Locations Locations { get; set; }

        public string Aff
        {
            get
            {
                if (Montant == 0) return "Ø    Date :   " + Date.ToShortDateString() + "  -- Transaction annulée --";
                else
                return FenPaiements.compteur() + ")   Date :   " + Date.ToShortDateString() + "          Montant :   " + String.Format("{0:0.00}", Montant) + "$";
            }
        }

    }
}
