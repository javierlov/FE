using System;
using System.Configuration;
using System.Web.UI;
/*
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
 * */

namespace FacturaElectronica
{
    public partial class GeneralErrors : BasePage
    {
        private const string DefaultSelectCommand = "SELECT * FROM [ErroresGenerales] ORDER BY [ErrorID] DESC";
        private const string DefaultProviderName = "System.Data.SqlClient";

        protected void Page_Load(object sender, EventArgs e)
        {
            SetDefaultSQLCommand();
            GridView1.PageSize = Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]); 
        }

        private void SetDefaultSQLCommand()
        {
            SqlDataSource1.ConnectionString = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();
            SqlDataSource1.SelectCommand = DefaultSelectCommand;
            SqlDataSource1.ProviderName = DefaultProviderName;
        }

        protected void ImgReload_Click(object sender, ImageClickEventArgs e)
        {
            SqlDataSource1.ConnectionString = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();
            GridView1.DataBind();
        }
    }
}
