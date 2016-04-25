﻿using InterfaceBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AllianzMaintenanceHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CadastroPessoas : Window
    {
        public CadastroPessoas()
        {  
            InitializeComponent();
            radPF.IsChecked = true;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             null/*TODO: ATUALIZAR COM A FUNÇÃO DE CARREGAMENTO*/,
                                             txtCodigoCarregar.Text);
        }

        private void CheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            RadioButton lRadioButton = sender as RadioButton;
            if (lRadioButton.Name == "radPF")
                UncheckPJ();
            else
                UncheckPF();

            lInterfaceManagement.ShowByAttribute(lRadioButton.GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),this);
        }

        private void UncheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.HideByAttribute(((RadioButton)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);            
        }

        private void UncheckPF()
        {
            if (radPF != null)
                radPF.IsChecked = false;
        }

        private void UncheckPJ()
        {
            if (radPJ != null)
                radPJ.IsChecked = false;
        }

        private void CheckCategoria(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.ShowByAttribute(((CheckBox)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);           
        }

        private void UncheckCategoria(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.HideByAttribute(((CheckBox)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }
        
    }
}