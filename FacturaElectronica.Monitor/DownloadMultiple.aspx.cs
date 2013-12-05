using System;
using System.Collections;
using System.Configuration;
using System.Threading;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Data.SqlClient;
using FacturaElectronica.Utils;
using Ionic.Zip;

namespace FacturaElectronica
{
    public partial class DownloadMultiple : BasePage
    {       
        ZipFile fiZip = new ZipFile();

        protected void Page_Load(object sender, EventArgs e)
        {
            string EmpresaId = Request.QueryString["eid"];
            ProcessDownload(EmpresaId);
        }

        //todo: hacer
        private void ProcessDownload(string EmpresaID)
        {
            Settings oSettings = new Settings(EmpresaID);

            FEPrint.FEPrintService FEPrintService = new FEPrint.FEPrintService();
            FEPrintService.Url = oSettings.UrlPrintWebService;
            FEPrintService.Credentials = System.Net.CredentialCache.DefaultCredentials;

            string strIds = Request.QueryString["ids"];
            string strId = string.Empty;
            string strTipoCbte = string.Empty;
            string strNumeroLegal = string.Empty;
            string strTipoCbteName = string.Empty;

            try
            {
                foreach (string strCbte in strIds.Split('|'))
                {
                    if (strCbte != string.Empty && strCbte.Split(';').Length == 3)
                    {
                        strId = strCbte.Split(';')[0];
                        strTipoCbte = strCbte.Split(';')[1];
                        strNumeroLegal = strCbte.Split(';')[2];

                        string fileName = oSettings.PathTemporales + @"\" + strNumeroLegal;

                        FEPrintService.GetCbte(EmpresaID, strId, FEPrint.TipoDeCopia.Original);//(byte[])sqlDReader["PDF" + strType];

                        if (!fiZip.EntryFileNames.Contains("Comprobantes/" + strNumeroLegal + "-" + strTipoCbteName + "-Original.pdf"))
                            fiZip.AddFile(fileName + "-" + strTipoCbteName + "-Original.pdf", "Comprobantes");

                        if (!fiZip.EntryFileNames.Contains("Comprobantes/" + strNumeroLegal + "-" + strTipoCbteName + "-Duplicado.pdf"))
                            fiZip.AddFile(fileName + "-" + strTipoCbteName + "-Duplicado.pdf", "Comprobantes");

                        if (!fiZip.EntryFileNames.Contains("Comprobantes/" + strNumeroLegal + "-" + strTipoCbteName + "-Triplicado.pdf"))
                            fiZip.AddFile(fileName + "-" + strTipoCbteName + "-Triplicado.pdf", "Comprobantes");
                    }
                }

                if (fiZip.Count > 0)
                {
                    LoadZipFile(fiZip);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "window.close();", true);
        }

        //TODO: PASAR AL SQLENGINE
        private void SavePDFToDB(string strPath, string strId, string strType)
        {
            FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read);

            //a byte array to read the file                 
            byte[] pdfbyte = new byte[fs.Length];
            fs.Read(pdfbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            fs.Dispose();

            string connstr = XMLConnector.GetConfigurationField("connectionstring");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            string query = "update CbteCabecera SET PDF" + strType + " = @pdf WHERE CbteID = " + strId;
            SqlParameter pdfparameter = new SqlParameter();

            pdfparameter.SqlDbType = SqlDbType.Image;
            pdfparameter.ParameterName = "pdf";
            pdfparameter.Value = pdfbyte;

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(pdfparameter);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            conn.Close();
            conn.Dispose();
        }

        private void SavePDFToFS(string strPath, string strId, string strType)
        {
            try
            {
                string connstr = XMLConnector.GetConfigurationField("connectionstring");
                SqlConnection conn = new SqlConnection(connstr);
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT PDF" + strType + ", PuntoVenta,NroComprobanteDesde,tipocomprobante FROM CbteCabecera WHERE CbteID=" + strId, conn);

                SqlDataReader sqlDReader = cmd.ExecuteReader();

                if (sqlDReader.Read())
                {
                    byte[] fileData = (byte[])sqlDReader["PDF" + strType];

                    FileStream fs = new FileStream(strPath, FileMode.CreateNew, FileAccess.ReadWrite);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(fileData);
                    bw.Close();
                    fs.Close();
                    fs.Dispose();
                }

                sqlDReader.Close();
                sqlDReader.Dispose();

                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
        }

        private bool CheckPDFFromDB(string strId, string strType)
        {
            string connstr = XMLConnector.GetConfigurationField("connectionstring");

            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT PDF" + strType + " FROM CbteCabecera WHERE CbteID=" + strId, conn);

            SqlDataReader sqlDReader = cmd.ExecuteReader();

            bool bResult = false;

            if (sqlDReader.Read())
            {
                if (sqlDReader["PDF" + strType] != DBNull.Value)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }

            sqlDReader.Close();
            sqlDReader.Dispose();

            conn.Close();
            conn.Dispose();

            return bResult;
        }

        private void LoadZipFile(ZipFile ZipFileObject)
        {
            try
            {
                Response.ClearContent();
                Response.AppendHeader("Content-Disposition", "attachment; filename=Comprobantes.zip");
                Response.ContentType = "application/x-zip-compressed";

                ZipFileObject.Save(Response.OutputStream);

                Response.End();

                ZipFileObject.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
        }
    }    
}