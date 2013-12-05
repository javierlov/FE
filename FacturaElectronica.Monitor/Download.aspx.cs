using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Xml;
using FacturaElectronica.Utils;
using System.ComponentModel;

namespace FacturaElectronica
{
    public partial class download : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string EmpresaId = Request.QueryString["eid"];

                ProcessDownload(EmpresaId);
            }
            catch (Exception ex)
            {
                lblMsg.Text = "No se pudo generar archivo PDF.<br>" + ex.Message;
            }
        }

        private void ProcessDownload(string EmpresaID)
        {
            FEPrint.FEPrintService FEPrintService = new FEPrint.FEPrintService();

            string strId = Request.QueryString["nc"];
            string strTipoCbte = Request.QueryString["nt"];
            string strNumeroLegal = Request.QueryString["leg"];

            try
            {
                Settings oSettings = new Settings(EmpresaID);
                
                FEPrintService.Url = oSettings.UrlPrintWebService;
                FEPrintService.Credentials = System.Net.CredentialCache.DefaultCredentials;

                byte[] fileData = null;              
                
                switch (Request.QueryString["type"])
                {
                    case "o":
                        fileData = FEPrintService.GetCbte(EmpresaID, strId, FEPrint.TipoDeCopia.Original);//(byte[])sqlDReader["PDF" + strType];
                        break;
                    case "d":
                        fileData = FEPrintService.GetCbte(EmpresaID, strId, FEPrint.TipoDeCopia.Duplicado);
                        break;
                    case "t":
                        fileData = FEPrintService.GetCbte(EmpresaID, strId, FEPrint.TipoDeCopia.Triplicado);
                        break;
                    case "c":
                        fileData = FEPrintService.GetCbte(EmpresaID, strId, FEPrint.TipoDeCopia.Copia);
                        break;
                }

                if (fileData != null)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + strNumeroLegal + ".pdf");
                    Response.ContentType = "application/pdf";

                    BinaryWriter bw = new BinaryWriter(Response.OutputStream);
                    bw.Write(fileData);
                    bw.Close();

                    Response.End();
                }
                else
                {
                    lblMsg.Text = "No se pudo generar archivo PDF.<br>No se encontro detalle del comprobante.";
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }    
}