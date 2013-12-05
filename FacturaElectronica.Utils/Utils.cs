using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Xml;

namespace FacturaElectronica.Utils
{
    public class Utils
    {
        static public bool FileIsInUse(FileInfo fi)
        {
            bool fileIsInUse = true;
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fi.FullName);
                fileIsInUse = false;
                fs.Close();
            }
            catch (IOException)
            {
                fileIsInUse = true;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return fileIsInUse;
        }
        
        static public double DoubleFromString(string stringNumber)
        {
            double result = 0;

            try
            {
                if (stringNumber != string.Empty && stringNumber != null)
                {
                    //TODO: VERIFICAR LA CONFIGURACION DEL HOST PARA NO HACER ESTO
                    //Se asume que el punto decimal es "." y NO es ","
                    int posicionComa = stringNumber.IndexOf(".");
                    if (posicionComa > 0)
                    {
                        string strParteEntera = stringNumber.Substring(0, posicionComa);
                        string strParteDecimal = stringNumber.Substring(posicionComa + 1, stringNumber.Length - posicionComa - 1);
                        double parteEntera = Convert.ToDouble(strParteEntera);
                        double parteDecimal = Convert.ToDouble(strParteDecimal) / (float)Math.Pow(10, strParteDecimal.Length);
                        result = parteEntera + parteDecimal;
                    }
                    else
                    {
                        result = Convert.ToDouble(stringNumber);
                    }
                }
            }
            catch 
            {
                throw new Exception("No fue posible convertir " + stringNumber + " a número.");
            }
            return result;

        }

        static public string GetCurrentNTUser()
        {
            string strResult = string.Empty;

            try
            {
                strResult = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch 
            {
                strResult = string.Empty;
            }
            return strResult;
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

       
        static public bool SetRegistryKey(string RegPath, string regKey, string regValue)
        {
            try
            {
                string strResult = string.Empty;

                RegistryKey key1 = null;

                key1 = Registry.LocalMachine.CreateSubKey(RegPath);

                if (key1 == null)
                    throw (new Exception("Could not load registry key"));

                key1.SetValue(regKey, regValue);

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        static public string FillWithCeros(string StringNumber, int iCantCeros)
        {
            string AuxString = StringNumber;

            if (StringNumber.Length < iCantCeros)
            {
                for (int i = 0; i < (iCantCeros - StringNumber.Length); i++)
                {
                    AuxString = "0" + AuxString;
                }
            }

            return AuxString;
        }

        static public string SerializeObject(object obj)
        {
            XmlDocument doc = new XmlDocument();
            MemoryStream stream = new MemoryStream();

            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());

                serializer.Serialize(stream, obj);
                stream.Position = 0;

                doc.Load(stream);

                return doc.InnerXml;
            }
            catch
            {
                return "<error>Objeto no se pudo serializar</error>";
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
        }

        static public void DebugLine(string xmlTextLine, string DebugFullPath)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(xmlTextLine);

                doc.Save(DebugFullPath);
            }
            catch(Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Debug Line", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }
    }
}
