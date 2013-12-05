using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace Accendo.ComprobanteElectronico
{
    public class XMLConnector
    {
        static private XmlDocument xmlSettings = new XmlDocument();

        static string sPath = string.Empty;

        static XMLConnector()
        {
            sPath = HttpContext.Current.Server.MapPath("App_Data");
        }

        static public string GetConfigurationField(string strField)
        {
            string strReturn = string.Empty;

            try
            {
                xmlSettings.Load(sPath + "\\settings.acc");

                if (xmlSettings.SelectSingleNode("settings/" + strField) != null)
                {
                    strReturn = xmlSettings.SelectSingleNode("settings/" + strField).InnerText;
                }
                else
                {
                    strReturn = string.Empty;
                }
            }
            catch (Exception ex)
            {
            }
            return strReturn;
        }
    }
}