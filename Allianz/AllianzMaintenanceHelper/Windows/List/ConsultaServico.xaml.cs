﻿using BusinessRules.DatabaseBase.Classes;
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

namespace AllianzMaintenanceHelper.Windows.List
{
    /// <summary>
    /// Interaction logic for ConsultaServico.xaml
    /// </summary>
    public partial class ConsultaServico : Window
    {
        public ConsultaServico()
        {
            InitializeComponent();
            Atualizar();
            
        }

        private void Atualizar()
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Servico lServico = new Servico();
            Dictionary<string, string> lParametro = new Dictionary<string, string>();
                      
            lParametro.Add(serDescricao.Name, serDescricao.Text);

            dtRegistros.ItemsSource = lServico.AtualizarGrade(lParametro);

        }

        private void serDescricao_LostFocus(object sender, RoutedEventArgs e)
        {
            Atualizar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CadastroServico lCadastroServico = new CadastroServico();
            lCadastroServico.Show();
        }
    }
}
