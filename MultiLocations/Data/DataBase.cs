using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MultiLocations
{
    public partial class DataBase : DbContext
    {
        public DataBase()
            : base("name=DataBase")
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Couleurs> Couleurs { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Termes_Location> Termes_Location { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }
        public virtual DbSet<Vehicules> Vehicules { get; set; }
        public virtual DbSet<Paiements> Paiements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>()
                .Property(e => e.CodePostal)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.Telephone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.Locations)
                .WithRequired(e => e.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Couleurs>()
                .Property(e => e.IDCouleur)
                .IsUnicode(false);

            modelBuilder.Entity<Couleurs>()
                .HasMany(e => e.Vehicules)
                .WithRequired(e => e.Couleurs)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Locations>()
                .Property(e => e.MontantPaie)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Locations>()
                .Property(e => e.NIV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Locations>()
                .Property(e => e.PaieSurcharge)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Locations>()
                .HasMany(e => e.Paiements)
                .WithRequired(e => e.Locations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Termes_Location>()
                .Property(e => e.Surprime)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Termes_Location>()
                .HasMany(e => e.Locations)
                .WithRequired(e => e.Termes_Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Types>()
                .HasMany(e => e.Vehicules)
                .WithRequired(e => e.Types)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilisateur>()
                .Property(e => e.Poste)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vehicules>()
                .Property(e => e.NIV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vehicules>()
                .Property(e => e.IDCouleur)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicules>()
                .Property(e => e.Annee)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vehicules>()
                .Property(e => e.Valeur)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Vehicules>()
                .Property(e => e.Transmission)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicules>()
                .HasMany(e => e.Locations)
                .WithRequired(e => e.Vehicules)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Paiements>()
                .Property(e => e.Montant)
                .HasPrecision(10, 4);
        }
    }
}
