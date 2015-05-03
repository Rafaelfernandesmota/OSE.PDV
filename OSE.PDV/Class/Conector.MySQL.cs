using System;
using System.Configuration;
using System.IO;
using System.Windows.Navigation;
using System.Xml;
using MySql.Data.MySqlClient;
namespace OSE.PDV.Class
{
    //-----------------------------------------------------------------------
    // <copyright file="Conector.cs" company="OSE Solution Inc.">
    //     Copyright (c) OSE Solution Inc.  All rights reserved.
    // </copyright> - Data 02/01/2015
    // <summary>Contains the Conector class.</summary>
    //-----------------------------------------------------------------------
    public static class Conector
    {
        /// <summary>
        /// Conector.MySQL.cs
        /// Conector - Provedor de Conexao MySQL
        /// </summary>
        #region Declare
        public static string MServidor { get; set; }
        public static string MUsuario { get; set; }
        public static string MSenha { get; set; }
        public static string MBanco { get; set; }
        public static string MPorta { get; set; }

        public static MySqlConnection Connection;
        public static XmlDocument XmlDouDocument;
        #endregion
        
        #region Constructor
        static string CFile()
        {
            return string.Concat(AppDomain.CurrentDomain.BaseDirectory , "Conf\\MySQL.XML");
        }
            
        #endregion

        #region Methods
        public static bool EXiste_CFile()
        {
            return File.Exists(CFile());
        }
        public static void LoadFileXml()
        {
            if (!EXiste_CFile()) { return;}
            XmlDouDocument = new XmlDocument();             
            XmlDouDocument.Load(CFile());
            XmlNodeList laList = XmlDouDocument.SelectNodes("/CFMYSQL");
            if (laList != null)
                foreach (XmlNode node in laList)
                {
                    MServidor = HashF.Decodifica(node.SelectSingleNode("SERVIDOR").InnerText);
                    MPorta = (HashF.Decodifica(node.SelectSingleNode("PORTA").InnerText));
                    MUsuario = HashF.Decodifica(node.SelectSingleNode("USUARIO").InnerText);
                    MSenha = HashF.Decodifica(node.SelectSingleNode("SENHA").InnerText);
                    MBanco = HashF.Decodifica(node.SelectSingleNode("BANCO").InnerText);    
                }
        }

        public static bool OpenConnection()
        {
            var connectionString = "SERVER=" + MServidor +
                                   ";DATABASE=" + MBanco +
                                   ";UID=" + MUsuario +
                                   ";PASSWORD=" + MSenha ;         
            Connection = new MySqlConnection(connectionString);
            try
            {
                Connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
             Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion
    }
}



// ------------------------------------------ MySQL.XML
//<?xml version="1.0" encoding="utf-8" ?> 
//<!--Este e Arquivo que configura a conexao MySQL Server-->
//<CFMYSQL>
//    <SERVIDOR></SERVIDOR>
//    <PORTA></PORTA>
//    <USUARIO></USUARIO>
//    <SENHA></SENHA>
//    <BANCO></BANCO>
//</CFMYSQL>
