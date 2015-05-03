
#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using MahApps.Metro.Controls;
using MahApps.Metro;
using OSE.PDV.Class;
using Brush = System.Windows.Media.Brushes;

#endregion

//-----------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="OSE Solution Inc.">
//     Copyright (c) OSE Solution Inc.  All rights reserved.
// </copyright>
// <summary>Contains the MainWindow class.</summary>
//-----------------------------------------------------------------------
        
namespace OSE.PDV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Declare

        public static bool IsConnect { get; set; }
        public static List<string> CheckList = new List<string>();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            StartConnection();
            Debug_MySQL_Connection();
            InicializarEstacao();

            // Display All Functions Start
            Console.WriteLine();
            foreach (var list in CheckList)
            {
                Console.WriteLine(list);
            }
        }
        #region Inicia Servico MySQL Server
        void StartConnection()
        {
            if (!Conector.EXiste_CFile())
            {
                CheckList.Add(@"StartConnection -> Conector.EXiste_CFile() - Falha");
                return ;
            }
            Conector.LoadFileXml();
            IsConnect = Conector.OpenConnection();
            
            if (IsConnect)
            {
                RecMySqlOn.Visibility = Visibility.Visible;
                LblStatusMySql.Content = @"Conectado";
            }
            else
            {
                RecMySqlOff.Visibility =Visibility.Visible;
                LblStatusMySql.Content = @"Desligado";
                LblStatusMySql.Foreground = Brush.Red;
            }
            CheckList.Add(@"StartConnection - OK");
        }

        #endregion

        #region Inicializar Estacao
        private void InicializarEstacao()
        {
            // Numero Ponto de Venda
            if (!Utilidade.CarregarXml())
            {
                CheckList.Add(@"InicializarEstacao -> Falha");
                return;
            }
            LblNumCaixa.Content = Utilidade.NumeroPdv;
            
            // IP da Maquina
            var hostAddress = IPAddress.None;
            foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                hostAddress = ip;
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                    break;
            }
            LblIpdaMaquina.Content = hostAddress;

            // Data 


            // Hora

            // CheckList
            CheckList.Add(@"InicializarEstacao - OK");
        }

        #endregion

        #region Debug_MySQL_Connection
        // - Este Metodo Degub a Conexao MySQL
        static void Debug_MySQL_Connection()
        {
            if (!Conector.EXiste_CFile()) { Console.WriteLine(@"Falha Load Arquivo [Conf\MySQL.XML]"); return; }
            Console.WriteLine(@" - - - - - - - - Debug_MySQL_Connection  - - - - - - - - - -" +
                              "\n Servidor: " + Conector.MServidor +
                              "\n Porta: " + Conector.MPorta +
                              "\n Usuario: " + Conector.MUsuario +
                              "\n Senha: " + Conector.MSenha +
                              "\n Banco: " + Conector.MBanco);
            Console.WriteLine();
            
            Console.WriteLine(Conector.OpenConnection()
                ? @"Sucesso Conexão MySQL Server"
                : @"Falha Conexão MySQL Server");
            Console.WriteLine(@" - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");

            
        }
        #endregion

    }
}