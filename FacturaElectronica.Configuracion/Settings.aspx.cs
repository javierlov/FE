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
using System.Xml;

namespace Accendo.ComprobanteElectronico
{
    public partial class settings : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                FillddlEmpresa();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            if (ddlEmpresa.SelectedValue != string.Empty)
            {
                strQuery = "ActivarDebug = '" + chkDebug.Checked.ToString() + "', ";
                strQuery += "TipoEntrada = '" + ddlTipoEntrada.SelectedValue + "', ";
                strQuery += "Entrada = '" + PathEntrada.Text + "', ";
                strQuery += "EntradaExtra = '" + PathEntradaExtra.Text + "', ";
                strQuery += "TipoSalida = '" + ddlTipoSalida.SelectedValue + "', ";
                strQuery += "Salida = '" + PathSalida.Text + "', ";
                strQuery += "PathHistorico = '" + PathHistorico.Text + "', ";
                strQuery += "PathDebug = '" + PathDebug.Text + "', ";
                strQuery += "PathCertificate = '" + PathCertificate.Text + "', ";
                strQuery += "PathConnectionFiles = '" + PathConnectionFiles.Text + "', ";
                strQuery += "PathImpresion = '" + PathImpresion.Text + "', ";
                strQuery += "PathTemporales = '" + PathTemporales.Text + "', ";
                strQuery += "UrlFEWebService = '" + UrlFEWebService.Text + "', ";
                strQuery += "UrlPrintWebService = '" + UrlPrintWebService.Text + "', ";
                strQuery += "UrlAFIPwsaa = '" + UrlAFIPwsaa.Text + "', ";
                strQuery += "UrlAFIPwsfe = '" + UrlAFIPwsfe.Text + "', ";
                strQuery += "UrlAFIPwsfex = '" + UrlAFIPwsfex.Text + "', ";
                strQuery += "UrlAFIPwsbfe = '" + UrlAFIPwsbfe.Text + "', ";
                strQuery += "SMTPServer = '" + txtSMTPServer.Text + "', ";
                strQuery += "SMTPUser = '" + txtSMTPUser.Text + "', ";
                strQuery += "SMTPPassword = '" + txtSMTPPassword.Text + "', ";
                strQuery += "SMTPFrom = '" + txtSMTPFrom.Text + "', ";
                strQuery += "MailSubject = '" + txtAsunto.Text + "', ";
                strQuery += "MailMessage = '" + txtMessage.Text + "' ";

                UpdateConfigurationData("ConfiguracionGeneral", strQuery, "EmpresaID = '" + EmpresaID.Text + "'");

                strQuery = "Activo = '" + chkEmpresa.Checked.ToString() + "',";
                strQuery += "NroDocumento = '" + CUITEmpresaOperaServicio.Text + "' ";

