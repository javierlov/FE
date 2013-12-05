using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FacturaElectronica.Utils;
using System.Net.Mail;
using System.IO;
using System.Data;

namespace FacturaElectronica
{
    public partial class Mailing : BasePage
    {
        private string EmpresaID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            EmpresaID = Request.QueryString["eid"];
            Settings oSettings = new Settings(EmpresaID);

            if (oSettings.SMTPServer != string.Empty)
            {
                LoadCbte();
            }
            else
            {
                lblErrors.Text = "El servidor SMTP no esta configurado.";
            }
        }

        private void LoadCbte()
        {
            string strIds = Request.QueryString["ids"];
            string strId = string.Empty;

            TableRow tbRow;
            TableCell tbCell;

            DataTable dtCbte = new DataTable();

            try
            {
                foreach (string strCbte in strIds.Split('|'))
                {
                    tbCell = new TableCell();
                    tbRow = new TableRow();

                    strId = strCbte.Split(';')[0];

                    if (strId != string.Empty)
                    {
                        dtCbte = GetComprobanteDataTable(strId);

                        if (dtCbte.Rows[0]["nrocomprobantedesde"].ToString() != string.Empty)
                        {
                            tbCell.Controls.Add(new LiteralControl(dtCbte.Rows[0]["nrocomprobantedesde"].ToString() + " - " + dtCbte.Rows[0]["compradorrazonsocial"].ToString()));

                            tbRow = new TableRow();
                            tbRow.Cells.Add(tbCell);

                            tblComprobantes.Rows.Add(tbRow);
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                tblComprobantes.Rows.Clear();
                lblErrors.Text = "Error Cargando Comprobantes. " + ex.Message;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string strIds = Request.QueryString["ids"];
            string strId = string.Empty;

            MailMessage oMessage = null;
            Attachment oAttach = null;
            SmtpClient emailClient = null;
            System.Net.NetworkCredential SMTPUserInfo = null;

            FEPrint.FEPrintService FEPrintService = new FEPrint.FEPrintService();

            byte[] fileData = null;
            Stream oStream = null;

            DataTable dtCbte = new DataTable();
            DataTable dtUser = new DataTable();

            TableRow tbRow;
            TableCell tbCell;

            try
            {
                tblComprobantes.Rows.Clear();
                btnSend.Visible = false;

                EmpresaID = Request.QueryString["eid"];
                Settings oSettings = new Settings(EmpresaID);

                FEPrintService.Url = oSettings.UrlPrintWebService;
                FEPrintService.Credentials = System.Net.CredentialCache.DefaultCredentials;

                foreach (string strCbte in strIds.Split('|'))
                {
                    tbCell = new TableCell();
                    tbRow = new TableRow();                   

                    if (strCbte != string.Empty && strCbte.Split(';').Length == 2)
                    {
                        strId = strCbte.Split(';')[0];

                        dtCbte = GetComprobanteDataTable(strId);

                        if (dtCbte.Rows[0]["COMPRADORCODIGOCLIENTE"].ToString() != string.Empty)
                        {
                            dtUser = GetUserData(oSettings.Entrada.Split('\\')[0], oSettings.Entrada.Split('\\')[1], "IFMSFA_TERCEROS_AFIP", dtCbte.Rows[0]["COMPRADORCODIGOCLIENTE"].ToString());

                            if (dtUser.Rows.Count > 0)
                            {
                                if (dtUser.Rows[0]["CORREO"].ToString() != string.Empty)
                                {
                                    fileData = FEPrintService.GetCbte(EmpresaID, strId, FEPrint.TipoDeCopia.Original);

                                    oStream = new MemoryStream(fileData);
                                    oAttach = new Attachment(oStream, "Comprobante.pdf");

                                    oMessage = new MailMessage(oSettings.SMTPFrom, dtUser.Rows[0]["CORREO"].ToString(), oSettings.MailSubject, oSettings.MailMessage);
                                    oMessage.Attachments.Add(oAttach);

                                    if (oSettings.SMTPServer != string.Empty)
                                    {
                                        emailClient = new SmtpClient(oSettings.SMTPServer);

                                        if (oSettings.SMTPUser != string.Empty && oSettings.SMTPPassword != string.Empty)
                                        {
                                            SMTPUserInfo = new System.Net.NetworkCredential(oSettings.SMTPUser, oSettings.SMTPPassword);

                                            emailClient.UseDefaultCredentials = false;
                                            emailClient.Credentials = SMTPUserInfo;
                                        }

                                        emailClient.Send(oMessage);

                                        tbCell.Controls.Add(new LiteralControl(dtCbte.Rows[0]["nrocomprobantedesde"].ToString() + " - OK.<br>"));
                                    }
                                }
                                else
                                {
                                    tbCell.Controls.Add(new LiteralControl(dtCbte.Rows[0]["nrocomprobantedesde"].ToString() + " - El cliente no tiene correo.<br>"));
                                }
                            }
                            else
                            {
                                tbCell.Controls.Add(new LiteralControl(dtCbte.Rows[0]["nrocomprobantedesde"].ToString() + " - No hay información de Usuario para este cliente.<br>"));
                            }
                        }
                        else
                        {
                            tbCell.Controls.Add(new LiteralControl(dtCbte.Rows[0]["nrocomprobantedesde"].ToString() + " - El cliente no tiene un usuario creado.<br>"));
                        }
                    }

                    tbRow.Cells.Add(tbCell);

                    tblComprobantes.Rows.Add(tbRow);
                }
            }
            catch (Exception ex)
            {
                tblComprobantes.Rows.Clear();
                lblErrors.Text = "Error Enviando Correos. " + ex.Message;
            }
        }

        protected void BbtnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.close();</script>");
        }
    }
}