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
    /// Interaction logic for ConsultaServico.xaml
    /// </summary>
    public partial class ConsultaServico : Window
    {
        public ConsultaServico()
        {
            InitializeComponent();
            Atualizar(true);
        }

        private void Atualizar(bool pAbrindo = false)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Servico lServico = new Servico();
            Dictionary<string, string> lParametro = new Dictionary<string, string>();
            List<Object> lChildren = new List<Object>();
            lInterfaceManagement.GetControlsList(serDescricao, lChildren, pNoVisibilityCheck: pAbrindo);
            if (lChildren.Count > 0)
            {
                string[] lTipoPessoa = ((RadioButton)lChildren.First()).Name.Split('_');
                lParametro.Add(lTipoPessoa[0], lTipoPessoa[1]);

            }
            lParametro.Add(serDescricao.Name, serDescricao.Text);

            dtRegistros.ItemsSource = lServico.AtualizarGrade(lParametro);

        }
    }
}
