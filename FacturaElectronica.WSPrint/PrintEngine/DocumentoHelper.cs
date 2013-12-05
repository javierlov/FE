using System;
using System.Data;
using System.Drawing;
using DevExpress.Charts.Native;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using FacturaElectronica.Utils;
using System.IO;
using ExtensionMethods;
using FacturaElectronica.WSPrint.Reportes.Sprayette;

namespace FacturaElectronica.PrintEngine
{
    public enum TipoDocumento
    {
        Factura,
        NotaCredito,
        NotaDebito,
        FacturaExportacion,
        NotaCreditoExportacion,
        NotaDebitoExportacion
    }

    public enum TipoDeCopia
    {
        Original,
        Duplicado,
        Triplicado,
        Copia
    }

    public class DocumentoHelper
    {
        private const string FORMATO_NRO = "{0:#,##0.00}";

        private Documento UnReporte { get; set; }

        /// <summary>
        /// Constructor del Helper, que expondrá metodos utiles para la operatoria con los documentos/reportes.
        /// </summary>
        /// <param name="unDocumento"></param>
        public DocumentoHelper(Documento unDocumento)
        {
            UnReporte = unDocumento;
        }

        #region Metodos Principales

        /// <summary>
        /// Preparo el objeto 'pdf' que luego servirá para llenar todos los campos del reporte.
        /// </summary>
        /// <param name="CbteCodigo"></param>
        /// <param name="CbteCopia"></param>
        /// <param name="sqlEngine"></param>
        /// <param name="oEmpresa"></param>
        /// <param name="oSettings"></param>
        /// <param name="dtHeader"></param>
        public void LlenarPropiedades(string CbteCodigo, TipoDeCopia tipoDeCopia, Empresa oEmpresa, Settings oSettings, DataTable dtHeader)
        {
            DBEngine.SQLEngine sqlEngine = new DBEngine.SQLEngine();

            UnReporte.oEmpresa = oEmpresa;
            UnReporte.TipoDeCopia = tipoDeCopia;
            UnReporte.CbteID = dtHeader.Rows[0]["CbteID"].ToString().Trim();
            UnReporte.NroCbte =  Utils.Utils.FillWithCeros(dtHeader.Rows[0]["PuntoVenta"].ToString().Trim(),4) + "-" +  Utils.Utils.FillWithCeros(dtHeader.Rows[0]["NroComprobanteDesde"].ToString(), 8);
            UnReporte.NombreEmpresa = dtHeader.Rows[0]["CompradorRazonSocial"].ToString();

            //-------------------------------------------------------------
            //Armo la dirección...
            string direccion = "";
            direccion += dtHeader.Rows[0]["CompradorDireccion"].ToString();

            //El código postal va siempre.
            if (dtHeader.Rows[0]["CompradorCodigoPostal"].ToString() != string.Empty && dtHeader.Rows[0]["CompradorCodigoPostal"].ToString() != "0")
                direccion += " - (" + dtHeader.Rows[0]["CompradorCodigoPostal"].ToString() + ")";

            //Solo muestro la Localidad, cuando la PROVINCIA es distinta de 0 (no es Capital Federal).
            if (Convert.ToString(dtHeader.Rows[0]["CompradorProvincia"]).Trim() != "0")
            {
                if (dtHeader.Rows[0]["CompradorLocalidad"].ToString() != string.Empty && dtHeader.Rows[0]["CompradorLocalidad"].ToString() != "0")
                    direccion += " - " + dtHeader.Rows[0]["CompradorLocalidad"].ToString();
            }

            //La provincia se muestra siempre.
            string provinciaConvertida = FacturaElectronica.WSPrint.Common.GetDescripcionProvincia(Convert.ToString(dtHeader.Rows[0]["CompradorProvincia"]));
            if (provinciaConvertida.Trim() != string.Empty)
                direccion += " - " + provinciaConvertida;

            if (sqlEngine.Open())
            {
                string paisConvertido = sqlEngine.ObtenerEquivalencia("EquivAFIPPais", oSettings.EmpresaID, dtHeader.Rows[0]["CompradorPais"].ToString());
                paisConvertido = WSPrint.Common.GetDescripcionPais(paisConvertido);
                if (paisConvertido != string.Empty && paisConvertido.Trim() != "0")
                    direccion += " - " + paisConvertido;
                
                sqlEngine.Close();
            }
            UnReporte.Domicilio = direccion;
            //-------------------------------------------------------------

            UnReporte.TipoIva = dtHeader.Rows[0]["CompradorTipoResponsableDescripcion"].ToString();
            UnReporte.CondicionDeVenta = dtHeader.Rows[0]["FormaPagoDescripcion"].ToString();
            UnReporte.CondicionDePago = dtHeader.Rows[0]["CondicionPago"].ToString();
            UnReporte.NumCuit = dtHeader.Rows[0]["CompradorNroDocumento"].ToString();
            UnReporte.NumRemito = dtHeader.Rows[0]["NroRemito"].ToString();
            UnReporte.NumIIBB = dtHeader.Rows[0]["CompradorNroIIBB"].ToString(); //
            UnReporte.NumCodIIBB = dtHeader.Rows[0]["CodigoJurisdiccionIIBB"].ToString();
            UnReporte.Son = dtHeader.Rows[0]["ImporteEscrito"].ToString();
            UnReporte.RefCliente = dtHeader.Rows[0]["CompradorNroReferencia"].ToString();
            UnReporte.RefInterna = dtHeader.Rows[0]["NroInternoERP"].ToString();
            UnReporte.AlicuotaIVA = dtHeader.Rows[0]["AlicuotaIVA"].ToString();

            UnReporte.EmisorDireccion = dtHeader.Rows[0]["EmisorDireccion"].ToString();
            UnReporte.EmisorCalle = dtHeader.Rows[0]["EmisorCalle"].ToString();
            UnReporte.EmisorCP = dtHeader.Rows[0]["EmisorCP"].ToString();
            UnReporte.EmisorLocalidad = dtHeader.Rows[0]["EmisorLocalidad"].ToString();
            UnReporte.EmisorProvincia = dtHeader.Rows[0]["EmisorProvincia"].ToString();
            UnReporte.EmisorPais = dtHeader.Rows[0]["EmisorPais"].ToString();
            UnReporte.EmisorTelefonos = dtHeader.Rows[0]["EmisorTelefonos"].ToString();
            UnReporte.EmisorEMail = dtHeader.Rows[0]["EmisorEMail"].ToString();

            UnReporte.DAGRUF =Convert.ToString(dtHeader.Rows[0]["DAGRUF"]);
            UnReporte.OPER = Convert.ToString(dtHeader.Rows[0]["OPER"]);
            UnReporte.NOPER = Convert.ToString(dtHeader.Rows[0]["NOPER"]);

            UnReporte.FECPG1_FORMATEADO = Convert.ToString(dtHeader.Rows[0]["FECPG1_FORMATEADO"]);
            UnReporte.FECPG2_FORMATEADO = Convert.ToString(dtHeader.Rows[0]["FECPG2_FORMATEADO"]);
            UnReporte.FACTORI_FORMATEADO = Convert.ToString(dtHeader.Rows[0]["FACTORI_FORMATEADO"]); //Convert.ToString(dtHeader.Rows[0]["FACTORI"]);
            UnReporte.USUARIO = Convert.ToString(dtHeader.Rows[0]["USUARIO"]);
            UnReporte.CODCLIENTECOMPRADOR = Convert.ToString(dtHeader.Rows[0]["CompradorCodigoCliente"]);
            UnReporte.OBSERVACIONRAPIPAGO = Convert.ToString(dtHeader.Rows[0]["ObservacionRapipago"]);
            UnReporte.ERRORCODIGO = GetErrorCodigo(UnReporte.CbteID);
            UnReporte.TasaIIBB = Convert.ToString(dtHeader.Rows[0]["TasaIIBB"]);

            if (dtHeader.Rows[0]["LetraComprobante"].ToString() != "B")
            {
                if (dtHeader.Rows[0]["ImporteImpuestoLiquidadoMonedaFacturacion"].ToString() != string.Empty)
                    UnReporte.TotalIva = String.Format(FORMATO_NRO, Convert.ToDecimal(dtHeader.Rows[0]["ImporteImpuestoLiquidadoMonedaFacturacion"].ToString()));
            }
            else
            {
                UnReporte.TotalIva = "0,00";
            }

            decimal subtotal = 0;
            if (dtHeader.Rows[0]["ImporteMonedaFacturacion"].ToString() != string.Empty)
                subtotal = Convert.ToDecimal(dtHeader.Rows[0]["ImporteMonedaFacturacion"].ToString());
            if (dtHeader.Rows[0]["ImportePercepcionIIBBMonedaFacturacion"].ToString() != string.Empty)
                subtotal -= Convert.ToDecimal(dtHeader.Rows[0]["ImportePercepcionIIBBMonedaFacturacion"].ToString());
            UnReporte.SubTotal = String.Format(FORMATO_NRO, subtotal);

            decimal subtotalB = 0;
            if (dtHeader.Rows[0]["ImporteMonedaFacturacionComprobanteB"].ToString() != string.Empty)
                subtotalB = Convert.ToDecimal(dtHeader.Rows[0]["ImporteMonedaFacturacionComprobanteB"].ToString());
            UnReporte.SubTotalComprobanteB = String.Format(FORMATO_NRO, subtotalB);

            decimal subtotalgravado = 0;
            if (dtHeader.Rows[0]["ImporteGravadoMonedaFacturacion"].ToString() != string.Empty)
                subtotalgravado -= Convert.ToDecimal(dtHeader.Rows[0]["ImporteGravadoMonedaFacturacion"].ToString());
            UnReporte.SubTotalGravadoMonedaFacturacion = String.Format(FORMATO_NRO, subtotal);

            decimal totalpercep = 0;
            if (dtHeader.Rows[0]["ImportePercepcionIIBBMonedaFacturacion"].ToString() != string.Empty)
                totalpercep = Convert.ToDecimal(dtHeader.Rows[0]["ImportePercepcionIIBBMonedaFacturacion"].ToString());
            UnReporte.TotalPercepcionIIBB = String.Format(FORMATO_NRO, totalpercep);

            if (dtHeader.Rows[0]["ImporteMonedaFacturacion"].ToString() != string.Empty)
                UnReporte.Total = String.Format(FORMATO_NRO, Convert.ToDecimal(dtHeader.Rows[0]["ImporteMonedaFacturacion"].ToString()));

            if (dtHeader.Rows[0]["ImporteMonedaFacturacionComprobanteB"].ToString() != string.Empty)
                UnReporte.TotalComprobanteB = String.Format(FORMATO_NRO, Convert.ToDecimal(dtHeader.Rows[0]["ImporteMonedaFacturacionComprobanteB"].ToString()));

            if (dtHeader.Rows[0]["ImporteGravadoMonedaFacturacion"].ToString() != string.Empty)
                UnReporte.TotalGravadoMonedaFacturacion = String.Format(FORMATO_NRO, Convert.ToDecimal(dtHeader.Rows[0]["ImporteGravadoMonedaFacturacion"].ToString()));

            UnReporte.LetraCbte = dtHeader.Rows[0]["LetraComprobante"].ToString();

            UnReporte.TipoCbte = tipoDeCopia.ToString();
            UnReporte.NumCodigo = "Código N° " + Utils.Utils.FillWithCeros(CbteCodigo, 2);

            string strTime = WSPrint.Common.GetDateStringValue(dtHeader.Rows[0]["FechaComprobante"].ToString());
            UnReporte.DayCbte = (strTime.Split('/')[0].Length > 1) ? strTime.Split('/')[0] : "0" + strTime.Split('/')[0];
            UnReporte.MonthCbte = (strTime.Split('/')[1].Length > 1) ? strTime.Split('/')[1] : "0" + strTime.Split('/')[1];
            UnReporte.YearCbte = strTime.Split('/')[2].ToString();
            UnReporte.FechaCbte = UnReporte.DayCbte + "/" + UnReporte.MonthCbte + "/" + UnReporte.YearCbte;

            DateTime dVencimiento;
            if (DateTime.TryParse(Convert.ToString(dtHeader.Rows[0]["FechaVencimiento"]), out dVencimiento))
                UnReporte.FechaVto = dVencimiento.ToString("dd/MM/yyyy");

            UnReporte.ObservacionesCabecera = dtHeader.Rows[0]["Observaciones1"].ToString().Replace("|", "\n");
            UnReporte.ObservacionesCuerpo = dtHeader.Rows[0]["Observaciones2"].ToString().Replace("|", "\n");
            UnReporte.ObservacionesPie = dtHeader.Rows[0]["Observaciones3"].ToString().Replace("|", "\n");

            string monedaConvertida = sqlEngine.ObtenerEquivalencia("EquivAFIPMoneda", oSettings.EmpresaID, dtHeader.Rows[0]["CodigoMoneda"].ToString().Trim());
            UnReporte.Moneda = WSPrint.Common.GetDescripcionMoneda(monedaConvertida);


            UnReporte.CAE = dtHeader.Rows[0]["CAE"].ToString();

            string strtimeVto = UnReporte.FechaVto;

            if (dtHeader.Rows[0]["RapiPago"] != null)
                UnReporte.Rapipago = dtHeader.Rows[0]["RapiPago"].ToString();

            if (dtHeader.Rows[0]["PagoFacil"] != null) 
                UnReporte.Pagofacil = dtHeader.Rows[0]["PagoFacil"].ToString();

            UnReporte.BarCodeCbte = WSPrint.Common.GetBarCode(oEmpresa.NroDocumento,
                                                    Utils.Utils.FillWithCeros(CbteCodigo, 2),
                                                    dtHeader.Rows[0]["PuntoVenta"].ToString(),
                                                    UnReporte.CAE,
                                                    strtimeVto.Split('/')[2] + ((strtimeVto.Split('/')[1].Length == 1) ? "0" + strtimeVto.Split('/')[1] : strtimeVto.Split('/')[1]) + ((strtimeVto.Split('/')[0].Length == 1) ? "0" + strtimeVto.Split('/')[0] : strtimeVto.Split('/')[0]));
            /**************************************************************************************/
            decimal ImporteGravado = 0;
            if (dtHeader.Rows[0]["ImporteGravado"].ToString() != string.Empty)
                ImporteGravado = Convert.ToDecimal(dtHeader.Rows[0]["ImporteGravado"].ToString());
            UnReporte.ImpuestoSubGrav = String.Format(FORMATO_NRO, ImporteGravado);

            decimal ImporteNoGravado = 0;
            if (dtHeader.Rows[0]["ImporteNoGravado"].ToString() != string.Empty)
                ImporteNoGravado = Convert.ToDecimal(dtHeader.Rows[0]["ImporteNoGravado"].ToString());
            UnReporte.ImporteSubNoGrav = String.Format(FORMATO_NRO, ImporteNoGravado);

            if (dtHeader.Rows[0]["LetraComprobante"].ToString() == "A")
            {
                decimal ImporteSubTotal = ImporteGravado + ImporteNoGravado;
                UnReporte.ImporteSubTotal = String.Format(FORMATO_NRO, ImporteSubTotal);
            }
            if (dtHeader.Rows[0]["LetraComprobante"].ToString() == "B")
            {
                decimal ImporteImpuestoLiquidado = 0;
                if (dtHeader.Rows[0]["ImporteImpuestoLiquidado"].ToString() != string.Empty)
                    ImporteImpuestoLiquidado = Convert.ToDecimal(dtHeader.Rows[0]["ImporteImpuestoLiquidado"].ToString());

                decimal ImporteSubTotal = ImporteGravado + ImporteNoGravado + ImporteImpuestoLiquidado;
                UnReporte.ImporteSubTotal = String.Format(FORMATO_NRO, ImporteSubTotal);
            }
            /**************************************************************************************/
            /*
            decimal CUOTAIVA21 = 0;
            if (dtHeader.Rows[0]["CUOTAIVA21"].ToString() != string.Empty)
                CUOTAIVA21 = Convert.ToDecimal(dtHeader.Rows[0]["CUOTAIVA21"].ToString());
            UnReporte.CUOTAIVA21 = String.Format(FORMATO_NRO, CUOTAIVA21);

            decimal CUOTAIVA105 = 0;
            if (dtHeader.Rows[0]["CUOTAIVA105"].ToString() != string.Empty)
                CUOTAIVA105 = Convert.ToDecimal(dtHeader.Rows[0]["CUOTAIVA105"].ToString());
            UnReporte.CUOTAIVA105 = String.Format(FORMATO_NRO, CUOTAIVA105);
            */
        }

