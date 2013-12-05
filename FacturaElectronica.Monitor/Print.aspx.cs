using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FacturaElectronica.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
 
namespace FacturaElectronica
{
    public partial class Impresion : BasePage
    {
        public string PDFName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessPrint();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ProcessPrint()
        {
            string EmpresaId = Request.QueryString["eid"];
            string strIds = Request.QueryString["ids"];
            string strTipos = Request.QueryString["t"];
            string strId = string.Empty;
            string strTipoCbte = string.Empty;
            string fileName = string.Empty;
            string strMergedFileName = string.Empty;

            Settings oSettings = new Settings(EmpresaId);

            FEPrint.FEPrintService FEPrintService = new FEPrint.FEPrintService();
            FEPrintService.Url = oSettings.UrlPrintWebService;
            FEPrintService.Credentials = System.Net.CredentialCache.DefaultCredentials;

            PdfMerge pdfMerger = new PdfMerge();

            try
            {
                foreach (string strCbte in strIds.Split('|'))
                {
                    if (strCbte != string.Empty && strCbte.Split(';').Length == 2)
                    {
                        strId = strCbte.Split(';')[0];
                        strTipoCbte = strCbte.Split(';')[1];

                        // imprimir original?
                        if (strTipos.IndexOf("o") > -1)
                        {
                            fileName = FEPrintService.SendToFileSystem(EmpresaId, strId, "Original");

                            if (fileName != string.Empty)
                                pdfMerger.AddDocument(fileName);
                        }

                        // imprimir duplicado?
                        if (strTipos.IndexOf("d") > -1)
                        {
                            fileName = FEPrintService.SendToFileSystem(EmpresaId, strId, "Duplicado");

                            if (fileName != string.Empty)
                                pdfMerger.AddDocument(fileName);
                        }

                        // imprimir triplicado?
                        if (strTipos.IndexOf("t") > -1)
                        {
                            // triplicado solo para comprobantes A y E
                            if (strTipoCbte != "6" || strTipoCbte != "7" || strTipoCbte != "8")
                            {
                                fileName = FEPrintService.SendToFileSystem(EmpresaId, strId, "Triplicado");

                                if (fileName != string.Empty)
                                    pdfMerger.AddDocument(fileName);
                            }
                        }

                        // imprimir copia?
                        if (strTipos.IndexOf("c") > -1)
                        {
                            fileName = FEPrintService.SendToFileSystem(EmpresaId, strId, "Copia");

                            if (fileName != string.Empty)
                                pdfMerger.AddDocument(fileName);
                        }
                    }
                }

                //cantidad de ids + fecha y hora actual
                if (pdfMerger != null && pdfMerger.Documents.Count > 0)
                {
                    strMergedFileName = pdfMerger.Documents.Count.ToString() + "_" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000");

                    pdfMerger.Merge(Server.MapPath("Temp") + "\\" + strMergedFileName + ".pdf");

                    PDFName = "Temp/" + strMergedFileName + ".pdf";
                }
                else
                {
                    PDFName = "Temp/blank.pdf";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
        }
    }    
}