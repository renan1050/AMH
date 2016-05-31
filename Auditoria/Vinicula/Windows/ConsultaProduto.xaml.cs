using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
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
    /// Interaction logic for ConsultaProduto.xaml
    /// </summary>
    public partial class ConsultaProduto : Window
    {
        public ConsultaProduto()
        {
            InitializeComponent();
            Atualizar(true);
        }

        private void Atualizar(bool pAbrindo = false)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                Produto lProduto = new Produto();
                Dictionary<string, string> lParametro = new Dictionary<string, string>();

                lParametro.Add(proNome.Name, proNome.Text);
                dtRegistros.ItemsSource = null;
                dtRegistros.ItemsSource = lProduto.AtualizarGrade(lParametro);
                dtRegistros.AutoGenerateColumns = false;
                dtRegistros.AutoGenerateColumns = true;

                if (!pAbrindo)
                {
                    int lCount = 0;
                    FormatedName lAtributo;
                    List<int> lRemover = new List<int>();
                    foreach (PropertyInfo lProperty in typeof(ProdutoDM).GetProperties())
                    {
                        lAtributo = lProperty.GetCustomAttributes(typeof(FormatedName), false).Cast<FormatedName>().FirstOrDefault();
                        if (lAtributo != null)
                        {
                            dtRegistros.Columns[lCount].Header = lAtributo.Name;
                            dtRegistros.Columns[lCount].IsReadOnly = true;
                            lCount++;
                        }
                        else
                        {
                            dtRegistros.Columns.RemoveAt(lCount);
                        }

                    }
                }
                dtRegistros.CanUserAddRows = false;
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CadastroProduto lCadastroProduto = new CadastroProduto();
            lCadastroProduto.Show();
        }

        private void proNome_LostFocus(object sender, RoutedEventArgs e)
        {
            Atualizar();
        }
    }
}
