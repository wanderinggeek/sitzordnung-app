using Sitzplanverteilung.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sitzplanverteilung.Views
{
    /// <summary>
    /// Interaktionslogik für SitzplanMit6TischenView.xaml
    /// </summary>
    public partial class SitzplanMit6TischenView : UserControl
    {
        public SitzplanMit6TischenView()
        {
            InitializeComponent();
            DataContext = new SitzplanMit1TischModel();
        }
    }
}
