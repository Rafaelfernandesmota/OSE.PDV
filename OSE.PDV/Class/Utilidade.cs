#region Using
using System;
using System.Xml;
using System.IO;
using System.Linq;
#endregion
//-----------------------------------------------------------------------
// <copyright file="Utilidade.cs" company="OSE Solution Inc.">
//     Copyright (c) OSE Solution Inc.  All rights reserved.
// </copyright>
// <summary>Contains the Utilidade class.</summary>
//-----------------------------------------------------------------------
namespace OSE.PDV.Class
{
    public static class Utilidade
    {
             #region Declare
            static readonly  XmlDocument XmlDocument = new XmlDocument();
            public static string NumeroPdv;

            #endregion

            #region Constructor
            static string CFile()
            {
                return string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Conf\\OsePdv.XML");
            }

            static bool EXiste_CFile()
            {
                return File.Exists(CFile());
            }

            public static bool CarregarXml()
            {
                if (!EXiste_CFile())
                {
                    NumeroPdv = @"000";
                    return false;
                }
                try
                {
                    XmlDocument.Load(CFile());
                    var laList = XmlDocument.SelectNodes("/OSEPDV");
                    if (laList != null)
                    {
                        foreach (var selectSingleNode in laList.Cast<XmlNode>().Select(node => node.SelectSingleNode("NUMERO")).Where(selectSingleNode => selectSingleNode != null))
                        {
                            NumeroPdv = selectSingleNode.InnerText;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    NumeroPdv = @"000";
                    return false;
                }
                return true;
            }
            #endregion

            #region Methods
        
            #endregion
        }
}

