using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Accendo.ComprobanteElectronico
{
    public partial class edititem : BasePage
    {
        private string strItemID = string.Empty;
        private string strTable = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            strItemID = Request.QueryString["ca"];
            strTable = Request.QueryString["t"];

            DataTable dtEmpresas = GetEmpresas();

            if (!IsPostBack)
            {
                if (strTable != null && strTable != string.Empty)
                {
                    if (strTable.StartsWith("Equiv") || strTable.StartsWith("AFIP"))
                    {
                        DataTable dtAfip = GetCodigoAFIPValues(strTable.Replace("Equiv", ""));

                        if (dtAfip != null)
                        {
                            ddlCodigoAFIP.Items.Clear();

                            for (int i = 0; i < dtAfip.Rows.Count; i++)
                            {
                                ddlCodigoAFIP.Items.Add(dtAfip.Rows[i]["CodigoAFIP"].ToString().Trim());
                            }
                        }
                    }
                }
            }

            if (strItemID != null && strTable != null && strItemID != string.Empty && strTable != string.Empty)
            {
                btnModificar.Text = "Modificar";

                DataTable dT = new DataTable();

                if (!IsPostBack)
                {
                    lblPageTitle.Text = "Modificar Item";

                    if (strTable.StartsWith("Equiv") || strTable.StartsWith("AFIP"))
                    {
                        string stritemselect = "Id = '" + strItemID + "'";
                        if (strTable.Equals("AFIPImpuesto")) stritemselect = "EmpresaID = '" + strItemID + "'";
                        dT = GetAFIPTableItem(strTable, stritemselect);
                    }
                    else if (strTable.StartsWith("PuntoVenta"))
                    {
                        dT = GetAFIPTableItem(strTable, "PuntoVentaID = '" + strItemID + "'");
                    }
                    else
                    {
                        //todo:empresa
                    }

                    if (dT != null && dT.Rows.Count > 0)
                    {
                        if (strTable.StartsWith("EquivAFIP"))
                        {
                            pnEmpresaID.Visible = true;
                            pnCodigoEmpresa.Visible = true;
                            pnDDLCodigoAFIP.Visible = true;
                            pnTXTCodigoAFIP.Visible = false;
                            pnDescripcion.Visible = false;

                            foreach (DataRow dre in dtEmpresas.Rows)
                            {
                                ddlEmpresaID.Items.Add(new ListItem(dre["RazonSocial"].ToString().Trim(), dre["EmpresaID"].ToString().Trim()));

                                if (ddlEmpresaID.Items[ddlEmpresaID.Items.Count - 1].Value == dT.Rows[0]["EmpresaID"].ToString().Trim())
                                    ddlEmpresaID.Items[ddlEmpresaID.Items.Count - 1].Selected = true;
                            }
                            
                            txtCodigoEmpresa.Text = dT.Rows[0]["CodigoEmpresa"].ToString().Trim();

                            for (int i = 0; i < ddlCodigoAFIP.Items.Count; i++)
                            {
                                if (dT.Rows[0]["CodigoAFIP"].ToString() == ddlCodigoAFIP.Items[i].Text.Trim())
                                {
                                    ddlCodigoAFIP.Items[i].Selected = true;
                                }
                            }
                        }
                        else if (strTable.StartsWith("AFIP"))
                        {
                            pnEmpresaID.Visible = false;
                            pnCodigoEmpresa.Visible = false;
                            pnDDLCodigoAFIP.Visible = false;
                            pnDescripcion.Visible = true;

                            txtCodigoAFIP.Text = dT.Rows[0]["CodigoAFIP"].ToString().Trim();
                            txtDescripcion.Text = dT.Rows[0]["Descripcion"].ToString().Trim();
                        }
                        else if (strTable.StartsWith("PuntoVenta"))
                        {
                            pnEmpresaID.Visible = true;
                            pnCodigoEmpresa.Visible = true;
                            pnTXTCodigoAFIP.Visible = false;
                            pnDescripcion.Visible = true;
                            pnDDLCodigoAFIP.Visible = false;

                            lblCodigoEmpresa.Text = "Punto de Venta";

                            foreach (DataRow dre in dtEmpresas.Rows)
                            {
                                ddlEmpresaID.Items.Add(new ListItem(dre["RazonSocial"].ToString().Trim(), dre["EmpresaID"].ToString().Trim()));

                                if (ddlEmpresaID.Items[ddlEmpresaID.Items.Count - 1].Value == dT.Rows[0]["EmpresaID"].ToString().Trim())
                                    ddlEmpresaID.Items[ddlEmpresaID.Items.Count - 1].Selected = true;
                            }

                            txtCodigoEmpresa.Text = dT.Rows[0]["Nombre"].ToString().Trim();
                            txtDescripcion.Text = dT.Rows[0]["Descripcion"].ToString().Trim();
                        }
                        else
                        {
                            //todo:empresa
                        }
                    }
                    btnEliminar.Visible = true;
                }
            }
            else
            {
                foreach (DataRow dre in dtEmpresas.Rows)
                {
                    ddlEmpresaID.Items.Add(new ListItem(dre["RazonSocial"].ToString().Trim(), dre["EmpresaID"].ToString().Trim()));
                }

                btnEliminar.Visible = false;
                btnModificar.Text = "Crear";
                lblPageTitle.Text = "Crear Item";

                if (strTable.StartsWith("EquivAFIP"))
                {
                    pnEmpresaID.Visible = true;
                    pnCodigoEmpresa.Visible = true;
                    pnDDLCodigoAFIP.Visible = true;
                    pnDescripcion.Visible = false;
                    pnTXTCodigoAFIP.Visible = false;
                }
                else if (strTable.StartsWith("AFIP"))
                {
                    pnEmpresaID.Visible = false;
                    pnCodigoEmpresa.Visible = false;
                    pnDDLCodigoAFIP.Visible = false;
                    pnDescripcion.Visible = true;
                    pnTXTCodigoAFIP.Visible = true;
                }
                else if (strTable.StartsWith("PuntoVenta"))
                {
                    pnEmpresaID.Visible = true;
                    pnCodigoEmpresa.Visible = false;
                    pnDDLCodigoAFIP.Visible = false;
                    pnDescripcion.Visible = true;
                    pnTXTCodigoAFIP.Visible = true;

                    lblCodigoAFIP.Text = "Punto de Venta";
                }
                else
                {
                     //todo: empresa
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (strItemID != null && strTable != null && strItemID != string.Empty && strTable != string.Empty)
            {
                if (strTable.StartsWith("EquivAFIP"))
                {
                    SetAFIPTableItem(strTable, "CodigoAFIP = '" + ddlCodigoAFIP.SelectedItem.Value.Trim() + "', EmpresaID = " + ddlEmpresaID.SelectedValue + ", CodigoEmpresa = '" + txtCodigoEmpresa.Text.Trim() + "'", "Id = '" + strItemID.Trim() + "'");
                }
                else if (strTable.StartsWith("AFIP"))
                {
                    SetAFIPTableItem(strTable, "CodigoAFIP = '" + txtCodigoAFIP.Text.Trim() + "', Descripcion = '" + txtDescripcion.Text.Trim() + "'", "Id = '" + strItemID.Trim() + "'");
                }
                else if (strTable.StartsWith("PuntoVenta"))
                {
                    SetAFIPTableItem(strTable, "EmpresaID = '" + ddlEmpresaID.SelectedValue + "', Nombre = '" + txtCodigoEmpresa.Text + "', Descripcion = '" + txtDescripcion.Text.Trim() + "'", "PuntoVentaID = '" + strItemID.Trim() + "'");
                }
                else
                {
                    //todo: edit empresa
                }
            }
            else
            {
                if (strTable.StartsWith("EquivAFIP"))
                {
                    InsertAFIPTableItem(strTable, "( CodigoAFIP, EmpresaID, CodigoEmpresa ) values ('" + ddlCodigoAFIP.SelectedItem.Value.Trim() + "','" + ddlEmpresaID.SelectedValue + "','" + txtCodigoEmpresa.Text.Trim() + "')");
                }
                else if (strTable.StartsWith("AFIP"))
                {
                    InsertAFIPTableItem(strTable, "( CodigoAFIP, Descripcion ) values ('" + txtCodigoAFIP.Text.Trim() + "','" + txtDescripcion.Text.Trim() + "')");
                }
                else if (strTable.StartsWith("PuntoVenta"))
                {
                    InsertAFIPTableItem(strTable, "( EmpresaID, Nombre, Descripcion ) values ('" + ddlEmpresaID.SelectedValue + "','" + txtCodigoAFIP.Text.Trim() + "','" + txtDescripcion.Text.Trim() + "')");
                }
                else
                {
                    //todo:insert empresa
                }
            }

            Response.Redirect("afipabm.aspx?db=" + strTable);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (strItemID != null && strTable != null && strItemID != string.Empty && strTable != string.Empty)
            {
                if (strTable.StartsWith("EquivAFIP") || strTable.StartsWith("AFIP"))
                {
                    DeleteAFIPTableItem(strTable, "Id = '" + strItemID + "'");
                }
                else
                {
                    DeleteAFIPTableItem(strTable, "PuntoVentaID = '" + strItemID + "'");
                }
            }

            Response.Redirect("afipabm.aspx?db=" + strTable);
        }
}
}
