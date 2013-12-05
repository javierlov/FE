using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace FacturaElectronica
{
    public partial class activityreg : BasePage
    {
        private const string DefaultProviderName = "System.Data.SqlClient";
        private string strCheckedItemsIndex = string.Empty;
        private string strCheckedItems = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                GridView1.PageSize = Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
                ddlSearchBy.Attributes.Add("onchange", "calendarPicker(this, 'false');");
                // for multiple downloads
                strCheckedItemsIndex = GetCheckedItemsIndex();
                UpdateGrid();
                strCheckedItems = GetCheckedItems(strCheckedItemsIndex);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Session["xmlSearchQuery"] = null;
            UpdateGrid(); 
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(9).ToString().Trim() == "Error" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(9).ToString().Trim() == "Rechazado"
                    || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(9).ToString().Trim() == "Iniciada")
                {
                    e.Row.Cells[0].Enabled = false;
                    e.Row.Cells[14].Controls.Clear();

                    //TODO: agregar EmpresaID
                    if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(9).ToString().Trim() != "Iniciada")
                    {
                        e.Row.Cells[12].Attributes.Add("OnClick", "window.showModelessDialog('showerrors.aspx?nc=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(0).ToString() + "&eid=" + ConfigurationManager.AppSettings["EmpresaID"].ToString() + "','viewerror','dialogWidth:720px; dialogHeight:300px; center: yes; resizable: no; status: no')");
                        e.Row.Cells[12].Style.Add("color", "navy");
                        e.Row.Cells[12].Attributes.Add("OnMouseOver", "javascript:this.style.textDecoration='underline';");
                        e.Row.Cells[12].Attributes.Add("OnMouseOut", "javascript:this.style.textDecoration='none';");
                        e.Row.Cells[12].Style["cursor"] = "hand";
                    }
                }
                else if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(9).ToString().Trim() == "Aceptado")
                {
                    //Original
                    ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[14].Controls[1])).Attributes.Add("OnClick", "window.open('download.aspx?nc=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(0).ToString() + "&type=o&nt=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(10).ToString() + "&leg=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(6).ToString() + "&eid=" + ConfigurationManager.AppSettings["EmpresaID"].ToString() + "','down', 'width=200px,height=150px,top=1, left=1, toolbar=no,resizable=no,statusbar=no');return false;");

                    //Duplicado
                    ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[14].Controls[3])).Attributes.Add("OnClick", "window.open('download.aspx?nc=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(0).ToString() + "&type=d&nt=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(10).ToString() + "&leg=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(6).ToString() + "&eid=" + ConfigurationManager.AppSettings["EmpresaID"].ToString() + "','down', 'width=200px,height=150px,top=1, left=1, toolbar=no,resizable=no,statusbar=no');return false;");

                    //Triplicado
                    ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[14].Controls[5])).Attributes.Add("OnClick", "window.open('download.aspx?nc=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(0).ToString() + "&type=t&nt=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(10).ToString() + "&leg=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(6).ToString() + "&eid=" + ConfigurationManager.AppSettings["EmpresaID"].ToString() + "','down', 'width=200px,height=150px,top=1, left=1, toolbar=no,resizable=no,statusbar=no');return false;");
                }
                else
                {
                    e.Row.Cells[14].Controls.Clear();
                }

                //log icon
                ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[15].Controls[1])).Attributes.Add("OnClick", "window.showModelessDialog('showlogs.aspx?nc=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(0).ToString() + "&nt=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(10).ToString() + "&leg=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(6).ToString() + "&erp=" + ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray.GetValue(4).ToString() + "','viewerror','dialogWidth:710px; dialogHeight:300px; center: yes; resizable: no; status: no;scrollbars:no');return false;");

                //put .00 for import columns
                if (e.Row.Cells[11].Text.Length > 0)
                {
                    if (e.Row.Cells[11].Text.Split(',').Length > 1)
                    {
                        if (e.Row.Cells[11].Text.Split(',')[1].Length == 1)
                        {
                            e.Row.Cells[11].Text = e.Row.Cells[11].Text + "0";
                        }
                    }
                    else
                    {
                        e.Row.Cells[11].Text = e.Row.Cells[11].Text + ",00";
                    }
                }
                else
                {
                    e.Row.Cells[11].Text = "0,00";
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("OnClick", "SelectAll()");
            }
        }

        protected void ImgReload_Click(object sender, ImageClickEventArgs e)
        {
            UpdateGrid();
        }

        private string DefaultSelectCommand(string EmpresaID)
        {
            return string.Format("SELECT * FROM Comprobantes WHERE EmpresaID = '{0}' ORDER BY [ID] DESC",EmpresaID);
        }

        private void UpdateGrid()
        {
            XmlDocument xmlQuery = new XmlDocument();

            string strAuxDate = string.Empty;
            string filterAux = string.Empty;
            string strFilterTitle = "Condicion: ";

            try
            {
                SqlDataSource1.ConnectionString = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();
                string empresa_id = ConfigurationManager.AppSettings["EmpresaID"].ToString();
                SqlDataSource1.SelectCommand = DefaultSelectCommand(empresa_id);
                SqlDataSource1.ProviderName = DefaultProviderName;
                
                if (Session["xmlSearchQuery"] == null)
                {
                    lblConditionInfo.Visible = false;

                    if (txtSearchText.Text != string.Empty && ddlSearchBy.SelectedIndex > 0)
                    {
                        if (ddlSearchBy.SelectedItem.Value == "CompradorCodigoCliente")
                        {
                            if (txtSearchText.Text != string.Empty)
                            {
                                SqlDataSource1.FilterExpression = "[" + ddlSearchBy.SelectedItem.Value + "] = '" + txtSearchText.Text + "'";

                                SqlDataSource1.DataBind();

                                GridView1.DataBind();
                            }
                        }
                        else if (ddlSearchBy.SelectedItem.Value == "FechaComprobante")
                        {
                            if (txtSearchText.Text.Split('/').Length == 3)
                            {
                                strAuxDate = GetFormatedDate(txtSearchText.Text);

                                SqlDataSource1.FilterExpression = ddlSearchBy.SelectedItem.Value + " = '" + strAuxDate + "'";

                                SqlDataSource1.DataBind();

                                GridView1.DataBind();
                            }
                        }
                        else
                        {
                            if (txtSearchText.Text != string.Empty)
                            {
                                SqlDataSource1.FilterExpression = "[" + ddlSearchBy.SelectedItem.Value + "] like '%" + txtSearchText.Text + "%'";

                                SqlDataSource1.DataBind();

                                GridView1.DataBind();
                            }
                        }
                    }
                    else
                    {
                        if (ddlSearchBy.SelectedIndex == 0)
                        {
                            SqlDataSource1.FilterExpression = "";

                            SqlDataSource1.DataBind();

                            GridView1.DataBind();                            
                        }
                    }
                }
                else
                {
                    xmlQuery.LoadXml(Session["xmlSearchQuery"].ToString());

                    foreach (XmlNode xNode in xmlQuery.SelectNodes("search/condition"))
                    {
                        if (xNode["field"].InnerText == "FechaComprobante")
                        {
                            if (xNode["value"].InnerText.Split('/').Length == 3)
                            {
                                strFilterTitle += xNode["field"].InnerText + " >= " + xNode["value"].InnerText + "; ";

                                strAuxDate = GetFormatedDate(xNode["value"].InnerText);

                                filterAux += "[" + xNode["field"].InnerText + "] >= '" + strAuxDate + "'";

                                if (xNode["valueb"].InnerText.Split('/').Length == 3)
                                {
                                    strFilterTitle = strFilterTitle.Substring(0, strFilterTitle.Length - 2);
                                    strFilterTitle += " y " + xNode["field"].InnerText + " <= " + xNode["valueb"].InnerText + "; ";

                                    strAuxDate = GetFormatedDate(xNode["valueb"].InnerText);

                                    filterAux += " and ";
                                    filterAux += "[" + xNode["field"].InnerText + "] <= '" + strAuxDate + "'";
                                }
                            }
                        }
                        else if (xNode["field"].InnerText == "NroLegal")
                        {
                            strFilterTitle += xNode["field"].InnerText + " >= " + xNode["value"].InnerText + "; ";

                            filterAux += "[" + xNode["field"].InnerText + "] >= '" + xNode["value"].InnerText + "'";

                            if (xNode["valueb"].InnerText != string.Empty)
                            {
                                strFilterTitle = strFilterTitle.Substring(0, strFilterTitle.Length - 2);
                                strFilterTitle += " y " + xNode["field"].InnerText + " <= " + xNode["valueb"].InnerText + "; ";

                                filterAux += " and ";
                                filterAux += "[" + xNode["field"].InnerText + "] <= '" + xNode["valueb"].InnerText + "'";
                            }
                        }
                        else if (xNode["field"].InnerText == "CompradorCodigoCliente")
                        {
                            strFilterTitle += xNode["field"].InnerText + " = " + xNode["value"].InnerText + "; ";

                            filterAux += "[" + xNode["field"].InnerText + "] = '" + xNode["value"].InnerText + "'";
                        }
                        else
                        {
                            strFilterTitle += xNode["field"].InnerText + " = " + xNode["value"].InnerText + "; ";

                            filterAux += "[" + xNode["field"].InnerText + "] like '%%" + xNode["value"].InnerText + "%%'";
                        }

                        if (xmlQuery.SelectSingleNode("search").LastChild != xNode)
                            filterAux += " and ";
                    }

                    lblConditionInfo.Visible = true;
                    lblConditionInfo.Text = strFilterTitle;

                    SqlDataSource1.FilterExpression = filterAux;
                    SqlDataSource1.DataBind();
                    GridView1.DataBind();
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private string GetFormatedDate(string DateString)
        {
            string strAuxDate = string.Empty;

            DateTime auxDT = new DateTime();
            if (DateTime.TryParse("25/12/2010", out auxDT))
            {
                strAuxDate = DateString;
            }
            else
            {
                strAuxDate = DateString.Split('/')[1] + "/" + DateString.Split('/')[0] + "/" + DateString.Split('/')[2];
            }

            return strAuxDate;
        }

        protected void btnDownloadSel_Click(object sender, ImageClickEventArgs e)
        {
            if (strCheckedItems.Split('|').Length > 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "window.open('downloadmultiple.aspx?ids=" + strCheckedItems + "','down', 'width=150px,height=150px,top=1, left=1, toolbar=no,resizable=no,statusbar=no');", true);

            strCheckedItems = string.Empty;
        }

        private string GetCheckedItems(string RowIndexList)
        {
            string strIds = string.Empty;
            string strTipoCbte = string.Empty;

            foreach (string strRowIndex in RowIndexList.Split('|'))
            {
                if (strRowIndex != string.Empty)
                {
                    int iRowIndex = Convert.ToInt16(strRowIndex);

                    switch (GridView1.Rows[iRowIndex].Cells[4].Text)
                    {
                        case "Factura A":
                            strTipoCbte = "1";
                            break;
                        case "Factura B":
                            strTipoCbte = "6";
                            break;
                        case "Factura E":
                            strTipoCbte = "19";
                            break;
                        case "Nota de D&#233;bito A":
                            strTipoCbte = "2";
                            break;
                        case "Nota de D&#233;bito B":
                            strTipoCbte = "7";
                            break;
                        case "Nota de D&#233;bito E":
                            strTipoCbte = "20";
                            break;
                        case "Nota de Cr&#233;dito A":
                            strTipoCbte = "3";
                            break;
                        case "Nota de Cr&#233;dito B":
                            strTipoCbte = "8";
                            break;
                        case "Nota de Cr&#233;dito E":
                            strTipoCbte = "21";
                            break;
                    }
                    //sqlid;typocomprobante|
                    strIds += GridView1.Rows[iRowIndex].Cells[1].Text + ";" + strTipoCbte + "|";
                }
            }

            return strIds;
        }

        private string GetCheckedItemsIndex()
        {
            string strIds = string.Empty;

            try
            {
                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    CheckBox cb = (CheckBox)gvRow.Cells[0].FindControl("chkSel");

                    if (cb != null && cb.Checked)
                    {
                        strIds += gvRow.RowIndex.ToString() + "|";
                    }
                }
            }
            catch
            {
            }
            return strIds;
        }

        protected void btnMail_Click(object sender, ImageClickEventArgs e)
        {
            if (strCheckedItems.Split('|').Length > 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "window.open('mailing.aspx?ids=" + strCheckedItems + "&eid=" + ConfigurationManager.AppSettings["EmpresaID"].ToString() + "','mail', 'width=650px,height=250px,top=200, left=200, toolbar=no,resizable=no,statusbar=no,scrollbars=no');", true);

            strCheckedItems = string.Empty;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string strCheckTipo = string.Empty;

            if (strCheckedItems != string.Empty && strCheckedItems.Split('|').Length > 0)
            {
                if (chkOriginal.Checked)
                    strCheckTipo = "o";
        
                if (chkDuplicado.Checked)
                    strCheckTipo += "d";

                if (chkTriplicado.Checked)
                    strCheckTipo += "t";

                if (strCheckTipo.Length > 0)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "window.open('print.aspx?ids=" + strCheckedItems + "&eid=" + ConfigurationManager.AppSettings["EmpresaID"].ToString() + "&t=" + strCheckTipo + "','print', 'width=600px,height=500px,top=50, left=100, toolbar=no,resizable=no,statusbar=no,scrollbars=no');", true);
            }
            strCheckedItems = string.Empty;
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
}
}