using System;
using System.Data;
using System.Web.UI.WebControls;
using FacturaElectronica.Utils;
using FacturaElectronica.DBEngine;
/*
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using FacturaElectronica.Common;
*/

namespace Accendo.ComprobanteElectronico
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            chkJurisdiccion.Enabled = false;
            chkOperacion.Enabled = false;
            chkTipoResponsable.Enabled = false;

            if (!IsPostBack)
            {
                lblBienes.ForeColor = System.Drawing.Color.Black;
                lblBienes.Text = "Sin Verificar";

                lblCapital.ForeColor = System.Drawing.Color.Black;
                lblCapital.Text = "Sin Verificar";

                lblExportacion.ForeColor = System.Drawing.Color.Black;
                lblExportacion.Text = "Sin Verificar";

                lblWSAABienes.ForeColor = System.Drawing.Color.Black;
                lblWSAABienes.Text = "Sin verificar";

                lblWSAACapital.ForeColor = System.Drawing.Color.Black;
                lblWSAACapital.Text = "Sin verificar";

                lblWSAAExportacion.ForeColor = System.Drawing.Color.Black;
                lblWSAAExportacion.Text = "Sin verificar";

                FillEmpresas();

                Settings oSettings = new Settings(ddlEmpresa.SelectedItem.Value);                

                FillSucursales(oSettings.EmpresaID);
            }
        }

        protected void btnObtenerNum_Click(object sender, EventArgs e)
        {            
            Settings oSettings = new Settings(ddlEmpresa.SelectedItem.Value);

            AfipConnection afipConnFex = new AfipConnection("wsfex", oSettings);
            AfipConnection afipConnFe = new AfipConnection("wsfe", oSettings);

            wsfex.Service afipFexService = new wsfex.Service();
            afipFexService.Url = oSettings.UrlAFIPwsfex;

            wsfe.Service feService = new wsfe.Service();
            feService.Url = oSettings.UrlAFIPwsfe;

            wsfex.ClsFEX_LastCMP afipLastEx = new wsfex.ClsFEX_LastCMP();

            wsfe.FEAuthRequest afipObjFEAuthRequest = new wsfe.FEAuthRequest();

            switch (ddlTipoComprobante.SelectedValue)
            {
                case "1":
                case "2":
                case "3":
                case "6":
                case "7":
                case "8":
                    try
                    {
                        lblErrorMessages.Text = "";
                        if (afipConnFe.ConnectionErrorDescription == string.Empty)
                        {
                            //Inicializo el objeto AuthRequest de la Afip
                            afipObjFEAuthRequest.Cuit = afipConnFe.Cuit;
                            afipObjFEAuthRequest.Sign = afipConnFe.Sign;
                            afipObjFEAuthRequest.Token = afipConnFe.Token;

                            wsfe.FERecuperaLastCbteResponse objFERecuperaLastCMPResponse = new wsfe.FERecuperaLastCbteResponse();

                            objFERecuperaLastCMPResponse = feService.FECompUltimoAutorizado(afipObjFEAuthRequest, Convert.ToInt16(ddlSucursales.SelectedValue), Convert.ToInt16(ddlTipoComprobante.SelectedValue));

                            lblNumCbte.Text = (objFERecuperaLastCMPResponse.CbteNro + 1).ToString();
                            lblNumCbte.Text = FillWithCeros(lblNumCbte.Text);
                        }
                        else
                        {
                            lblNumCbte.Text = "XXXXXXXX";
                            lblErrorMessages.Text = "Error de login con AFIP.<br>Error: " + afipConnFe.ConnectionErrorDescription;
                        }                        
                    }
                    catch (Exception ex)
                    {
                        lblNumCbte.Text = "XXXXXXXX";
                        lblErrorMessages.Text = "Error: " + ex.Message;
                    }

                    //wsfe.FETributoResponse trires = feService.FEParamGetTiposTributos(afipObjFEAuthRequest);
                    break;

                case "19":
                case "20":
                case "21":
                    try
                    {
                        lblErrorMessages.Text = "";
                        if (afipConnFex.ConnectionErrorDescription == string.Empty)
                        {
                            //Inicializo el objeto AuthRequest de la Afip
                            afipLastEx.Cuit = afipConnFex.Cuit;
                            afipLastEx.Sign = afipConnFex.Sign;
                            afipLastEx.Token = afipConnFex.Token;
                            afipLastEx.Pto_venta = Convert.ToInt16(ddlSucursales.SelectedItem.Text);
                            afipLastEx.Tipo_cbte = Convert.ToInt16(ddlTipoComprobante.SelectedItem.Value);

                            wsfex.FEXResponseLast_CMP lastResp = afipFexService.FEXGetLast_CMP(afipLastEx);

                            lblNumCbte.Text = (lastResp.FEXResult_LastCMP.Cbte_nro + 1).ToString();
                            lblNumCbte.Text = FillWithCeros(lblNumCbte.Text);
                        }
                        else
                        {
                            lblNumCbte.Text = "XXXXXXXX";
                            lblErrorMessages.Text = "Error de login con AFIP.<br>Error: " + afipConnFex.ConnectionErrorDescription;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblNumCbte.Text = "XXXXXXXX";
                        lblErrorMessages.Text = "Error: " + ex.Message;
                    }


                    break;
            }
        }

        private void FillSucursales(string EmpresaID)
        {
            DataTable dt = MSSQLConnector.GetListOf("PuntoVenta", "Nombre, Descripcion", "EmpresaID = " + EmpresaID,null, 0);

            ddlSucursalCAE.Items.Clear();
            ddlSucursales.Items.Clear();

            foreach(DataRow dRow in dt.Rows)
            {
                ddlSucursales.Items.Add(new ListItem(dRow["Descripcion"].ToString(), dRow["Nombre"].ToString()));
                ddlSucursalCAE.Items.Add(new ListItem(dRow["Descripcion"].ToString(), dRow["Nombre"].ToString()));
            }
        }

        private void FillEmpresas()
        {
            EmpresaCollection eColl = Settings.GetEmpresas();

            ddlEmpresa.Items.Clear();

            foreach (Empresa oEmp in eColl)
            {
                ddlEmpresa.Items.Add(new ListItem(oEmp.RazonSocial, oEmp.EmpresaID));
            }
        }

        private string FillWithCeros(string StringNumber)
        {
            string AuxString = StringNumber;

            if (StringNumber.Length < 8)
            {
                for (int i = 0; i < (8 - StringNumber.Length); i++)
                {
                    AuxString = "0" + AuxString;
                }
            }

            return AuxString;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Settings oSettings = null;

            wsfex.Service afipFEXService = null;
            wsfex.ClsFEXAuthRequest afipObjFEXAuthRequest = null;
            
            wsfeb.Service afipFEBService = null;
            wsfeb.ClsBFEAuthRequest afipObjFEBAuthRequest = null;

            AfipConnection afipConnFEX = null;
            AfipConnection afipConnFEB = null;

            string strCodigo = string.Empty;
            string strDescripcion = string.Empty;

            FacturaElectronica.DBEngine.SQLEngine sqlEngine = new FacturaElectronica.DBEngine.SQLEngine();

            try
            {
                oSettings = new Settings(ddlEmpresa.SelectedItem.Value);

                //Exportacion
                afipFEXService = new wsfex.Service();
                afipFEXService.Url = oSettings.UrlAFIPwsfex;
                afipObjFEXAuthRequest = new wsfex.ClsFEXAuthRequest();

                afipConnFEX = new AfipConnection("wsfex", oSettings);
                afipConnFEB = new AfipConnection("wsbfe", oSettings);

                //Inicializo el objeto AuthRequest de la Afip
                afipObjFEXAuthRequest.Cuit = afipConnFEX.Cuit;
                afipObjFEXAuthRequest.Sign = afipConnFEX.Sign;
                afipObjFEXAuthRequest.Token = afipConnFEX.Token;

                //Bienes y Servicios
                afipFEBService = new wsfeb.Service();
                afipFEBService.Url = oSettings.UrlAFIPwsbfe;
                afipObjFEBAuthRequest = new wsfeb.ClsBFEAuthRequest();

                if (afipConnFEB.ConnectionErrorDescription == string.Empty)
                {
                    //Inicializo el objeto AuthRequest de la Afip
                    afipObjFEBAuthRequest.Cuit = afipConnFEB.Cuit;
                    afipObjFEBAuthRequest.Sign = afipConnFEB.Sign;
                    afipObjFEBAuthRequest.Token = afipConnFEB.Token;

                    if (chkIncoterms.Checked)
                    {
                        wsfex.FEXResponse_Inc resultInc = afipFEXService.FEXGetPARAM_Incoterms(afipObjFEXAuthRequest);
                        for (int u = 0; u < resultInc.FEXResultGet.Length; u++)
                        {
                            wsfex.ClsFEXResponse_Inc thisValue = (wsfex.ClsFEXResponse_Inc)resultInc.FEXResultGet.GetValue(u);
                            strCodigo = thisValue.Inc_Id.ToString();
                            strDescripcion = thisValue.Inc_Ds.ToString();
                            sqlEngine.InsertarCodigo("AFIPIncoterms", strCodigo, strDescripcion);
                        }
                    }

                    if (chkJurisdiccion.Checked)
                    {
                        //No hay clase en AFIP
                    }

                    if (chkMoneda.Checked)
                    {
                        wsfex.FEXResponse_Mon resultMon = afipFEXService.FEXGetPARAM_MON(afipObjFEXAuthRequest);
                        for (int u = 0; u < resultMon.FEXResultGet.Length; u++)
                        {
                            wsfex.ClsFEXResponse_Mon thisValue = (wsfex.ClsFEXResponse_Mon)resultMon.FEXResultGet.GetValue(u);
                            strCodigo = thisValue.Mon_Id.ToString();
                            strDescripcion = thisValue.Mon_Ds.ToString();
                            sqlEngine.InsertarCodigo("AFIPMoneda", strCodigo, strDescripcion);
                        }
                    }

                    if (chkOperacion.Checked)
                    {
                        //No hay clase en AFIP
                    }

                    if (chkPais.Checked)
                    {
                        wsfex.FEXResponse_DST_pais resultPais = afipFEXService.FEXGetPARAM_DST_pais(afipObjFEXAuthRequest);
                        for (int u = 0; u < resultPais.FEXResultGet.Length; u++)
                        {
                            wsfex.ClsFEXResponse_DST_pais thisValue = (wsfex.ClsFEXResponse_DST_pais)resultPais.FEXResultGet.GetValue(u);
                            strCodigo = thisValue.DST_Codigo.ToString();
                            strDescripcion = thisValue.DST_Ds.ToString();
                            sqlEngine.InsertarCodigo("AFIPPais", strCodigo, strDescripcion);
                        }
                    }

                    if (chkTasaIVA.Checked)
                    {
                        wsfeb.BFEResponse_Tipo_IVA resultTipoIVA = afipFEBService.BFEGetPARAM_Tipo_IVA(afipObjFEBAuthRequest);
                        for (int u = 0; u < resultTipoIVA.BFEResultGet.Length; u++)
                        {
                            wsfeb.ClsBFEResponse_Tipo_IVA thisValue = (wsfeb.ClsBFEResponse_Tipo_IVA)resultTipoIVA.BFEResultGet.GetValue(u);
                            strCodigo = thisValue.IVA_Id.ToString();
                            strDescripcion = thisValue.IVA_Ds.ToString();
                            sqlEngine.InsertarCodigo("AFIPTasaIVA", strCodigo, strDescripcion);
                        }
                    }

                    if (chkTipoCbte.Checked)
                    {
                        wsfex.FEXResponse_Tipo_Cbte resultCbtes = afipFEXService.FEXGetPARAM_Tipo_Cbte(afipObjFEXAuthRequest);
                        for (int u = 0; u < resultCbtes.FEXResultGet.Length; u++)
                        {
                            wsfex.ClsFEXResponse_Tipo_Cbte thisValue = (wsfex.ClsFEXResponse_Tipo_Cbte)resultCbtes.FEXResultGet.GetValue(u);
                            strCodigo = thisValue.Cbte_Id.ToString();
                            strDescripcion = thisValue.Cbte_Ds.ToString();
                            sqlEngine.InsertarCodigo("AFIPTipoComprobante", strCodigo, strDescripcion);
                        }
                    }

                    if (chkTipoDoc.Checked)
                    {
                        wsfeb.BFEResponse_Tipo_doc resultTipoDoc = afipFEBService.BFEGetPARAM_Tipo_doc(afipObjFEBAuthRequest);
                        for (int u = 0; u < resultTipoDoc.BFEResultGet.Length; u++)
                        {
                            wsfeb.ClsBFEResponse_Tipo_doc thisValue = (wsfeb.ClsBFEResponse_Tipo_doc)resultTipoDoc.BFEResultGet.GetValue(u);
                            strCodigo = thisValue.Doc_Id.ToString();
                            strDescripcion = thisValue.Doc_Ds.ToString();
                            sqlEngine.InsertarCodigo("AFIPTipoDocumento", strCodigo, strDescripcion);
                        }
                    }

                    if (chkUnidadMedida.Checked)
                    {
                        wsfex.FEXResponse_Umed resultUmed = afipFEXService.FEXGetPARAM_UMed(afipObjFEXAuthRequest); ;
                        for (int u = 0; u < resultUmed.FEXResultGet.Length; u++)
                        {
                            wsfex.ClsFEXResponse_UMed thisValue = (wsfex.ClsFEXResponse_UMed)resultUmed.FEXResultGet.GetValue(u);
                            strCodigo = thisValue.Umed_Id.ToString();
                            strDescripcion = thisValue.Umed_Ds.ToString();
                            sqlEngine.InsertarCodigo("AFIPUnidadMedida", strCodigo, strDescripcion);
                        }
                    }

                    if (chkTipoResponsable.Checked)
                    {
                        //No hay clase en AFIP                
                    }

                    if (chkZona.Checked)
                    {
                        wsfeb.BFEResponse_Zon resultZon = afipFEBService.BFEGetPARAM_Zonas(afipObjFEBAuthRequest);
                        for (int u = 0; u < resultZon.BFEResultGet.Length; u++)
                        {
                            wsfeb.ClsBFEResponse_Zon thisValue = (wsfeb.ClsBFEResponse_Zon)resultZon.BFEResultGet.GetValue(u);
                            strCodigo = thisValue.Zon_Id.ToString();
                            strDescripcion = thisValue.Zon_Ds.ToString();
                            sqlEngine.InsertarCodigo("AFIPZona", strCodigo, strDescripcion);
                        }
                    }

                    if (chkTipoNCM.Checked)
                    {
                        wsfeb.BFEResponse_NCM resultNCM = afipFEBService.BFEGetPARAM_NCM(afipObjFEBAuthRequest);

                        if (resultNCM.BFEResultGet != null)
                        {
                            for (int u = 0; u < resultNCM.BFEResultGet.Length; u++)
                            {
                                wsfeb.ClsBFEResponse_NCM thisValue = (wsfeb.ClsBFEResponse_NCM)resultNCM.BFEResultGet.GetValue(u);
                                strCodigo = thisValue.NCM_Codigo.ToString();
                                strDescripcion = thisValue.NCM_Ds.ToString();
                                sqlEngine.InsertarCodigo("AFIPCodigoNCM", strCodigo, strDescripcion);
                            }
                        }
                        else if (resultNCM.BFEErr != null)
                        {
                            lblErrorMessages.Text = "Error al procesar Tabla AFIPCodigoNCM.<br>Codigo: " + resultNCM.BFEErr.ErrCode.ToString() + "<br>Error: " + resultNCM.BFEErr.ErrMsg;
                        }
                    }
                }
                else
                {
                    string errorMessage = "Error de login con AFIP.";

                    if (afipConnFEB != null && afipConnFEB.ConnectionErrorDescription != string.Empty)
                        errorMessage += "Error: " + afipConnFEB.ConnectionErrorDescription;

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('" + errorMessage + "')", true);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Error: " + ex.Message + "')", true);
            }
            sqlEngine.Close();
        }

        protected void btnCheckAfipService_Click(object sender, EventArgs e)
        {
            try
            {
                Settings oSettings = new Settings(ddlEmpresa.SelectedItem.Value);

                CheckAfipFexService(oSettings);

                CheckAfipFeService(oSettings);

                CheckAfipFebService(oSettings);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Error: " + ex.Message + "')", true);
            }
        }

        //TODO: correr en otro thread o utilizar updatepanel para mostrar barra de estado
        protected void btnCheckWSAA_Click(object sender, EventArgs e)
        {
            try
            {
                Settings oSettings = new Settings(ddlEmpresa.SelectedItem.Value);

                lblWSAAExportacion.Text = CheckAfipLogin("wsfex", oSettings);
                if (lblWSAAExportacion.Text == "OK")
                    lblWSAAExportacion.ForeColor = System.Drawing.Color.Green;
                else
                    lblWSAAExportacion.ForeColor = System.Drawing.Color.Red;

                lblWSAABienes.Text = CheckAfipLogin("wsfe", oSettings);
                if (lblWSAABienes.Text == "OK")
                    lblWSAABienes.ForeColor = System.Drawing.Color.Green;
                else
                    lblWSAABienes.ForeColor = System.Drawing.Color.Red;

                lblWSAACapital.Text = CheckAfipLogin("wsbfe", oSettings);
                if (lblWSAACapital.Text == "OK")
                    lblWSAACapital.ForeColor = System.Drawing.Color.Green;
                else
                    lblWSAACapital.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Error al intentar login con AFIP, vuelva a intentar la operacion en unos minutos. Exception: " + ex.Message + "')", true);
            }
        }

        //TODO: correr en otro thread o utilizar updatepanel para mostrar barra de estado
        protected string CheckAfipLogin(string serviceID, Settings oSettings)
        {
            lblErrorMessages.Text = "";
            AfipConnection.ResetCurrentAfipConnection(serviceID, oSettings);
            AfipConnection afipConn = new AfipConnection(serviceID, oSettings);
            if (afipConn.ConnectionErrorDescription == string.Empty)
            {
                return "OK";
            }
            else
            {
                if (afipConn.ConnectionErrorDescription != string.Empty)
                    return "ERROR: " + afipConn.ConnectionErrorDescription;
                else
                    return "ERROR de Conexion.";
            }
         }

        //TODO: correr en otro thread o utilizar updatepanel para mostrar barra de estado
        protected void CheckAfipFexService(Settings oSettings)
        {
            try
            {
                wsfex.Service afipFexService = new wsfex.Service();
                afipFexService.Url = oSettings.UrlAFIPwsfex;

                wsfex.DummyResponse dumResp = afipFexService.FEXDummy();

                if (dumResp.AppServer == "OK" && dumResp.AuthServer == "OK" && dumResp.DbServer == "OK")
                {
                    lblExportacion.ForeColor = System.Drawing.Color.Green;
                    lblExportacion.Text = "OK";
                }
                else
                {
                    lblExportacion.ForeColor = System.Drawing.Color.Red;
                    lblExportacion.Text = "ERROR";
                }
            }
            catch
            {
                lblExportacion.ForeColor = System.Drawing.Color.Red;
                lblExportacion.Text = "ERROR";
            }
        }

        //TODO: correr en otro thread o utilizar updatepanel para mostrar barra de estado
        protected void CheckAfipFeService(Settings oSettings)
        {
            try
            {
                wsfe.Service afipFeService = new wsfe.Service();
                afipFeService.Url = oSettings.UrlAFIPwsfe;

                wsfe.DummyResponse dumResp = afipFeService.FEDummy();

                if (dumResp.AppServer == "OK" && dumResp.AuthServer == "OK" && dumResp.DbServer == "OK")
                {
                    lblBienes.ForeColor = System.Drawing.Color.Green;
                    lblBienes.Text = "OK";
                }
                else
                {
                    lblBienes.ForeColor = System.Drawing.Color.Red;
                    lblBienes.Text = "ERROR";
                }
            }
            catch
            {
                lblBienes.ForeColor = System.Drawing.Color.Red;
                lblBienes.Text = "ERROR";
            }
        }

        //TODO: correr en otro thread o utilizar updatepanel para mostrar barra de estado
        protected void CheckAfipFebService(Settings oSettings)
        {
            try
            {
                wsfeb.Service afipFebService = new wsfeb.Service();
                afipFebService.Url = oSettings.UrlAFIPwsbfe;

                wsfeb.DummyResponse dumResp = afipFebService.BFEDummy();

                if (dumResp.AppServer == "OK" && dumResp.AuthServer == "OK" && dumResp.DbServer == "OK")
                {
                    lblCapital.ForeColor = System.Drawing.Color.Green;
                    lblCapital.Text = "OK";
                }
                else
                {
                    lblCapital.ForeColor = System.Drawing.Color.Red;
                    lblCapital.Text = "ERROR";
                }
            }
            catch
            {
                lblCapital.ForeColor = System.Drawing.Color.Red;
                lblCapital.Text = "ERROR";
            }
        }

        protected void btnGetCAE_Click(object sender, EventArgs e)
        {
            try
            {
                Settings oSettings = new Settings(ddlEmpresa.SelectedItem.Value);

                AfipConnection afipConnFex = new AfipConnection("wsfex", oSettings);
                AfipConnection afipConnFe = new AfipConnection("wsfe", oSettings);
                AfipConnection afipConnFeB = new AfipConnection("wsbfe", oSettings);

                wsfex.Service fexService = new wsfex.Service();
                fexService.Url = oSettings.UrlAFIPwsfex;

                wsfe.Service feService = new wsfe.Service();
                feService.Url = oSettings.UrlAFIPwsfe;

                wsfeb.Service febService = new wsfeb.Service();
                febService.Url = oSettings.UrlAFIPwsbfe;

                wsfex.ClsFEX_LastCMP afipLastEx = new wsfex.ClsFEX_LastCMP();
                wsfex.ClsFEXAuthRequest objFEXAuthRequest = new wsfex.ClsFEXAuthRequest();
                wsfex.FEXGetCMPResponse objFEXGetCMPResponse = new wsfex.FEXGetCMPResponse();
                wsfex.ClsFEXRequest objFEXRequest = new wsfex.ClsFEXRequest();
                wsfex.FEXResponseAuthorize fexResponse = new wsfex.FEXResponseAuthorize();

                wsfe.FEAuthRequest objFEAuthRequest = new wsfe.FEAuthRequest();
                wsfe.FECompConsultaReq objFERequest = new wsfe.FECompConsultaReq();
                wsfe.FECompConsultaResponse feResponse = new wsfe.FECompConsultaResponse();

                //wsfe.FELastCMPtype objFELastCMType = new wsfe.FELastCMPtype();
                //wsfe.FEConsultaCAEResponse objFEConsultaCAEResponse = new wsfe.FEConsultaCAEResponse();
                //wsfe.FEConsultaCAEReq objCMPFE = new wsfe.FEConsultaCAEReq();
                //wsfe.FEResponse feResponse = new wsfe.FEResponse();
                //wsfe.FERequest objFERequest = new wsfe.FERequest();

                wsfeb.BFEGetCMPResponse objBFEGetCMPResponse = new wsfeb.BFEGetCMPResponse();
                wsfeb.ClsBFEGetCMP objCMP = new wsfeb.ClsBFEGetCMP();
                wsfeb.ClsBFEAuthRequest objBFEAuthRequest = new wsfeb.ClsBFEAuthRequest();

                DataTable returnDTable = new DataTable();

                SQLEngine sqlEngine = new SQLEngine();

                switch (ddlTipoComprobanteCAE.SelectedValue)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "6":
                    case "7":
                    case "8":
                        lblErrorMessages.Text = "";

                        if (afipConnFeB.ConnectionErrorDescription == string.Empty)
                        {
                            objBFEAuthRequest.Cuit = afipConnFeB.Cuit;
                            objBFEAuthRequest.Sign = afipConnFeB.Sign;
                            objBFEAuthRequest.Token = afipConnFeB.Token;

                            objCMP.Punto_vta = Convert.ToInt16(ddlSucursalCAE.SelectedValue);
                            objCMP.Tipo_cbte = Convert.ToInt16(ddlTipoComprobanteCAE.SelectedValue);
                            objCMP.Cbte_nro = Convert.ToInt64(txtNroComprobante.Text);

                            objBFEGetCMPResponse = febService.BFEGetCMP(objBFEAuthRequest, objCMP);

                            if (objBFEGetCMPResponse.BFEResultGet != null && objBFEGetCMPResponse.BFEResultGet.Fch_venc_Cae != null)
                            {
                                lblCAE.Text = objBFEGetCMPResponse.BFEResultGet.Cae;
                                lblVto.Text = objBFEGetCMPResponse.BFEResultGet.Fch_venc_Cae;//objBFEGetCMPResponse.BFEResultGet.Fch_venc_Cae.Substring(3, 2) + "/" + objBFEGetCMPResponse.BFEResultGet.Fch_venc_Cae.Substring(4, 2) + "/" + objBFEGetCMPResponse.BFEResultGet.Fch_venc_Cae.Substring(0, 4);
                            }
                            else
                            {
                                objFEAuthRequest.Cuit = afipConnFe.Cuit;
                                objFEAuthRequest.Sign = afipConnFe.Sign;
                                objFEAuthRequest.Token = afipConnFe.Token;
                                
                                objFERequest.CbteNro = Convert.ToInt64(txtNroComprobante.Text);
                                objFERequest.CbteTipo = Convert.ToInt16(ddlTipoComprobanteCAE.SelectedValue);
                                objFERequest.PtoVta = Convert.ToInt16(ddlSucursalCAE.SelectedValue);

                                feResponse = feService.FECompConsultar(objFEAuthRequest, objFERequest);

                                if (feResponse.ResultGet != null && feResponse.ResultGet.CodAutorizacion != null)
                                {
                                    lblCAE.Text = feResponse.ResultGet.CodAutorizacion;
                                    lblVto.Text = feResponse.ResultGet.FchVto; //.Substring(6, 2) + "/" + feResponse.FecResp.fecha_cae.Substring(4, 2) + "/" + feResponse.FecResp.fecha_cae.Substring(0, 4); ;
                                }
                                else
                                {
                                    lblCAE.Text = "Comprobante inexistente.";
                                    lblVto.Text = "XX/XX/XXXX";
                                }

                                ////si es bienes y servicios hay que reprocesar para obtener CAE, lo busco en la base
                                //returnDTable = sqlEngine.GetItems("cbtecabecera", "*", "NroComprobanteDesde = '" + FillWithCeros(txtNroComprobante.Text) + "' and PuntoVenta = '" + ddlSucursalCAE.SelectedValue + "' and TipoComprobante = '" + ddlTipoComprobanteCAE.SelectedValue + "'", 0);



                                //if (returnDTable.Rows.Count > 0)
                                //{
                                //    if (returnDTable.Rows[0]["BatchUniqueId"].ToString() != string.Empty)
                                //    {
                                //        wsfe.FECabeceraRequest objFECabeceraRequest = new wsfe.FECabeceraRequest();
                                //        objFECabeceraRequest.cantidadreg = 0;
                                //        objFECabeceraRequest.id = Convert.ToInt64(returnDTable.Rows[0]["BatchUniqueId"].ToString());
                                //        objFECabeceraRequest.presta_serv = 0;

                                //        wsfe.FEDetalleRequest[] aObjFEDetalleRequest = new wsfe.FEDetalleRequest[1];

                                //        wsfe.FEDetalleRequest objFEDetalleRequest = new wsfe.FEDetalleRequest();
                                //        objFEDetalleRequest.tipo_doc = Convert.ToInt16(returnDTable.Rows[0]["CompradorCodigoDocumento"]);
                                //        objFEDetalleRequest.nro_doc = (long)Convert.ToDouble(returnDTable.Rows[0]["CompradorNroDocumento"]);
                                //        objFEDetalleRequest.tipo_cbte = Convert.ToInt16(returnDTable.Rows[0]["TipoComprobante"]);
                                //        objFEDetalleRequest.punto_vta = Convert.ToInt16(returnDTable.Rows[0]["PuntoVenta"]);
                                //        objFEDetalleRequest.cbt_desde = (long)Convert.ToDouble(returnDTable.Rows[0]["NroComprobanteDesde"]);
                                //        objFEDetalleRequest.cbt_hasta = (long)Convert.ToDouble(returnDTable.Rows[0]["NroComprobanteHasta"]);
                                //        objFEDetalleRequest.imp_total = Convert.ToDouble(returnDTable.Rows[0]["Importe"]);
                                //        objFEDetalleRequest.imp_tot_conc = Convert.ToDouble(returnDTable.Rows[0]["ImporteNoGravado"]);
                                //        objFEDetalleRequest.imp_neto = Convert.ToDouble(returnDTable.Rows[0]["ImporteGravado"]);
                                //        objFEDetalleRequest.impto_liq = Convert.ToDouble(returnDTable.Rows[0]["ImporteImpuestoLiquidado"]);
                                //        objFEDetalleRequest.impto_liq_rni = Convert.ToDouble(returnDTable.Rows[0]["ImporteRNI_Percepcion"]);
                                //        objFEDetalleRequest.imp_op_ex = Convert.ToDouble(returnDTable.Rows[0]["ImporteExento"]);

                                //        // Las fechas deben venir en formato "YYYY-MM-DD"
                                //        objFEDetalleRequest.fecha_cbte = Convert.ToDateTime(returnDTable.Rows[0]["FechaComprobante"]).ToString("yyyyMMdd");
                                //        objFEDetalleRequest.fecha_serv_desde = Convert.ToDateTime(returnDTable.Rows[0]["FechaDesdeServicioFacturado"]).ToString("yyyyMMdd");
                                //        objFEDetalleRequest.fecha_serv_hasta = Convert.ToDateTime(returnDTable.Rows[0]["FechaHastaServicioFacturado"]).ToString("yyyyMMdd");
                                //        objFEDetalleRequest.fecha_venc_pago = Convert.ToDateTime(returnDTable.Rows[0]["FechaVencimientoPago"]).ToString("yyyyMMdd");
                                //        aObjFEDetalleRequest[0] = objFEDetalleRequest;

                                //        objFERequest.Fecr = objFECabeceraRequest;
                                //        objFERequest.Fedr = aObjFEDetalleRequest;

                                //        feResponse = feService.FEAutRequest(objFEAuthRequest, objFERequest);

                                //        if (feResponse.FedResp != null && feResponse.FedResp[0].cae != null)
                                //        {
                                //            lblCAE.Text = feResponse.FedResp[0].cae;
                                //            lblVto.Text = feResponse.FedResp[0].fecha_vto; //.Substring(6, 2) + "/" + feResponse.FecResp.fecha_cae.Substring(4, 2) + "/" + feResponse.FecResp.fecha_cae.Substring(0, 4); ;
                                //        }
                                //        else
                                //        {
                                //            lblCAE.Text = "Comprobante inexistente.";
                                //            lblVto.Text = "XX/XX/XXXX";
                                //        }
                                //    }
                                //    else
                                //    {
                                //        lblCAE.Text = "Comprobante sin BatchUniqueId.";
                                //        lblVto.Text = "XX/XX/XXXX";
                                //    }
                                //}
                                //else
                                //{
                                //    lblCAE.Text = "Comprobante inexistente.";
                                //    lblVto.Text = "XX/XX/XXXX";
                                //}
                            }
                        }
                        else
                        {
                            lblNumCbte.Text = "XXXXXXXX";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Error de login con AFIP, vuelva a intentar la operacion en unos minutos.')", true);
                        }
                        break;

                    case "19":
                    case "20":
                    case "21":

                        lblErrorMessages.Text = "";
                        if (afipConnFex.ConnectionErrorDescription == string.Empty)
                        {
                            //si es bienes y servicios hay que reprocesar para obtener CAE, lo busco en la base
                            returnDTable = sqlEngine.GetItems("cbtecabecera", "*", "NroComprobanteDesde = '" + FillWithCeros(txtNroComprobante.Text) + "' and PuntoVenta = '" + ddlSucursalCAE.SelectedValue + "' and TipoComprobante = '" + ddlTipoComprobanteCAE.SelectedValue + "' and BatchUniqueId IS NOT NULL", 0);

                            objFEXAuthRequest.Cuit = afipConnFex.Cuit;
                            objFEXAuthRequest.Sign = afipConnFex.Sign;
                            objFEXAuthRequest.Token = afipConnFex.Token;

                            if (returnDTable.Rows.Count > 0)
                            {
                                if (returnDTable.Rows[0]["BatchUniqueId"].ToString() != string.Empty)
                                {
                                    objFEXRequest.Id = (long)Convert.ToDouble(returnDTable.Rows[0]["BatchUniqueId"]);
                                    objFEXRequest.Tipo_cbte = Convert.ToInt16(returnDTable.Rows[0]["TipoComprobante"]);
                                    objFEXRequest.Punto_vta = Convert.ToInt16(returnDTable.Rows[0]["PuntoVenta"]);
                                    objFEXRequest.Cbte_nro = (long)Convert.ToDouble(returnDTable.Rows[0]["NroComprobanteDesde"]);
                                    objFEXRequest.Tipo_expo = Convert.ToInt16(returnDTable.Rows[0]["TipoExportacion"]);
                                    objFEXRequest.Permiso_existente = returnDTable.Rows[0]["PermisoExistente"].ToString();
                                    objFEXRequest.Dst_cmp = Convert.ToInt16(sqlEngine.ObtenerEquivalencia("EquivAFIPPais", ddlEmpresa.SelectedItem.Value, returnDTable.Rows[0]["PaisComprador"].ToString()));
                                    objFEXRequest.Cliente = returnDTable.Rows[0]["CompradorRazonSocial"].ToString();
                                    objFEXRequest.Domicilio_cliente = returnDTable.Rows[0]["CompradorDireccion"].ToString();
                                    objFEXRequest.Moneda_Id = returnDTable.Rows[0]["CodigoMoneda"].ToString();
                                    objFEXRequest.Moneda_ctz = Convert.ToDouble(returnDTable.Rows[0]["TasaCambio"]);
                                    objFEXRequest.Imp_total = Convert.ToDouble(returnDTable.Rows[0]["ImporteMonedaFacturacion"]);
                                    objFEXRequest.Idioma_cbte = Convert.ToInt16(returnDTable.Rows[0]["Idioma"]);
                                    objFEXRequest.Id_impositivo = returnDTable.Rows[0]["CompradorNroDocumento"].ToString();
                                    objFEXRequest.Fecha_cbte = Convert.ToDateTime(returnDTable.Rows[0]["FechaComprobante"]).ToString("yyyyMMdd");
                                    objFEXRequest.Forma_pago = returnDTable.Rows[0]["FormaPagoDescrip"].ToString();
                                    objFEXRequest.Obs = returnDTable.Rows[0]["Observaciones"].ToString();
                                    objFEXRequest.Obs_comerciales = returnDTable.Rows[0]["ObservacionesComerciales"].ToString();
                                    objFEXRequest.Incoterms = returnDTable.Rows[0]["IncoTerms"].ToString();

                                    fexResponse = fexService.FEXAuthorize(objFEXAuthRequest, objFEXRequest);

                                    if (fexResponse.FEXResultAuth != null && fexResponse.FEXResultAuth.Cae != null)
                                    {
                                        lblCAE.Text = fexResponse.FEXResultAuth.Cae;
                                        lblVto.Text = fexResponse.FEXResultAuth.Fch_venc_Cae;//.Substring(6, 2) + "/" + fexResponse.FEXResultAuth.Fch_venc_Cae.Substring(4, 2) + "/" + fexResponse.FEXResultAuth.Fch_venc_Cae.Substring(0, 4);
                                    }
                                    else
                                    {
                                        lblCAE.Text = "Comprobante inexistente.";
                                        lblVto.Text = "XX/XX/XXXX";
                                    }
                                }
                                else
                                {
                                    lblCAE.Text = "Comprobante sin BatchUniqueId.";
                                    lblVto.Text = "XX/XX/XXXX";
                                }
                            }
                            else
                            {
                                lblCAE.Text = "Comprobante inexistente.";
                                lblVto.Text = "XX/XX/XXXX";
                            }
                        }
                        else
                        {
                            lblNumCbte.Text = "XXXXXXXX";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Error de login con AFIP, vuelva a intentar la operacion en unos minutos.')", true);
                        }

                        break;
                }
                sqlEngine.Close();
            }
            catch (Exception ex)
            {
                lblNumCbte.Text = "XXXXXXXX";
                lblErrorMessages.Text = "Error: " + ex.Message;
            }
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings oSettings = new Settings(ddlEmpresa.SelectedItem.Value);

            FillSucursales(oSettings.EmpresaID);
        }
}
}