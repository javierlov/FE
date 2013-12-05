using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;

namespace FacturaElectronica.DBUtils
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://accendo.com.ar/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DBWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string DBConnection()
        {
            return ConfigurationManager.ConnectionStrings["FacturaElectronicaConnectionString"].ConnectionString; 
        }
    }
}
