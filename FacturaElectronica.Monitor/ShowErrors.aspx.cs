using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace FacturaElectronica
{
    public partial class showerrors : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCbteId = Request.QueryString["nc"];

            DataTable dT = GetLogErrorData(strCbteId);

            lblPageTitle.Text = "Registro de Errores";

            gvErrors.DataSource = dT;
            gvErrors.DataBind();
        }
    }
}
