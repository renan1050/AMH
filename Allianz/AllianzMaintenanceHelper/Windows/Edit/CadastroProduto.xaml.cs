﻿using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
using InterfaceBase;
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

namespace AllianzMaintenanceHelper
{
    /// <summary>
    /// Interaction logic for CadastroProduto.xaml
    /// </summary>
    /// 

    public partial class CadastroProduto : Window
    {
        public CadastroProduto()
        {
            InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             null,
                                             txtCodigoCarregar.Text);
        }


    }
}