        private string GetErrorCodigo(string CbteID)
        {
            //Si en la tabla de errores del Doc, encuentro el codigo 13, devuelvo 'Motivo: 13'. Sino, vacio.
            DataTable dtErrors = FacturaElectronica.WSPrint.Common.GetComprobanteErrorDataTable(CbteID);

            dtErrors.DefaultView.RowFilter = "ErrorCodigo LIKE '%13%'";

            if (dtErrors.DefaultView.Count > 0)
                return "Motivo: 13";
            else
                return "";
        }

        /// <summary>
        /// Bindeo el detalle del documento, a los campos correspondientes de 'LinesTable'.
        /// </summary>
        /// <param name="tipoDoc"></param>
        /// <param name="LinesTable"></param>
        /// <param name="reporte"></param>
        public void ReporteLlenarDetalle(TipoDocumento tipoDoc, DataTable LinesTable, ReporteBase reporte)
        {
            //Fill Details
            reporte.DataSource = LinesTable;
            reporte.xrTableCellDetalle.DataBindings.Add("Text", null, LinesTable.Columns["Descripcion"].Caption);
            
            if (UnReporte.LetraCbte != "B")
                reporte.xrTableCellTotal.DataBindings.Add("Text", null, LinesTable.Columns["ImporteSubtotalMonedaFacturacion"].Caption);
            else
                reporte.xrTableCellTotal.DataBindings.Add("Text", null, LinesTable.Columns["ImporteSubtotalMonedaFacturacionConIVA"].Caption);

            if (reporte.xrTableCellTotal.DataBindings[0] != null)
                reporte.xrTableCellTotal.DataBindings[0].FormatString = FORMATO_NRO;
            
        }

