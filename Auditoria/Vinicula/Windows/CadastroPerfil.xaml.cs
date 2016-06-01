using RegrasDeNegocios.DatabaseBase.Classes;
using RegrasDeNegocios.DatabaseBase.Model;
using InterfaceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Vinicula
{
    /// <summary>
    /// Interaction logic for CadastroOrcamento.xaml
    /// </summary>
    public partial class CadastroPerfil : Window
    {
        public CadastroPerfil()
        {
            InitializeComponent();
            gItens.Visibility = Visibility.Hidden;
                       
            pxtTela.SelectedValuePath = "Tag";

            Logs.Log("CadastroPerfil", "Abrir");
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             LoadPerfil,
                                             txtCodigoCarregar.Text);
        }

        private void LoadPerfil(string pCodigo)
        {
            Clear();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Perfil lPerfil = new Perfil();
            PerfilDM lPerfilDM = lPerfil.SelectCodigo(pCodigo);

            if (lInterfaceManagement.CarregarDM(this, lPerfilDM))
            {
                gItens.Visibility = Visibility.Visible;
                LoadSub(lPerfilDM.perCodigo.ToString());
            }

            txtCodigoCarregar.Text = null;

            Logs.Log("CadastroPerfil", "Carregar perfil, código: " + pCodigo);
        }

        private void LoadSub(string pCodigo)
        {
            ClearSub();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            PerfilxTela lPerfilxTela = new PerfilxTela();
            List<PerfilxTelaDM> lPerfilxTelaDMList = lPerfilxTela.SelectPorPerfil(pCodigo);
            dtItens.ItemsSource = null;
            dtItens.ItemsSource = lPerfilxTelaDMList;
            dtItens.AutoGenerateColumns = false;
            dtItens.AutoGenerateColumns = true;

            int lCount = 0;
            FormatedName lAtributo;
            List<int> lRemover = new List<int>();
            foreach (PropertyInfo lProperty in typeof(PerfilxTelaDM).GetProperties())
            {
                lAtributo = lProperty.GetCustomAttributes(typeof(FormatedName), false).Cast<FormatedName>().FirstOrDefault();
                if (lAtributo != null)
                {
                    dtItens.Columns[lCount].Header = lAtributo.Name;
                    dtItens.Columns[lCount].IsReadOnly = true;
                    lCount++;
                }
                else
                {
                    dtItens.Columns.RemoveAt(lCount);
                }

            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Perfil lPerfil = new Perfil();
            List<string> lErrosValidacao = new List<string>();
            PerfilDM lPerfilDM = (PerfilDM)lInterfaceManagement.BuildDM(this, typeof(PerfilDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(perCodigo.Text))
                {
                    if (lPerfil.EditarCliente(lPerfilDM))
                        MessageBox.Show("Editado com sucesso");
                    else
                        MessageBox.Show("Erro ao editar");

                    Logs.Log("CadastroPerfil", "Editar perfil, código: " + lPerfilDM.perCodigo);                    
                }
                else
                {
                    if (lPerfil.NovoCliente(lPerfilDM, LoadPerfil))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");

                    Logs.Log("CadastroPerfil", "Inserir perfil");
                }
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Perfil lPerfil = new Perfil();
            lPerfil.ExcluirCliente(perCodigo.Text);
            Logs.Log("CadastroPerfil", "Excluir perfil, código: " + perCodigo.Text);
            Clear();            
        }

        private void Clear()
        {
            perCodigo.Text = null;
            perNome.Text = null;            
            dtItens.ItemsSource = null;
            gItens.Visibility = Visibility.Hidden;
        }

        private void ClearSub()
        {
            genCodigo.Text = null;
            pxtTela.SelectedValue = null;            
        }

        private void btnSalvarSub_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            PerfilxTela lPerfilxTela = new PerfilxTela();
            List<string> lErrosValidacao = new List<string>();
            PerfilxTelaDM lPerfilxTelaDM = (PerfilxTelaDM)lInterfaceManagement.BuildDM(this, typeof(PerfilxTelaDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(genCodigo.Text))
                {
                    if (lPerfilxTela.EditarCliente(lPerfilxTelaDM))
                        MessageBox.Show("Editado com sucesso");
                    else
                        MessageBox.Show("Erro ao editar");

                    Logs.Log("CadastroPerfil", "Editar item perfil, código: " + lPerfilxTelaDM.pxtCodigo);
                }
                else
                {
                    if (lPerfilxTela.NovoCliente(lPerfilxTelaDM))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");

                    Logs.Log("CadastroPerfil", "Inserir item de perfil");
                }

                LoadSub(perCodigo.Text);
            }
        }        


        private void dtItens_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem != null)
            {
                PerfilxTela lPerfilxTela = new PerfilxTela();
                string lCodigo = ((sender as DataGrid).SelectedItem as PerfilxTelaDM).pxtCodigo.ToString();
                if (lPerfilxTela.ExcluirCliente(lCodigo))
                {
                    LoadSub(perCodigo.Text);
                    Logs.Log("CadastroPerfil", "Excluir item de perfil, código: " + lCodigo);
                }
            }
        }
    }
}
