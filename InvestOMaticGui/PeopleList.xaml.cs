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

namespace InvestOMaticGui
{
    /// <summary>
    /// Interaction logic for PeopleList.xaml
    /// </summary>
    public partial class PeopleList : Window
    {
        public PeopleList()
        {
            InitializeComponent();
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "This is the InvestOMatic dummy app. Don't use this to actually invest anything!",
                "About InvestOMatic",
                MessageBoxButton.OK);
        }
    }
}
