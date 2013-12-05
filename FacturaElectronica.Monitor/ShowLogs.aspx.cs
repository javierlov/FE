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
    public partial class showlogs : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCbteId = Request.QueryString["nc"];
            string strCbteNroLegal = Request.QueryString["leg"];
            string strCbteTipo = Request.QueryString["nt"];
            string strCbteNroErp = Request.QueryString["erp"];

            DataTable dT = GetCbteLogData(strCbteId, strCbteNroLegal, strCbteTipo, strCbteNroErp);

            lblPageTitle.Text = "Log de procesos";

            Page.Header.Title = "FEID:" + strCbteId;

            gvErrors.DataSource = dT;
            gvErrors.DataBind();

            if (gvErrors.Rows.Count == 0)
            {

            }
        }
        protected void gvErrors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("dd/MM/yyyy");

                e.Row.Cells[3].Attributes.Add("OnClick", "window.showModelessDialog('showerrors.aspx?nc=" + e.Row.Cells[0].Text + "','viewerrordetail','dialogWidth:720px; dialogHeight:300px; center: yes; resizable: no; status: no')");
                e.Row.Cells[3].Style.Add("color", "navy");
                e.Row.Cells[3].Attributes.Add("OnMouseOver", "javascript:this.style.textDecoration='underline';");
                e.Row.Cells[3].Attributes.Add("OnMouseOut", "javascript:this.style.textDecoration='none';");
                e.Row.Cells[3].Style["cursor"] = "hand";
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }
}
