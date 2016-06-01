using RegrasDeNegocios.DatabaseBase.Classes;
using RegrasDeNegocios.DatabaseBase.Model;
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

namespace Vinicula
{
    /// <summary>
    /// Interaction logic for CadastroProduto.xaml
    /// </summary>
    public partial class CadastroUsuario : Window
    {
        public CadastroUsuario()
        {
            InitializeComponent();

            Pessoa lPessoa = new Pessoa();
            List<PessoaDM> lPessoaDMList = lPessoa.SelecionarTudo();
            pesCodigo.ItemsSource = lPessoaDMList.ToDictionary(x => x.pesCodigo, x => x.pesNome);
            pesCodigo.DisplayMemberPath = "Value";
            pesCodigo.SelectedValuePath = "Key";

            Perfil lPerfil = new Perfil();
            List<PerfilDM> lPerfilDM = lPerfil.SelecionarTudo();
            pesCodigo.ItemsSource = lPerfilDM.ToDictionary(x => x.perCodigo, x => x.perNome);
            pesCodigo.DisplayMemberPath = "Value";
            pesCodigo.SelectedValuePath = "Key";

            Logs.Log("CadastroUsuario", "Abrir");
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                                 this,
                                                 LoadUsuario,
                                                 txtCodigoCarregar.Text);
                if (txtCodigoCarregar.IsVisible)
                    txtCodigoCarregar.Focus();
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void LoadUsuario(string pCodigo)
        {
            try
            {
                Clear();
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                Usuario lUsuario = new Usuario();
                UsuarioDM lUsuarioDM = lUsuario.SelectCodigo(pCodigo);
                lInterfaceManagement.CarregarDM(this, lUsuarioDM);
                txtCodigoCarregar.Text = null;

                Logs.Log("CadastroUsuario", "Carregar usuario, código: " + pCodigo);
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                Usuario lUsuario = new Usuario();
                List<string> lErrosValidacao = new List<string>();
                UsuarioDM lUsuarioDM = (UsuarioDM)lInterfaceManagement.BuildDM(this, typeof(UsuarioDM), ((Button)sender).Name, lErrosValidacao);
                if (lErrosValidacao != null && lErrosValidacao.Count > 0)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
                }
                else
                {
                    if (!string.IsNullOrEmpty(usuCodigo.Text))
                    {
                        if (lUsuario.EditarCliente(lUsuarioDM))
                            MessageBox.Show("Editado com sucesso");
                        else
                            MessageBox.Show("Erro ao editar");

                        Logs.Log("CadastroUsuario", "Editar usuario, código: " + lUsuarioDM.usuCodigo);
                    }
                    else
                    {
                        if (lUsuario.NovoCliente(lUsuarioDM, LoadUsuario))
                            MessageBox.Show("Salvo com sucesso");
                        else
                            MessageBox.Show("Erro ao salvar");

                        Logs.Log("CadastroUsuario", "Inserir usuario");
                    }
                }
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario lUsuario = new Usuario();
                lUsuario.ExcluirCliente(usuCodigo.Text);
                Logs.Log("CadastroUsuario", "Excluir usuario, código: " + usuCodigo.Text);
                Clear();
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void Clear()
        {
            usuCodigo.Text = null;
            usuNome.Text = null;
            usuSenha.Password = null;
            pesCodigo.SelectedValue = null;
            perCodigo.SelectedValue = null;
        }
    }
}