                UpdateConfigurationData("Empresas", strQuery, "EmpresaID = '" + EmpresaID.Text + "'");
            }
        }

        private void FillddlEmpresa()
        {
            DataTable dT = GetEmpresas();

            //ddlEmpresa.Items.Add("Seleccione una Empresa");
            ddlEmpresa.Items.Add(new ListItem("Seleccione una Empresa", "0"));
            
            foreach (DataRow dr in dT.Rows)
            {
                ddlEmpresa.Items.Add(new ListItem(dr["RazonSocial"].ToString(), dr["EmpresaID"].ToString()));
            }
        }

        private void ClearControls()
        {
            EmpresaID.Text = string.Empty;
            EmpresaID.ReadOnly = true;
            CUITEmpresaOperaServicio.Text = string.Empty;
            chkEmpresa.Checked = false;
            chkDebug.Checked = false;
            PathEntrada.Text = string.Empty;
            PathEntradaExtra.Text = string.Empty;
            PathSalida.Text = string.Empty;
            PathHistorico.Text = string.Empty;
            PathDebug.Text = string.Empty;
            PathCertificate.Text = string.Empty;
            PathConnectionFiles.Text = string.Empty;
            UrlFEWebService.Text = string.Empty;
            UrlPrintWebService.Text = string.Empty;
            PathImpresion.Text = string.Empty;
            PathTemporales.Text = string.Empty;
            UrlAFIPwsaa.Text = string.Empty;
            UrlAFIPwsfe.Text = string.Empty;
            UrlAFIPwsfex.Text = string.Empty;
            UrlAFIPwsbfe.Text = string.Empty;
            txtSMTPServer.Text = string.Empty;
            txtSMTPUser.Text = string.Empty;
            string strPassword = string.Empty;
            txtSMTPPassword.Attributes.Add("value", strPassword);
            txtSMTPFrom.Text = string.Empty;
            txtAsunto.Text = string.Empty;
            txtMessage.Text = string.Empty;
            ddlTipoEntrada.Items[1].Selected = false;
            ddlTipoEntrada.Items[0].Selected = false;
            ddlTipoSalida.Items[1].Selected = false;
            ddlTipoSalida.Items[0].Selected = true;
            
        }

        private void FillTable()
        {
            DataTable dT = GetConfigurationData(ddlEmpresa.SelectedValue);

            ClearControls();

            if (dT.Rows.Count != 0)
            {
                EmpresaID.Text = (dT.Rows[0]["EmpresaID"] != null) ? dT.Rows[0]["EmpresaID"].ToString() : string.Empty;
                EmpresaID.ReadOnly = true;
                CUITEmpresaOperaServicio.Text = (dT.Rows[0]["NroDocumento"] != null)
                    ? dT.Rows[0]["NroDocumento"].ToString()
                    : string.Empty;
                chkEmpresa.Checked = (dT.Rows[0]["Activo"].ToString() == "True") ? true : false;
                chkDebug.Checked = (dT.Rows[0]["ActivarDebug"].ToString() == "True") ? true : false;
                PathEntrada.Text = (dT.Rows[0]["Entrada"] != null) ? dT.Rows[0]["Entrada"].ToString() : string.Empty;
                PathEntradaExtra.Text = (dT.Rows[0]["EntradaExtra"] != null)
                    ? dT.Rows[0]["EntradaExtra"].ToString()
                    : string.Empty;
                PathSalida.Text = (dT.Rows[0]["Salida"] != null) ? dT.Rows[0]["Salida"].ToString() : string.Empty;
                PathHistorico.Text = (dT.Rows[0]["PathHistorico"] != null)
                    ? dT.Rows[0]["PathHistorico"].ToString()
                    : string.Empty;
                PathDebug.Text = (dT.Rows[0]["PathDebug"] != null) ? dT.Rows[0]["PathDebug"].ToString() : string.Empty;
                PathCertificate.Text = (dT.Rows[0]["PathCertificate"] != null)
                    ? dT.Rows[0]["PathCertificate"].ToString()
                    : string.Empty;
                PathConnectionFiles.Text = (dT.Rows[0]["PathConnectionFiles"] != null)
                    ? dT.Rows[0]["PathConnectionFiles"].ToString()
                    : string.Empty;
                UrlFEWebService.Text = (dT.Rows[0]["UrlFEWebService"] != null)
                    ? dT.Rows[0]["UrlFEWebService"].ToString()
                    : string.Empty;
                UrlPrintWebService.Text = (dT.Rows[0]["UrlPrintWebService"] != null)
                    ? dT.Rows[0]["UrlPrintWebService"].ToString()
                    : string.Empty;
                PathImpresion.Text = (dT.Rows[0]["PathImpresion"] != null)
                    ? dT.Rows[0]["PathImpresion"].ToString()
                    : string.Empty;
                PathTemporales.Text = (dT.Rows[0]["PathTemporales"] != null)
                    ? dT.Rows[0]["PathTemporales"].ToString()
                    : string.Empty;
                UrlAFIPwsaa.Text = (dT.Rows[0]["UrlAFIPwsaa"] != null)
                    ? dT.Rows[0]["UrlAFIPwsaa"].ToString()
                    : string.Empty;
                UrlAFIPwsfe.Text = (dT.Rows[0]["UrlAFIPwsfe"] != null)
                    ? dT.Rows[0]["UrlAFIPwsfe"].ToString()
                    : string.Empty;
                UrlAFIPwsfex.Text = (dT.Rows[0]["UrlAFIPwsfex"] != null)
                    ? dT.Rows[0]["UrlAFIPwsfex"].ToString()
                    : string.Empty;
                UrlAFIPwsbfe.Text = (dT.Rows[0]["UrlAFIPwsbfe"] != null)
                    ? dT.Rows[0]["UrlAFIPwsbfe"].ToString()
                    : string.Empty;

                txtSMTPServer.Text = (dT.Rows[0]["SMTPServer"] != null)
                    ? dT.Rows[0]["SMTPServer"].ToString()
                    : string.Empty;
                txtSMTPUser.Text = (dT.Rows[0]["SMTPUser"] != null) ? dT.Rows[0]["SMTPUser"].ToString() : string.Empty;
                string strPassword = (dT.Rows[0]["SMTPPassword"] != null)
                    ? dT.Rows[0]["SMTPPassword"].ToString()
                    : string.Empty;
                txtSMTPPassword.Attributes.Add("value", strPassword);
                txtSMTPFrom.Text = (dT.Rows[0]["SMTPFrom"] != null) ? dT.Rows[0]["SMTPFrom"].ToString() : string.Empty;
                txtAsunto.Text = (dT.Rows[0]["MailSubject"] != null)
                    ? dT.Rows[0]["MailSubject"].ToString()
                    : string.Empty;
                txtMessage.Text = (dT.Rows[0]["MailMessage"] != null)
                    ? dT.Rows[0]["MailMessage"].ToString()
                    : string.Empty;

                if (dT.Rows[0]["TipoEntrada"].ToString() == "FS")
                {
                    ddlTipoEntrada.Items[1].Selected = false;
                    ddlTipoEntrada.Items[0].Selected = true;
                }
                else
                {
                    ddlTipoEntrada.Items[0].Selected = false;
                    ddlTipoEntrada.Items[1].Selected = true;
                }

                if (dT.Rows[0]["TipoSalida"].ToString() == "FS")
                {
                    ddlTipoSalida.Items[1].Selected = false;
                    ddlTipoSalida.Items[0].Selected = true;
                }
                else
                {
                    ddlTipoSalida.Items[0].Selected = false;
                    ddlTipoSalida.Items[1].Selected = true;
                }
            }

        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTable();
        }
}
}