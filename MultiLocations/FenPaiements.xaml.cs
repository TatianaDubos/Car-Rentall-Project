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
    /// Logique d'interaction pour FenPaiements.xaml
    /// </summary>
    public partial class FenPaiements : Window
    {
        public ListBox list;
        public Label lbl;
        public static int c = 0;
        public FenPaiements()
        {
            InitializeComponent();
            list = ListPaiements;
            lbl = lblCount;
        }

        public static int compteur()
        {
            c++;
            return c;
        }

        
    }
}
