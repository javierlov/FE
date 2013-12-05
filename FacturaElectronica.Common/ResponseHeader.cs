using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacturaElectronica.Common
{
    public class ResponseHeader
    {
        public string SQLID = "";

        public string TipoTransaccion = "";
        public string Comprobante = "";
        public string CodigoDocumentoComprador = "";
        public string NroDocumentoComprador = "";
        public string TipoComprobante = "";
        public string PuntoVenta = "";
        public string LetraComprobante = "";
        public string NroComprobanteDesde = "";
        public string NroComprobanteHasta = "";
        public string Importe = "";
        public string ImporteNoGravado = "";
        public string ImporteGravado = "";
        public string ImporteImpuestoLiquidado = "";
        public string ImporteRNI_Percepcion = "";
        public string ImporteExento = "";
        public string Resultado = "";
        public string CAE = "";
        public string FechaComprobante = "";
        public string FechaVencimiento = "";
        public string Motivo = "";
        public string MotivoDescripcion = "";
        public string FechaDesdeServicioFacturado = "";
        public string FechaHastaServicioFacturado = "";
        public string FechaVencimientoPago = "";
        public string UltimoIDUsado = "";
        public string UltimoNroComprobanteUsado = "";
        public string NroInternoERP = "";

        public List<ResponseHeaderObs> Observaciones = new List<ResponseHeaderObs>();
    }
}
