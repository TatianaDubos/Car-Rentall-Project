using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MultiLocations
{
    /// <summary>
    /// Logique d'interaction pour FenClients.xaml
    /// </summary>
    public partial class FenClients : Window
    {
        DataBase db = new DataBase();
        Utilisateur member;
        Clients select;
        bool update = false;
        bool nouv = false;
        

        public FenClients(Utilisateur actif, Clients selection)
        {
            InitializeComponent();
            member = actif; // utilisateur actif
            select = selection; // // Si un client doit etre selectionné

            Title += "/Connecté en tant que " + member.NomUtilisateur + " - " + member.NomPoste; // Modifier le titre de la fenetre
            binding(); // Faire la liaison entre nos controles et nos données


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            reset(); // Mettre nos controles dans leur état initial
            desactForm(); // desactiver notre formulaire

            if (select != null)  //Si l'utilisateur désire afficher les informations d'un client en particulier
            {
                ComboClients.SelectedValue = select.IDClient; // Selectionner le client

            }
        }
        private void reset() // Fonction rénitialiser tous nos contoles
        {
            update = false;
            nouv = false;
            lblID.Content = string.Empty;
            ComboClients.SelectedIndex = -1;
            ComboClients.IsEnabled = true;
            ListLocations.Visibility = Visibility.Hidden;
            lblLocations.Visibility = Visibility.Hidden;
            txtNom.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtVille.Text = string.Empty;
            txtProvince.Text = string.Empty; 
            txtCodePostal.Text = string.Empty;
            txtCourriel.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            btnAnnuler.IsEnabled = false;
            btnModifier.IsEnabled = false;
            btnNouvAjout.Content = "Nouveau";

        }
        private void desactForm() // Fonction pour désactiver tous nos controles
        {
            txtNom.IsEnabled = false;
            txtAdresse.IsEnabled = false;
            txtVille.IsEnabled = false;
            txtProvince.IsEnabled = false;
            txtCodePostal.IsEnabled = false;
            txtCourriel.IsEnabled = false;
            txtTelephone.IsEnabled = false;
        }
        private void activForm() // Fonction pour activer tous nos controles
        {
            txtNom.IsEnabled = true;
            txtAdresse.IsEnabled = true;
            txtVille.IsEnabled = true;
            txtProvince.IsEnabled = true;
            txtCodePostal.IsEnabled = true;
            txtCourriel.IsEnabled = true;
            txtTelephone.IsEnabled = true;
        }
        private void binding() // Fonction pour lier nod données à nos controles
        {
            ComboClients.DataContext = db.Clients.ToList().OrderBy(c => c.Nom);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e) // Clic sur bouton quitter
        {
            this.Close();
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e) // Bouton flèche de retour
        {
            
            Acceuil fen = new Acceuil(member, null);
            fen.Show();

            this.Close();

        }

        private void ComboClients_SelectionChanged(object sender, SelectionChangedEventArgs e) // La selection d'un client dans le ComboBox
        {
            // Une location a été selectionné dans la liste
            if (ComboClients.SelectedIndex == -1) return;

            activForm(); // activer notre formulaire

            update = false; // Modifications impossible

            //Obtenir notre objet selectionné et mettre les valeurs de ses propriétés dans nos controles
            Clients selection = (Clients)ComboClients.SelectedItem;

            lblID.Content = selection.IDClient.ToString();
            txtNom.Text = selection.Nom.ToString();
            txtAdresse.Text = selection.Adresse.ToString();
            txtVille.Text = selection.Ville.ToString();
            txtProvince.Text = selection.Province.ToString();
            txtCodePostal.Text = selection.CodePostal.ToString();
            txtCourriel.Text = selection.Courriel.ToString();
            txtTelephone.Text = selection.Telephone.ToString();

            if (selection.Locations.Count() != 0)  // Si le client a des locations : les afficher, sinon l'inverse
            {
                lblLocations.Visibility = Visibility.Visible;
                ListLocations.Visibility = Visibility.Visible;
                ListLocations.DataContext = selection.Locations.ToList();
                ListLocations.SelectedIndex = -1;
            }
            else
            {
                lblLocations.Visibility = Visibility.Hidden;
                ListLocations.Visibility = Visibility.Hidden;
            }

            
            update = true; //Modification possible
        }

        private void btnNouvAjout_Click(object sender, RoutedEventArgs e) // Click sur bouton Nouveau ou ajouter
        {
            if ((String)btnNouvAjout.Content == "Nouveau") // Si le bouton Nouveau est cliqué
            {
                reset();
                activForm(); // activer notre formulaire
                btnNouvAjout.Content = "Ajouter"; // Changer le texte du bouton
                ComboClients.IsEnabled = false; // Désactiver certains controles 
                btnModifier.IsEnabled = false;
                btnAnnuler.IsEnabled = true;
                nouv = true;
                return; // Quitter la fonction
            }
            // Si le bouton Ajouter est cliqué

            bool ok = verifierSaisie(); // Verifier que tous les champs on une valeur

            if (ok) // Si les champs obligatoire ont été renseigné
            {
                try //  Creer notre objet client et insérer le nouveau enregistrement dans notre objet DataBase
                {
                    Clients nouv = new Clients();

                    nouv.Nom = txtNom.Text;
                    nouv.Adresse = txtAdresse.Text;
                    nouv.Ville = txtVille.Text;
                    nouv.Province = txtProvince.Text;
                    nouv.CodePostal = txtCodePostal.Text;
                    nouv.Courriel = txtCourriel.Text;
                    nouv.Telephone = txtTelephone.Text;


                    db.Clients.Add(nouv); // Ajouter l'enregistrement et enregistrer les changements
                    db.SaveChanges();

                    MessageBox.Show("Le client " + nouv.Nom + " a été ajouté.", "Ajout", MessageBoxButton.OK, MessageBoxImage.Information);
                    binding(); // Mise a jour des liaisons
                    reset(); // Rénitialiser la fenetre
                    desactForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                // Si il manque des informations
                MessageBox.Show("Veuillez renseingner tous les champs.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)   // Click sur bouton annuler
        {
            if (ComboClients.SelectedIndex != -1 && update) // si l'utilisateur veut annuler les modifications à un enregistrement existant
            {
                update = false; // Modifications impossible

                //Obtenir notre objet selectionné et mettre les valeurs de ses propriétés dans nos controles
                Clients selection = (Clients)ComboClients.SelectedItem;

                lblID.Content = selection.IDClient.ToString();
                txtNom.Text = selection.Nom.ToString();
                txtAdresse.Text = selection.Adresse.ToString();
                txtVille.Text = selection.Ville.ToString();
                txtProvince.Text = selection.Province.ToString();
                txtCodePostal.Text = selection.CodePostal.ToString();
                txtCourriel.Text = selection.Courriel.ToString();
                txtTelephone.Text = selection.Telephone.ToString();

                if (selection.Locations.Count() != 0)  // Si le client a des locations : les afficher
                {
                    lblLocations.Visibility = Visibility.Visible;
                    ListLocations.Visibility = Visibility.Visible;
                    ListLocations.DataContext = selection.Locations.ToList();
                }

                update = true; //Modification possible
            }
            else if (nouv)  // si l'utilisateur veut annuler l'ajout d'un nouveau enregistrement
            {
                reset();
                desactForm();
            }

        }

        private void btnModifier_Click(object sender, RoutedEventArgs e) //Click sur bouton modifier
        {
           Clients selection = (Clients)ComboClients.SelectedItem; // Récupérer le client selectionné

            bool ok = verifierSaisie(); // Verifier que tous les champs on une valeur

            if (ok) // Si les champs ont été renseigné
            {
                try //  Modifier les propriétés de notre objet
                {
                    selection.Nom = txtNom.Text;
                    selection.Adresse = txtAdresse.Text;
                    selection.Ville = txtVille.Text;
                    selection.Province = txtProvince.Text;
                    selection.CodePostal = txtCodePostal.Text;
                    selection.Courriel = txtCourriel.Text;
                    selection.Telephone = txtTelephone.Text;

                    db.SaveChanges(); // Enregistrer les changements
                    MessageBox.Show("Les informations de " + selection.Nom + " ont été modifié.", "Mise-à-jour", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Si il manque des informations
                MessageBox.Show("Veuillez renseingner tous les champs.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool verifierSaisie() // Fonction pour verifier que nos textbox ne sont pas vide
        { bool ok = false;
            if (txtNom.Text.Trim() != string.Empty &&
            txtAdresse.Text.Trim() != string.Empty &&
            txtVille.Text.Trim() != string.Empty &&
            txtProvince.Text.Trim() != string.Empty &&
            txtCodePostal.Text.Trim() != string.Empty &&
            txtCourriel.Text.Trim() != string.Empty &&
            txtTelephone.Text.Trim() != string.Empty) 
            { ok = true;  }
            return ok;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ComboClients.SelectedIndex != -1 && update) // Si un client est selectionné  et les modifications sont permises
            {
                btnModifier.IsEnabled = true; // on active nos boutons modifier et annuler, sinon l'inverse
                btnAnnuler.IsEnabled = true;
            }
            else
            {
                btnModifier.IsEnabled = false;
                btnAnnuler.IsEnabled = false;
            }

            if (nouv || update  ) // Si l'utilisateur veut annuler un ajout
            { btnAnnuler.IsEnabled = true; } // on active notre bouton annuler, sinon l'inverse
            else
            { btnAnnuler.IsEnabled = false; }
        }

        private void ListLocations_SelectionChanged(object sender, SelectionChangedEventArgs e) // Clic sur une location dans le listbox
        {
            if (update == true && this.IsLoaded == true)
            {
               Locations selection = (Locations)ListLocations.SelectedItem; // Selectionner la location

               Acceuil fen = new Acceuil(member, selection); // Afficher notre fenetre
               fen.Show();


                this.Close();

            }
            
        }
    }
}
