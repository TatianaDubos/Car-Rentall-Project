using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour Acceuil.xaml
    /// </summary>
    public partial class Acceuil : Window
    {
        DataBase db = new DataBase();
        Utilisateur member;
        Locations location;
        bool update = false;
        bool nouv = false;
        
    
        public Acceuil(Utilisateur actif, Locations selection)
        {
            InitializeComponent();
            location = selection; // Si une location doit etre selectionné
            member = actif; // utilisateur actif

            Title += "/Connecté en tant que " + member.NomUtilisateur + " - " + member.NomPoste ; // Modifier le titre de la fenetre
            binding(); // Faire la liaison entre nos controles et nos données
            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            reset(); // Mettre nos controles dans leur état initial
            desactForm(); // desactiver notre formulaire

            if(location != null) //Si l'utilisateur désire afficher les informations d'une location en particulier
            { 
                ComboLocations.SelectedValue = location.IDLocation; // Selectionner la location
            }
        }

        private void desactForm()  // Desactiver notre formulaire
        {
            DateDebut.IsEnabled = false;
            DateFin.IsEnabled = false; 
            txtMontant.IsEnabled = false;
            ComboNombre.IsEnabled = false;
            txtKmDebut.IsEnabled = false;
            txtKmFin.IsEnabled = false;
            txtSurcharge.IsEnabled = false;
            ComboNiv.IsEnabled = false;
            ComboClient.IsEnabled = false;
            ComboTermes.IsEnabled = false;

        }

        private void activForm()  // Activer notre formulaire
        {
            DateDebut.IsEnabled = true;
            DateFin.IsEnabled = true ;
            txtMontant.IsEnabled = true;
            ComboNombre.IsEnabled = true;
            txtKmDebut.IsEnabled = true;
            txtKmFin.IsEnabled = true;
            txtSurcharge.IsEnabled = true;
            ComboNiv.IsEnabled = true;
            ComboClient.IsEnabled = true;
            ComboTermes.IsEnabled = true;
        }

        private void reset() 
        {
            // Fonction pour rénitialiser tous nos controles
            update = false;
            nouv = false;
            ComboNombre.SelectedIndex = -1;
            ComboLocations.IsEnabled = true;
            lblID.Content = string.Empty;
            DateDebut.SelectedDate = null;
            DateFin.SelectedDate = null;
            txtMontant.Text = string.Empty;
            txtKmDebut.Text = string.Empty;
            txtKmFin.Text = string.Empty;
            txtSurcharge.Text = string.Empty;
            lblAnnee.Content = string.Empty;
            lblKmMax.Content = string.Empty;
            lblSurprime.Content = string.Empty;
            btnModifier.IsEnabled = false;
            btnPaiements.Visibility = Visibility.Hidden;
            btnAnnuler.IsEnabled = false;
            btnNouvAjout.Content = "Nouveau";
            ComboNiv.SelectedIndex = -1;
            ComboClient.SelectedIndex = -1;
            ComboTermes.SelectedIndex = -1;
            ComboLocations.SelectedIndex = -1;
        }

        private void binding()
        {
            // Fonction pour faire la liaison entre nos controles et nos données
            ComboLocations.DataContext = db.Locations.ToList().OrderBy(l => l.IDLocation);
            ComboNiv.DataContext = db.Vehicules.ToList();
            ComboClient.DataContext = db.Clients.ToList();
            ComboTermes.DataContext = db.Termes_Location.ToList().OrderBy(t => t.IDTerme);
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        { 
            this.Close(); // Bouton Quitter

        }

        private void btnNouvAjout_Click(object sender, RoutedEventArgs e)
        {
            if((String)btnNouvAjout.Content == "Nouveau") // Si le bouton Nouveau est cliqué
            {
                reset();
                activForm(); // activer notre formulaire


                btnNouvAjout.Content = "Ajouter"; // Changer le texte du bouton
                ComboLocations.IsEnabled = false; // Désactiver certains controles 
                btnModifier.IsEnabled = false; 
                btnAnnuler.IsEnabled = true;
                txtKmFin.IsEnabled = false;
                txtSurcharge.IsEnabled = false;
                nouv = true;
                return; // Quitter la fonction
            }
            // Si le bouton Ajouter est cliqué
            bool ok = verifierSaisie(); // Verifier que tous les champs on une valeur

            if (ok) // Si les champs obligatoire ont été renseigné
            {
                try //  Creer notre objet Location et insérer le nouveau enregistrement dans notre objet DataBase
                { 
                    Locations nouv = new Locations();

                  
                  nouv.DateDebut = (DateTime)DateDebut.SelectedDate; 
                  nouv.DateFin = (DateTime)DateFin.SelectedDate;
                  nouv.MontantPaie = decimal.Parse(txtMontant.Text);
                  nouv.NombrePaie = byte.Parse(ComboNombre.SelectedValue.ToString());
                  nouv.NIV = ComboNiv.SelectedValue.ToString() ;
                  nouv.IDClient = (int) ComboClient.SelectedValue;
                  nouv.IDTerme = (int) ComboTermes.SelectedValue;
                  nouv.KmDebut = int.Parse(txtKmDebut.Text);

                   db.Locations.Add(nouv); // Ajouter l'enregistrements et enregistrer les changements
                   db.SaveChanges();

                    MessageBox.Show( "La location #" + nouv.IDLocation + " a été ajouté.", "Ajout", MessageBoxButton.OK, MessageBoxImage.Information);
                    binding(); // Metre a jour nos liaisons
                    reset(); // Rénitialiser la fenetre
                    desactForm();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else 
            {
                // Si il manque des informations
                MessageBox.Show("Veuillez renseingner tous les champs.", "Attention !" , MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private bool verifierSaisie() 
        {
            // Verifier si tout les champs ont recu une valeur
            bool ok = false;
            if (DateDebut.SelectedDate != null &&
            DateFin.SelectedDate != null &&
            txtMontant.Text != string.Empty &&
            ComboNombre.SelectedIndex != -1 &&
            txtKmDebut.Text != string.Empty &&
            ComboNiv.SelectedIndex != -1 &&
            ComboClient.SelectedIndex != -1 &&
            ComboTermes.SelectedIndex != -1 ) 
            { ok = true; }

            return ok;
        }

        private void ComboTermes_SelectionChanged(object sender, SelectionChangedEventArgs e) // Pour afficher des informations
                                                                                              // supplémentaires sur le terme de location choisi
        {
            if (ComboTermes.SelectedIndex == -1) return;


            Termes_Location selection = (Termes_Location)ComboTermes.SelectedItem;  // Récupérer l'objet selectionné
            
            // Afficher les informations
            lblAnnee.Content = "Nombre d'années - " + selection.NbrAnnees + " an(s)";
            lblKmMax.Content = "Kilométrage maximum permis - " + selection.KmMax + " km";
            lblSurprime.Content = "Surprime pour le kilométrage exédentaire - " + String.Format("{0:0.00}", selection.Surprime) + "$";

            Combo_SelectionChanged(sender, e); // Appeler notre événement qui s'occupe d'activer le bouton modifier
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e) // Click sur le bouton annuler
        {

            if (ComboLocations.SelectedIndex != -1 && update) // si l'utilisateur veut annuler les modifications à un enregistrement existant
            {
                update = false; // Modifications impossible

                //Obtenir notre objet selectionné et mettre les valeurs de ses propriétés dans nos controles
                Locations selection = (Locations)ComboLocations.SelectedItem;

                lblID.Content = selection.IDLocation.ToString();
                DateDebut.SelectedDate = selection.DateDebut;
                DateFin.SelectedDate = selection.DateFin;
                txtMontant.Text = String.Format("{0:0.00}", selection.MontantPaie) + " $";
                ComboNombre.SelectedValue = selection.NombrePaie;
                ComboNiv.SelectedValue = selection.NIV;
                ComboClient.SelectedValue = selection.IDClient;
                ComboTermes.SelectedValue = selection.IDTerme;
                txtKmDebut.Text = selection.KmDebut.ToString() + " km";
                if (selection.KmFin.ToString() != string.Empty && selection.KmFin != null) // Si la valeur de KmFin n'est pas null
                {
                    txtKmFin.Text = selection.KmFin.ToString() + " km";
                }
                else
                {
                    txtKmFin.Text = string.Empty;
                }
                if (selection.PaieSurcharge != 0 && selection.PaieSurcharge != null) // Si la valeur de PaieSurcharge n'est pas null
                {
                    txtSurcharge.Text = String.Format("{0:0.00}", selection.PaieSurcharge) + " $";
                }
                else
                {
                    txtSurcharge.Text = string.Empty;
                }

                update = true; //Modification possible
            }
            else if(nouv)  // si l'utilisateur veut annuler l'ajout d'un nouveau enregistrement
            {
                reset();
                desactForm();
            }
            
        }

        private void ComboLocations_SelectionChanged(object sender, SelectionChangedEventArgs e) //Combobox liste des locations
        {
            // Une location a été selectionné dans la liste
            if (ComboLocations.SelectedIndex == -1) return;

            activForm(); // activer notre formulaire

            update = false; // Modifications impossible

            //Obtenir notre objet selectionné et mettre les valeurs de ses propriétés dans nos controles
            Locations selection = (Locations)ComboLocations.SelectedItem;

            lblID.Content = selection.IDLocation.ToString();
            DateDebut.SelectedDate = selection.DateDebut ;
            DateFin.SelectedDate =  selection.DateFin;
            txtMontant.Text = String.Format("{0:0.00}", selection.MontantPaie) + " $";
            ComboNombre.SelectedValue = selection.NombrePaie;
            ComboNiv.SelectedValue = selection.NIV;
            ComboClient.SelectedValue = selection.IDClient ;
            ComboTermes.SelectedValue = selection.IDTerme;
            txtKmDebut.Text = selection.KmDebut.ToString() + " km";
            btnPaiements.Visibility = Visibility.Visible;
            if(selection.KmFin.ToString() != string.Empty && selection.KmFin != null) // Si la valeur de KmFin n'est pas null
            { 
                txtKmFin.Text = selection.KmFin.ToString() + " km";
            }
            else
            {
                txtKmFin.Text = string.Empty;
            }
            if(selection.PaieSurcharge != 0 && selection.PaieSurcharge != null) // Si la valeur de PaieSurcharge n'est pas null
            {
                txtSurcharge.Text = String.Format("{0:0.00}", selection.PaieSurcharge) + " $";
            }
            else
            {
                txtSurcharge.Text = string.Empty;
            }

            update = true; //Modification possible

        }



        // Fonction pour activer notre bouton Modifier et Annuler suite a un changement
        private void btnIsEnabled()
        {
            if (ComboLocations.SelectedIndex != -1 && update) // Si une Location est selectionné  et les modifications sont permises
            {
                btnModifier.IsEnabled = true; // on active nos boutons modifier et annuler, sinon l'inverse
                btnAnnuler.IsEnabled = true;
            }
            else
            {
                btnModifier.IsEnabled = false;
                btnAnnuler.IsEnabled = false;
            }

            if (nouv || update)  // Si l'utilisateur veut annuler un ajout
            { btnAnnuler.IsEnabled = true; }  // on active notre bouton annuler, sinon l'inverse
            else
            { btnAnnuler.IsEnabled = false; }
        }
        //Pour activer notre bouton Modifier et Annuler si un changement est fait a un enregistrement
        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnIsEnabled();
        }

        //Pour activer notre bouton Modifier et Annuler si un changement est fait a un enregistrement
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnIsEnabled();
        }

        //Pour activer notre bouton Modifier et Annuler si un changement est fait a un enregistrement
        private void Date_KeyDown(object sender, KeyEventArgs e)
        {
            btnIsEnabled();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e) // Click sur bouton modifier
        {
            Locations selection = (Locations)ComboLocations.SelectedItem; // Récupérer la location selectionné

            bool ok = verifierSaisie(); // Verifier que tous les champs on une valeur

            if (ok) // Si les champs obligatoire ont été renseigné
            {
                try //  Modifier les propriétés de notre objet
                {

                    selection.DateDebut = (DateTime)DateDebut.SelectedDate;
                    selection.DateFin = (DateTime)DateFin.SelectedDate;
                    selection.MontantPaie = decimal.Parse(txtMontant.Text.Replace("$", string.Empty).Trim());
                    selection.NombrePaie = byte.Parse(ComboNombre.SelectedValue.ToString());
                    selection.NIV = ComboNiv.SelectedValue.ToString();
                    selection.IDClient = (int)ComboClient.SelectedValue;
                    selection.IDTerme = (int)ComboTermes.SelectedValue;
                    selection.KmDebut = int.Parse(txtKmDebut.Text.Replace("km", string.Empty).Trim());
                    if (txtKmFin.Text.Replace("km", string.Empty).Trim() != string.Empty) // Si KmFin n'est pas null
                    {
                        selection.KmFin = int.Parse(txtKmFin.Text.Replace("km", string.Empty).Trim());
                    }
                    if (txtSurcharge.Text.Replace("$", string.Empty).Trim() != string.Empty) //Si Surcharge n'est pas null
                    {
                        selection.PaieSurcharge = decimal.Parse(txtSurcharge.Text.Replace("$", string.Empty).Trim());
                    }


                    db.SaveChanges(); // Enregistrer nos changements
                    MessageBox.Show("La location #" + selection.IDLocation + " a été modifié.", "Mise-à-jour", MessageBoxButton.OK, MessageBoxImage.Information);

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

        private void btnPaiements_Click(object sender, RoutedEventArgs e) // Boutons pour visualiser les paiements
        {
          Locations selection =  (Locations) ComboLocations.SelectedItem; // Récupérer la location selectionné

            //Créer notre fenetre 
            FenPaiements fen = new FenPaiements();

          
                FenPaiements.c = 0; // Mettre le compteur static d'enregistrement des Paiements à 0
                 fen.lbl.Content = selection.Paiements.Count() + " résultat(s)"; // Afficher le nombre d'enregistrements


                fen.ListPaiements.ItemsSource = selection.Paiements.OrderBy(p => p.Date); // Mettre les enregistrements dans le listbox
            
           
            //afficher notre fenetre en mode dialogue
            fen.ShowDialog();
            



        }

        private void btnAffClients_Click(object sender, RoutedEventArgs e) // Bouton pour afficher les clients
        {
            
            if (ComboClient.SelectedIndex == -1) // Si aucun client n'est selectionné
            {
                FenClients fen = new FenClients(member, null); // On crée notre nouvelle fenetre et on lui transmet l'utilisateur actif
                fen.Show();
            }
            else // Si un client est selectionné : afficher les informations sur ce client
            {
                Clients client = (Clients) ComboClient.SelectedItem;

                FenClients fen = new FenClients(member, client ); // On crée notre nouvelle fenetre et on lui transmet l'utilisateur actif
                fen.Show();
            }

            this.Close(); // On ferme la fenetre active
        }

    }
}