        public void AgregarRegistroImporte(XRTable tabla, string descripcion, string valor, bool limpiarTabla = false)
        {
            if (limpiarTabla)
            {
                tabla.Rows.Clear();
                tabla.Borders = DevExpress.XtraPrinting.BorderSide.None;
            }

            
            XRTableCell cell1 = new XRTableCell();
            cell1.TextAlignment = TextAlignment.BottomLeft;
            cell1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            //cell1.SizeF = new System.Drawing.SizeF(444.00F, 68.00F);
            //cell1.Size = new System.Drawing.Size(444, 68);
            cell1.StylePriority.UseTextAlignment = false;
            cell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            cell1.Text = string.Concat(cell1.Width.ToString(), descripcion);

            XRTableCell cell2 = new XRTableCell();
            cell2.TextAlignment = TextAlignment.BottomRight;
            cell2.Font = new System.Drawing.Font("Arial", 9F);
            //cell2.SizeF = new System.Drawing.SizeF(204.00F, 68.00F);
            //cell2.Size = new System.Drawing.Size(204, 68);
            cell2.StylePriority.UseTextAlignment = false;
            cell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);

            cell2.Text = valor;

            XRTableRow row = new XRTableRow();
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            
            tabla.Rows.Add(row);
        }

        /// <summary>
        /// Bindeo el detalle del documento, a los campos correspondientes de 'LinesTable'.
        /// </summary>
        /// <param name="tipoDoc"></param>
        /// <param name="LinesTable"></param>
        /// <param name="reporte"></param>
        public void ReporteLlenarImpuestos(TipoDocumento tipoDoc, DataTable Impuestos, ReporteBase reporte)
        {
            //Fill Details
           
            //reporte.DataSource = Impuestos;
            int i = 0;
            string descrip = "";
            double import = 0.0F;
            
             foreach (DataRow dr in Impuestos.Rows)
             {
                 i=i+1;
                 descrip = dr.Field<string>("Descripcion");
                 import = dr.Field<double>("Importe");

                 if (UnReporte.LetraCbte.Equals("A"))
                 {
                     if (i == 1)
                        AgregarRegistroImporte(reporte.xrTableImportes, descrip, String.Format(FORMATO_NRO, import));
                     
                     if (i == 2)
                         AgregarRegistroImporte(reporte.xrTableImportes, descrip, String.Format(FORMATO_NRO, import));
                     
                 }
                 if (i == 3)
                     AgregarRegistroImporte(reporte.xrTableImportes, descrip, String.Format(FORMATO_NRO, import));
                 

                 if (i == 4)
                     AgregarRegistroImporte(reporte.xrTableImportes, descrip, String.Format(FORMATO_NRO, import));
                 
             }
            
        }

