using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Accendo.ComprobanteElectronico
{
    public partial class afipabm : BasePage
    {
        private string strDBName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dT = null;

            strDBName = Request.QueryString["db"];

            if (strDBName != null && strDBName != string.Empty)
            {
                lnkCrear.PostBackUrl = "edititem.aspx?t=" + strDBName;
                lblPageTitle.Text = "Mostrando Tabla: " + strDBName;

                if (strDBName.StartsWith("AFIP") || strDBName.StartsWith("EquivAFIP"))
                {
                    string orderbyoption = "Id ASC";
                    if (strDBName.Equals("EquivAFIPImpuesto")) orderbyoption = "EmpresaID ASC";
                    dT = GetAFIPTable(strDBName, " * ", orderbyoption);
                }
                else if (strDBName.StartsWith("PuntoVenta"))
                {
                    dT = GetAFIPTable(strDBName, "PuntoVentaID, EmpresaID, Nombre, Descripcion", "PuntoVentaID ASC");
                }
                else
                {
                    dT = GetAFIPTable(strDBName, "EmpresaID, RazonSocial, TipoDocumento, NroDocumento, Activo", "EmpresaID ASC");
                }

                gvAFIPTable.DataSource = dT.DefaultView;
                gvAFIPTable.DataBind();
            }
        }

        protected void gvAFIPTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("OnClick", "window.location.href='edititem.aspx?ca=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(0).ToString() + "&t=" + strDBName + "'");

                //if (strDBName.StartsWith("EquivAFIP"))
                //{
                //    e.Row.Attributes.Add("OnClick", "window.location.href='edititem.aspx?ca=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(2).ToString() + "&t=" + strDBName + "'");
                //}
                //else if (strDBName.StartsWith("AFIP"))
                //{
                //    e.Row.Attributes.Add("OnClick", "window.location.href='edititem.aspx?ca=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(0).ToString() + "&t=" + strDBName + "'");
                //}
                //else
                //{
                //    e.Row.Attributes.Add("OnClick", "window.location.href='edititem.aspx?ca=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(1).ToString() + "&t=" + strDBName + "'");
                //}

                e.Row.Attributes.Add("OnMouseOver", "javascript:this.style.backgroundColor='#e4edf3';");
                e.Row.Attributes.Add("OnMouseOut", "javascript:this.style.backgroundColor='#FFFFFF';");
                e.Row.Style["cursor"] = "hand";
            }
        }

        protected void gvAFIPTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dT = null;

            strDBName = Request.QueryString["db"];

            if (strDBName != null && strDBName != string.Empty)
            {
                lnkCrear.PostBackUrl = "edititem.aspx?t=" + strDBName;
                lblPageTitle.Text = "Mostrando Tabla: " + strDBName;

                if (strDBName.StartsWith("AFIP") || strDBName.StartsWith("EquivAFIP"))
                {
                    dT = GetAFIPTable(strDBName, "*", "Id ASC");
                }
                else
                {
                    dT = GetAFIPTable(strDBName, "EmpresaID, PuntoVentaID, Descripcion", "PuntoVentaID ASC");
                }

                gvAFIPTable.DataSource = dT.DefaultView;
                gvAFIPTable.PageIndex = e.NewPageIndex;
                gvAFIPTable.DataBind();
            }
        }

        protected void gvAFIPTable_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // center all cells
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Style.Add("text-align", "center");
            }
        }
}
}
