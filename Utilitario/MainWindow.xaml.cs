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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Utilitario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CmdGerar_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtServidor.Text == string.Empty ||
                TxtPorta.Text == string.Empty ||
                TxtUsuario.Text == string.Empty ||
                TxtPorta.Text == string.Empty ||
                TxtBanco.Text == string.Empty){ return; }
            // Monta o Text Saida
            var Out = "\n Servidor : " + HashC.Codifica(TxtServidor.Text) +
                      "\n Porta : " + HashC.Codifica(TxtPorta.Text) +
                      "\n Usuario : " + HashC.Codifica(TxtUsuario.Text) +
                      "\n Senha : " + HashC.Codifica(TxtSenha.Text) + 
                      "\n Banco : " + HashC.Codifica(TxtBanco.Text);
            TxtSaida.Text = Out;

        }
    }
}