        /// <summary>
        /// Cargo el valor de cada campo del Reporte (excepto los del detalle).
        /// </summary>
        /// <param name="tipoDoc"></param>
        /// <param name="reporte"></param>
        public void ReporteLlenarCampos(TipoDocumento tipoDoc, ReporteBase reporte)
        {
            reporte.txtTipo.Text = GetTipoDocDesc(tipoDoc);
            
            reporte.rtbDatosEmpresa.Rtf = reporte.rtbDatosEmpresa.Rtf.Replace("Data1", UnReporte.EmisorCalle);
            reporte.rtbDatosEmpresa.Rtf = reporte.rtbDatosEmpresa.Rtf.Replace("Data2", UnReporte.EmisorLocalidad + " - " + UnReporte.EmisorPais);
            reporte.rtbDatosEmpresa.Rtf = reporte.rtbDatosEmpresa.Rtf.Replace("Data3", UnReporte.EmisorTelefonos);
            reporte.rtbDatosEmpresa.Rtf = reporte.rtbDatosEmpresa.Rtf.Replace("Data4", UnReporte.EmisorEMail);


            reporte.txtCae.Text = UnReporte.CAE;
            reporte.txtCondiciondeVenta.Text = UnReporte.CondicionDeVenta;
            reporte.txtDomicilio.Text = UnReporte.Domicilio;
            reporte.txtFechaVto.Text = UnReporte.FechaVto;
            reporte.txtFechaCbte.Text = UnReporte.FechaCbte; 
            reporte.txtLetraCbte.Text = UnReporte.LetraCbte;
            reporte.txtNombreEmpresa.Text = UnReporte.NombreEmpresa;
            reporte.txtNroCbte.Text = UnReporte.NroCbte;
            reporte.txtNumCodigo.Text = UnReporte.NumCodigo;
            reporte.txtNumCuit.Text = UnReporte.NumCuit;
            reporte.txtNumIIBB.Text = UnReporte.NumIIBB;
            reporte.txtNumCodIIBB.Text = UnReporte.NumCodIIBB;
            reporte.txtRefInterna.Text = UnReporte.RefInterna;
            reporte.txtSon.Text = UnReporte.Son;
            reporte.txtTipoCbte.Text = UnReporte.TipoCbte;
            reporte.txtTipoIva.Text = UnReporte.TipoIva;
            reporte.txtMoneda.Text = UnReporte.Moneda;
            
            reporte.txtObservaciones3.Text = UnReporte.ObservacionesPie;
            reporte.txtBarCodeCbte.Text = UnReporte.BarCodeCbte;
            reporte.txtCondiciondeVenta.Text = UnReporte.CondicionDePago; //UnReporte.CondiciondeVenta;
            reporte.txtLugarDeEntrega.Text = UnReporte.ObservacionesCabecera; //Observacion Cabecera = Observaciones1

            //Actualizo segun TasaIIBB el nombre del campo IIBB del grupo de totales.
            reporte.lbl10.Text = UnReporte.TasaIIBB;
            //Campo SubTotal suma gravado + nogravado
            
            //reporte.txtSubtotal.Text = UnReporte.ImporteSubTotal.ToMoney();
            AgregarRegistroImporte(reporte.xrTableImportes, "Sub Total", UnReporte.ImporteSubTotal.ToMoney(),true);
            
            if (UnReporte.LetraCbte != "B")
            {
                reporte.txtPercepcionIIBBDocsB.Text = string.Empty;
                reporte.lblTituloPercepcionIIBBDocsB.Text = string.Empty;

                if (Convert.ToDecimal(UnReporte.ImpuestoSubGrav) > 0)
                {
                    /*
                    reporte.txtSUBGRAVTotal.Visible = true;
                    reporte.txtSUBGRAVTotal.Text = UnReporte.ImpuestoSubGrav.ToMoney();
                    reporte.lblSubGrav.Visible = true;
                    reporte.lblSubGrav.Text = "Importe Gravado";
                     **/
                    AgregarRegistroImporte(reporte.xrTableImportes, "Importe Gravado", UnReporte.ImpuestoSubGrav.ToMoney());
                }

                if (Convert.ToDecimal(UnReporte.ImporteSubNoGrav) > 0)
                {
                    /*
                    reporte.txtSUBNOGRAVTotal.Visible = true;
                    reporte.txtSUBNOGRAVTotal.Text = UnReporte.ImporteSubNoGrav.ToMoney();
                    reporte.lblSubNoGrav.Visible = true;
                    reporte.lblSubNoGrav.Text = "Importe No Gravado";
                     */
                    AgregarRegistroImporte(reporte.xrTableImportes, "Importe No Gravado", UnReporte.ImporteSubNoGrav.ToMoney());
                }
            }
            else 
            {
                //Documento 'B'
                //Oculto los campos que no corresponden a comprobantes B.
                reporte.xrLine1.Visible = true; //Primera linea que separa el detalle de los totales.
                reporte.txtImporteIIBBMonedaFacturacion.Visible = false;
                reporte.txtImporteIIBBMonedaFacturacion.Visible = false;
                reporte.lbl10.Visible = false;
               
                //Si un documento B tiene percepcion IIBB y 'TasaIIBB' tiene una descripcion, se muestra en un campo aparte.
                if (UnReporte.TasaIIBB != string.Empty && UnReporte.TotalPercepcionIIBB != string.Empty && Convert.ToDecimal(UnReporte.TotalPercepcionIIBB) > 0)
                {
                    reporte.txtPercepcionIIBBDocsB.Text = UnReporte.TotalPercepcionIIBB.ToMoney();
                    reporte.lblTituloPercepcionIIBBDocsB.Text = UnReporte.TasaIIBB;
                }
                else
                {
                    reporte.txtPercepcionIIBBDocsB.Text = string.Empty;
                    reporte.lblTituloPercepcionIIBBDocsB.Text = string.Empty;
                }
            }
            
            //Si no hay titulo para el campo, obviamente, oculto el valor.
            if (UnReporte.TasaIIBB.Trim() == string.Empty)
                reporte.txtImporteIIBBMonedaFacturacion.Visible = false;

            //reporte.txtSUBGRAVTotal.Text = UnReporte.TotalIva.ToMoney();
            reporte.txtImporteIIBBMonedaFacturacion.Text = UnReporte.TotalPercepcionIIBB.ToMoney();
            reporte.txtTotal.Text = UnReporte.Total.ToMoney();
            reporte.txtBarCodeRapipago.Text = UnReporte.Rapipago;
            reporte.txtBarcodeRapipago2.Text = UnReporte.Rapipago;

            reporte.txtDAGRUF.Text = UnReporte.DAGRUF;

            if (UnReporte.OPER != string.Empty | UnReporte.NOPER != string.Empty)
                reporte.txCPer.Text = UnReporte.OPER + " - " + UnReporte.NOPER;


            if (UnReporte.FACTORI_FORMATEADO != string.Empty)
            {
                reporte.txtRefInterna.Text = UnReporte.FACTORI_FORMATEADO;  //"Comprobante asociado DOC Nº: " + UnReporte.FACTORI;
                reporte.txtRefInterna.Visible = true;
            }
            else
            {
                reporte.txtRefInterna.Text = string.Empty;
                reporte.txtRefInterna.Visible = false;
            }

            reporte.txtUsuario.Text = UnReporte.USUARIO;
            //reporte.txtErrorCodigo.Text = UnReporte.ERRORCODIGO;
            reporte.txtInformacionAdicional.Text = UnReporte.ObservacionesCuerpo;

            //Lleno los campos que solo estan en un reporte o el otro.
            ReporteLlenarCamposEspecificos(tipoDoc, reporte);
        }

