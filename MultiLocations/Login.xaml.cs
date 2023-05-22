using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MultiLocations
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        DataBase db = new DataBase();
        Utilisateur member;
        public Login()
        {
            InitializeComponent();
            txtNom.Focus();
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try { 
            string nom = txtNom.Text.Trim();             // Vérifier que la saisie n'est pas vide
            string mdp = txtMdp.Password.Trim();

            if (nom == string.Empty || mdp == string.Empty)  // Si la saisie est vide : on affiche l'erreur
            {
                lblError.Content = string.Empty;
                lblError.Content = "Veuillez saisir toutes les informations demandées.";
                return;
            }


            member = (Utilisateur)db.Utilisateur.Find(nom); // Selectionner l'enregistrement de l'utilisateur

            if (member == null) // Si l'utilisateur n'existe pas : Afficher l'erreur
            {
                lblError.Content = string.Empty;
                lblError.Content = "Utilisateur introuvable.";
                return;
            }

            if (member.MotDePasse != mdp) // Si le mot de passe est invalide : Afficher l'erreur
            {
                lblError.Content = string.Empty;
                lblError.Content = "Mot de passe erroné.";
                return;
            }

            // Si les données sont correctes : Afficher la fenêtre d'acceuil et lui transmettre l'utilisateur 
            Acceuil fen = new Acceuil(member, null);

            this.Close();
            fen.Show();

        }catch (Exception ex) // Si une exception est déclenché
            {
                MessageBox.Show(ex.Message, "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
       
        }
    }
}
