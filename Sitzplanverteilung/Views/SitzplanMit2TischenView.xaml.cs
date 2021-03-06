﻿using Sitzplanverteilung.ViewModels;
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
    /// Interaktionslogik für SitzplanMit2TischenView.xaml
    /// </summary>
    public partial class SitzplanMit2TischenView : UserControl
    {
        public SitzplanMit2TischenView()
        {
            InitializeComponent();
            loadDataForTemplates();
            DataContext = new SitzplanMit1TischModel();
        }

        private void loadDataForTemplates()
        {
            Sitzplan sitzplanFuerBlock = (Sitzplan)App.Current.Properties["Block"];
            List<Tischgruppe> aktiveTischegruppen = sitzplanFuerBlock.getTischgruppen();
            App.Current.Properties["tischNummer"] = 0;
            App.Current.Properties["Tischgruppe"] = aktiveTischegruppen;
        }
    }
}
