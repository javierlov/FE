using System.Data;
using FacturaElectronica.Utils;

namespace FacturaElectronica.PrintEngine
{
    public class Documento
    {

        #region Propiedades

        public string CbteID { get; set; }
        public TipoDeCopia TipoDeCopia { get; set; }

        public string ImpuestoSubGrav { get; set; }
        public string ImporteSubNoGrav { get; set; }
        public string ImporteSubTotal { get; set; }
        public string Impuesto1 { get; set; }
        public string Impuesto2 { get; set; }
        public string Impuesto3 { get; set; }
        public string Impuesto4 { get; set; }

        public string TasaIIBB { get; set; }
        public string CUOTAIVA105 { get; set; }
        public string CUOTAIVA21 { get; set; }
        public string ERRORCODIGO { get; set; }
        public string OBSERVACIONRAPIPAGO { get; set; }
        public string FECPG1_FORMATEADO { get; set; }
        public string FECPG2_FORMATEADO { get; set; }
        public string DAGRUF { get; set; }
        public string OPER { get; set; }
        public string NOPER { get; set; }
        public string FACTORI_FORMATEADO { get; set; }
        public string USUARIO { get; set; }
        public string CODCLIENTECOMPRADOR { get; set; }
        public string AlicuotaIVA { get; set; }
        public string CAE { get; set; }
        public string CondicionDeVenta { get; set; }
        public string CondicionDePago { get; set; }
        public string DayCbte { get; set; }
        public string Domicilio { get; set; }
        public string FechaCbte { get; set; }
        public string FechaVto { get; set; }
        public string LetraCbte { get; set; }
        public string MonthCbte { get; set; }
        public string NombreEmpresa { get; set; }
        public string NroCbte { get; set; }
        public string NumCodigo { get; set; }
        public string NumCuit { get; set; }
        public string NumRemito { get; set; }
        public string NumIIBB { get; set; }
        public string NumCodIIBB { get; set; }
        public string RefCliente { get; set; }
        public string RefInterna { get; set; }
        public string Son { get; set; }
        public string SubTotal { get; set; }
        public string SubTotalComprobanteB { get; set; }
        public string SubTotalGravadoMonedaFacturacion { get; set; }
        public string TipoCbte { get; set; }
        public string TipoIva { get; set; }
        public string YearCbte { get; set; }
        public string Total { get; set; }
        public string TotalComprobanteB { get; set; }
        public string TotalGravadoMonedaFacturacion { get; set; }
        public string TotalPercepcionIIBB { get; set; }
        public string TotalIva { get; set; }
        public string Moneda { get; set; }
        public string ObservacionesCabecera { get; set; }
        public string ObservacionesPie { get; set; }
        public string ObservacionesCuerpo { get; set; }
        public string BarCodeCbte { get; set; }
        public string FormaPagoDescrip { get; set; }
        public string EmisorDireccion { get; set; }
        public string EmisorCalle { get; set; }
        public string EmisorCP { get; set; }
        public string EmisorLocalidad { get; set; }
        public string EmisorProvincia { get; set; }
        public string EmisorPais { get; set; }
        public string EmisorTelefonos { get; set; }
        public string EmisorEMail { get; set; }
        public string OficinaVentas { get; set; }
        public string Rapipago { get; set; }
        public string Pagofacil { get; set; }
        public Empresa oEmpresa { get; set; } 

        #endregion
        
        #region Metodos de Impresion

        private void ImprimirDocumento(TipoDocumento tipoDoc, string fileName, DataTable LinesTable, DataTable Impuestos)
        {
            var h = new DocumentoHelper(this);
            var d = DocumentoFactory.GetReportInstance(oEmpresa, tipoDoc);

            ////ToDo: Sacar!
            //foreach (DataRow item in LinesTable.Rows)
            //{
            //    item["ImporteSubtotalMonedaFacturacion"] = Convert.ToString(item["ImporteSubtotalMonedaFacturacion"]).ToMoney();
            //    item["ImportePrecioUnitarioMonedaFacturacion"] = Convert.ToString(item["ImportePrecioUnitarioMonedaFacturacion"]).ToMoney();
            //    item["ImporteSubtotalMonedaFacturacionConIVA"] = Convert.ToString(item["ImporteSubtotalMonedaFacturacionConIVA"]).ToMoney();
            //}

            h.ReporteLlenarCampos(tipoDoc, d);
            h.ReporteLlenarDetalle(tipoDoc, LinesTable, d);
            h.ReporteLlenarImpuestos(tipoDoc, Impuestos, d);

            h.SaveDocumentInDB(d, tipoDoc);

            if (fileName != null && fileName != string.Empty)
            {
                d.ExportToPdf(fileName);
            }
        }

        public void FillPdfProperties(string CbteCodigo, TipoDeCopia tipoDeCopia, Empresa oEmpresa, Settings oSettings, DataTable dtHeader)
        {
            var h = new DocumentoHelper(this);
            h.LlenarPropiedades(CbteCodigo, tipoDeCopia, oEmpresa, oSettings, dtHeader);
        }

        public void ImprimirArchivoFacturaLocal(string fileName, DataTable LinesTable, DataTable Impuestos)
        {
            ImprimirDocumento(TipoDocumento.Factura, fileName, LinesTable, Impuestos); 
        }

        public void ImprimirArchivoFacturaExportacion(string fileName, DataTable LinesTable, DataTable Impuestos)
        {
            ImprimirDocumento(TipoDocumento.FacturaExportacion, fileName, LinesTable, Impuestos); 

        }

        public void ImprimirArchivoNotaCreditoExportacion(string fileName, DataTable LinesTable, DataTable Impuestos)
        {
            ImprimirDocumento(TipoDocumento.NotaCreditoExportacion, fileName, LinesTable, Impuestos); 

        }

        public void ImprimirArchivoNotaCreditoLocal(string fileName, DataTable LinesTable, DataTable Impuestos)
        {
            ImprimirDocumento(TipoDocumento.NotaCredito, fileName, LinesTable, Impuestos); 

        }

        public void ImprimirArchivoNotaDebitoExportacion(string fileName, DataTable LinesTable, DataTable Impuestos)
        {
            ImprimirDocumento(TipoDocumento.NotaDebitoExportacion, fileName, LinesTable, Impuestos); 

        }

        public void ImprimirArchivoNotaDebitoLocal(string fileName, DataTable LinesTable, DataTable Impuestos)
        {
            ImprimirDocumento(TipoDocumento.NotaDebito, fileName, LinesTable, Impuestos); 

        }
        
        #endregion

    }
}
