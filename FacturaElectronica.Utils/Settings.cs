using System;
using System.Collections;
using System.Data;
/*
using System.Text;
using Microsoft.Win32;
using System.Collections.Generic;
*/

namespace FacturaElectronica.Utils
{
    public class Settings
    {
        private Hashtable parametros = new Hashtable();

        public enum TipoDato { FS = 0, MSSQL, WS, MSMQ };

        public Settings(string EmpresaID)
        {    
           DBEngine.SQLEngine sqlEngine = new DBEngine.SQLEngine();
           parametros = sqlEngine.GetGeneralSettings(EmpresaID);
        }

        public string ConnectionString { get {             
            return FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();
        } }
        public string EmpresaID { get { return parametros["EmpresaID"].ToString(); } }
        public string ActivarDebug { get { return parametros["ActivarDebug"].ToString(); } }

        public TipoDato TipoEntrada 
        { 
            get 
            {
                TipoDato tResult = TipoDato.FS;

                switch (parametros["TipoEntrada"].ToString())
                {
                    case "FS":
                        tResult = TipoDato.FS;
                        break;

                    case "MSSQL":
                        tResult = TipoDato.MSSQL;
                        break;

                    case "WS":
                        tResult = TipoDato.WS;
                        break;

                    case "MSMQ":
                        tResult = TipoDato.MSMQ;
                        break;

                    default:
                        tResult = TipoDato.FS;
                        break;
                }

                return tResult;
            } 
        }

        public TipoDato TipoSalida
        {
            get
            {
                TipoDato tResult = TipoDato.FS;

                switch (parametros["TipoSalida"].ToString())
                {
                    case "FS":
                        tResult = TipoDato.FS;
                        break;

                    case "MSSQL":
                        tResult = TipoDato.MSSQL;
                        break;

                    case "WS":
                        tResult = TipoDato.WS;
                        break;

                    case "MSMQ":
                        tResult = TipoDato.MSMQ;
                        break;

                    default:
                        tResult = TipoDato.FS;
                        break;
                }

                return tResult;
            }
        }

        public int Intervalo { get { return Convert.ToInt16(parametros["Intervalo"]); } }
        public string Entrada { get { return parametros["Entrada"].ToString(); } }
        public string EntradaExtra { get { return parametros["EntradaExtra"].ToString(); } }
        public string Salida { get { return parametros["Salida"].ToString(); } }
        public string PathHistorico { get { return parametros["PathHistorico"].ToString(); } }
        public string PathDebug { get { return parametros["PathDebug"].ToString(); } }
        public string PathCertificate { get { return parametros["PathCertificate"].ToString(); } }
        public string PathConnectionFiles { get { return parametros["PathConnectionFiles"].ToString(); } }
        public string PathImpresion { get { return parametros["PathImpresion"].ToString(); } }
        public string PathTemporales { get { return parametros["PathTemporales"].ToString(); } }
        public string UrlAFIPwsaa { get { return parametros["UrlAFIPwsaa"].ToString(); } }
        public string UrlAFIPwsfe { get { return parametros["UrlAFIPwsfe"].ToString(); } }
        public string UrlAFIPwsfex { get { return parametros["UrlAFIPwsfex"].ToString(); } }
        public string UrlAFIPwsbfe { get { return parametros["UrlAFIPwsbfe"].ToString(); } }
        public string UrlPrintWebService { get { return parametros["UrlPrintWebService"].ToString(); } }
        public string UrlFEWebService { get { return parametros["UrlFEWebService"].ToString(); } }

        public string SMTPServer { get { return parametros["SMTPServer"].ToString(); } }
        public string SMTPUser { get { return parametros["SMTPUser"].ToString(); } }
        public string SMTPPassword { get { return parametros["SMTPPassword"].ToString(); } }
        public string SMTPFrom { get { return parametros["SMTPFrom"].ToString(); } }
        public string MailSubject { get { return parametros["MailSubject"].ToString(); } }
        public string MailMessage { get { return parametros["MailMessage"].ToString(); } }

        static public string LoggerSource { get { return "Factura Electronica"; } }
        static public string LoggerLog { get { return "Factura Electronica"; } }

        static public EmpresaCollection GetEmpresas()
        {
            DBEngine.SQLEngine sqlEngine = new DBEngine.SQLEngine();
            EmpresaCollection oEmpresaColl = new EmpresaCollection();

            DataTable dt = sqlEngine.GetItems("Empresas", "EmpresaID", "Activo = 'True'", 0);

            foreach (DataRow dr in dt.Rows)
            {
                Empresa oEmpresa = new Empresa(dr["EmpresaID"].ToString());

                oEmpresaColl.Add(oEmpresa);
            }

            return oEmpresaColl;
        }

        static public string GetRegistryKey(string RegPath, string regKey)
        {            
            return FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();
            /*
            string strResult = string.Empty;
            RegistryKey key1 = null;
            key1 = Registry.LocalMachine.OpenSubKey(RegPath);
            if (key1 == null)
                throw (new Exception("Could not load registry key"));
            strResult = Convert.ToString(key1.GetValue(regKey));
            return strResult;
             */ 
        }

        static public string GetConnectionString(string DBServer, string DBName)
        {
            string strResult = string.Empty;

            strResult = "server=" + DBServer + ";Integrated Security=true;Database=" + DBName;

            return strResult;
        }
    }
}
