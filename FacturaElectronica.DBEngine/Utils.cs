using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Xml;
using System.Configuration;

namespace FacturaElectronica.DBEngine
{
    public partial class SQLEngine
    {
        private const string KEYCONNECTIONSTRING = "FacturaElectronicaConnectionString";

        /// <summary>
        /// GetRegistryKey(string RegPath, string regKey)
        /// Este metodo se mantiene por compatibilidad        
        /// </summary>
        /// <param name="RegPath"></param>
        /// <param name="regKey"></param>
        /// <returns></returns>
        /*
        static private string GetRegistryKey(string RegPath, string regKey)
        {
            return FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();            
                        
      
            string strResult = string.Empty;
            RegistryKey key1 = null;
            key1 = Registry.LocalMachine.OpenSubKey(RegPath);
            if (key1 == null)
                throw (new Exception("Could not load registry key"));
            strResult = Convert.ToString(key1.GetValue(regKey));
            return strResult;
      
        }
    */
        /// <summary>
        /// GetPath()
        /// Retorna el path donde se ubica la dll DBEngine
        /// </summary>
        /// <returns>string</returns>
        private string GetPath()
        {
            try
            {
                ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
                filemap.ExeConfigFilename = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                filemap.ExeConfigFilename = filemap.ExeConfigFilename.Replace(@"file:///", "");
                return filemap.ExeConfigFilename;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        /// <summary>
        /// GetpathConfig()
        /// Publica el path de busqueda de la dll se usa en tiempo de ejecucion
        /// </summary>
        /// <returns>string</returns>
        public static string GetpathConfig()
        {
            FacturaElectronica.DBEngine.SQLEngine sqle = new SQLEngine();
            return sqle.GetPath();
        }

        /// <summary>
        /// GetConnestionStringXMLSetting()
        /// Busca en el archivo app.config el connectionstring de la base
        /// </summary>
        /// <returns>string</returns>
        public static string GetConnestionStringXMLSetting()
        {
            try
            {
                FacturaElectronica.DBEngine.SQLEngine sqle = new SQLEngine();

                System.Configuration.Configuration config =
                    System.Configuration.ConfigurationManager.OpenExeConfiguration(sqle.GetPath());

                System.Configuration.KeyValueConfigurationElement keyvalue =
                    config.AppSettings.Settings[KEYCONNECTIONSTRING];

                if (keyvalue.Value == null)
                {
                    throw (new Exception("Error: Archivo de Configuration es incorrecto o no existe (FacturaElectronica.DBEngine.dll.config)")); 
                }

                return keyvalue.Value;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }
        
    }
}