        private void ReporteLlenarCamposEspecificos(TipoDocumento tipoDoc, ReporteBase reporteBase)
        {
            Type tipoEspecifico = reporteBase.GetType();

            if (typeof(Sprayette) == tipoEspecifico)
            {
                Sprayette reporteEspecifico = (Sprayette)reporteBase;

                //reporteEspecifico.txtEmpresa.Visible = false;
                reporteEspecifico.picLogoCabecera.Visible = true;

                //Por ahora, sea A o B se muestra el mismo campo. Lo dejo preparado por si cambia.
                if (UnReporte.LetraCbte != "B")
                    reporteEspecifico.xrTableCellPrecioUnitario.DataBindings.Add("Text", null, "ImportePrecioUnitarioMonedaFacturacion");
                else
                    reporteEspecifico.xrTableCellPrecioUnitario.DataBindings.Add("Text", null, "ImportePrecioUnitarioMonedaFacturacion");

                if (reporteEspecifico.xrTableCellPrecioUnitario.DataBindings[0] != null)
                    reporteEspecifico.xrTableCellPrecioUnitario.DataBindings[0].FormatString = FORMATO_NRO;

                reporteEspecifico.xrTableCellCantidad.DataBindings.Add("Text", null, "CANTIDAD");

                //Datos de la empresa.
                reporteEspecifico.txtEmpresa.Text = UnReporte.oEmpresa.RazonSocial;
                reporteEspecifico.txtEmpresa.Visible = true;

                //Datos de la empresa, panel izquierdo.
                reporteEspecifico.rtbDatosEmpresa.Text = string.Format(reporteEspecifico.rtbDatosEmpresa.Text,
                                                                        UnReporte.oEmpresa.Direccion,
                                                                        UnReporte.oEmpresa.Localidad,
                                                                        UnReporte.oEmpresa.Pais,
                                                                        UnReporte.oEmpresa.Telefono);

                //Datos de la empresa, panel derecho.
                reporteEspecifico.rtbDatosEmpresa3.Text = string.Format(reporteEspecifico.rtbDatosEmpresa3.Text,
                                                                        UnReporte.oEmpresa.NroDocumento,
                                                                        UnReporte.oEmpresa.CodigoTipoResponsableAnteAFIP,
                                                                        UnReporte.oEmpresa.NroIIBB,
                                                                        UnReporte.oEmpresa.AgRecaudacionIIBB,
                                                                        UnReporte.oEmpresa.ImpuestosInternos,
                                                                        Convert.ToDateTime(UnReporte.oEmpresa.InicioActividades).ToString("dd/MM/yyyy"));

                SetReportLogo(reporteEspecifico);
            }
        }

