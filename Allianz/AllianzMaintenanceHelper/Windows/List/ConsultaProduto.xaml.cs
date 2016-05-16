using BusinessRules.DatabaseBase.Classes;
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
    /// Interaction logic for ConsultaProduto.xaml
    /// </summary>
    public partial class ConsultaProduto : Window
    {
        public ConsultaProduto()
        {
            InitializeComponent();
            Atualizar();
        }

        private void Atualizar()
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Produto lProduto = new Produto();
            Dictionary<string, string> lParametro = new Dictionary<string, string>();

            lParametro.Add(proNome.Name, proNome.Text);

            dtRegistros.ItemsSource = lProduto.AtualizarGrade(lParametro);

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
