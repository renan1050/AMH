using BusinessRules.DatabaseBase.Classes;
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
using BusinessRules.DatabaseBase.Model;

namespace Vinicula.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(usuNome.Text) && !string.IsNullOrEmpty(usuSenha.Password))
            {
                Dictionary<string, string> lParametros = new Dictionary<string,string>();
                lParametros.Add(usuNome.Name, usuNome.Text);
                lParametros.Add(usuSenha.Name, usuSenha.Password);
                
                List<UsuarioDM> lUsuarioDMList = Database.SelecionarTudo("usuario",lParametros, typeof(UsuarioDM)).Cast<UsuarioDM>().ToList();
                if(lUsuarioDMList.Count > 0)
                {                    
                    MessageBox.Show("Logado");
                    MainWindow lMainWindow = new MainWindow();
                    lMainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário não encontrado");
                }
            }
            else
            {
                MessageBox.Show("Usuário e senha são obrigatórios");
            }
        }
    }
}
