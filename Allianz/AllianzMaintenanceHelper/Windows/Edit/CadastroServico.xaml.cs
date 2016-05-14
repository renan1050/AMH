﻿using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
using InterfaceBase;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;



namespace AllianzMaintenanceHelper
{
    /// <summary>
    /// Interaction logic for CadastroServico.xaml
    /// </summary>
    public partial class CadastroServico : Window
    {
        public CadastroServico()
        {
            InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             LoadServico,
                                             txtCodigoCarregar.Text);
        }

        private void LoadServico(string pCodigo)
        {
            Clear();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Servico lServico = new Servico();
            ServicoDM lServicoDM = lServico.SelectCodigo(pCodigo);
            lInterfaceManagement.CarregarDM(this, lServicoDM);
            txtCodigoCarregar.Text = null;
        }



        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Servico lServico = new Servico();
            List<string> lErrosValidacao = new List<string>();
            ServicoDM lServicoDM = (ServicoDM)lInterfaceManagement.BuildDM(this, typeof(ServicoDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(serCodigo.Text))
                {
                    if (lServico.EditarCliente(lServicoDM))
                        MessageBox.Show("Editado com sucesso");
                    else
                        MessageBox.Show("Erro ao editar");
                }
                else
                {
                    if (lServico.NovoCliente(lServicoDM, LoadServico))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");
                }
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Servico lServico = new Servico();
            lServico.ExcluirCliente(serCodigo.Text);
            Clear();
        }

        private void Clear()
        {
            serCodigo.Text = null;
            serDescricao.Text = null;
            serValor.Text = null;
        }

       
    }
}