        #endregion

        #region Metodos Auxiliares

        public void SaveDocumentInDB(ReporteBase reporte, TipoDocumento strType)
        {
            try
            {
                var oSettings = new Settings(UnReporte.oEmpresa.EmpresaID);
                string pathName = oSettings.PathImpresion + @"\TEMP_SITEDOWNLOADS\";
                string fileName = pathName + UnReporte.CbteID;

                try
                {
                    //Si no existe el path, intento crearlo.
                    if (!System.IO.Directory.Exists(pathName))
                        System.IO.Directory.CreateDirectory(pathName);
                }
                catch (Exception ex)
                {
                    throw new Exception("No se pudo crear el Path: " + pathName, ex);
                }


                //El componente genera el archivo pdf.
                reporte.ExportToPdf(fileName);

                //Abro el Pdf creado por el componente, lo convierto en bytes, y lo guardo en la DB.
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] archivoPdf = new byte[fs.Length];
                    fs.Read(archivoPdf, 0, archivoPdf.Length);

                    var sqlEngine = new DBEngine.SQLEngine();

                    if (sqlEngine.Open())
                    {
                        sqlEngine.SavePdfInDB(archivoPdf, UnReporte.CbteID, UnReporte.TipoDeCopia.ToString());

                        sqlEngine.Close();
                    }
                }

                //Si se creo el archivo, lo elimino.
                if (System.IO.File.Exists(fileName))
                    System.IO.File.Delete(fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetReportLogo(ReporteBase reporte)
        {
            const string nombreLogo = "Pictures/Logo{0}.jpg"; //{0} = IdEmpresa

            reporte.picLogoCabecera.ImageUrl = System.Web.HttpContext.Current.Request.MapPath(
                string.Format(nombreLogo,UnReporte.oEmpresa.EmpresaID.ToString())
                );
        }

        private string GetTipoDocDesc(TipoDocumento tipoDoc)
        {
            switch (tipoDoc)
            {
                case TipoDocumento.Factura:
                case TipoDocumento.FacturaExportacion:
                    return "Factura";

                case TipoDocumento.NotaCredito:
                case TipoDocumento.NotaCreditoExportacion:
                    return "Nota de Crédito";

                case TipoDocumento.NotaDebito:
                case TipoDocumento.NotaDebitoExportacion:
                    return "Nota de Débito";
                
                default:
                    return "-";
            }
        }

        #endregion
    }
}

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static string ToMoney(this string s)
        {
            ////Cambia ',' por '.' y '.' por ','.
            //s = s.Replace(",", "_");
            //s = s.Replace(".", ",");
            //s = s.Replace("_", ".");
            return s;
        }
    }
}