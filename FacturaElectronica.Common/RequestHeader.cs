using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacturaElectronica.Common
{
    public class RequestHeader
    {
        public string SQLID = "";

        public string EmpresaID = "";
        public string TipoTransaccion = "";
        public string FechaComprobante = "";
        public string FechaDesdeServicioFacturado = "";
        public string FechaHastaServicioFacturado = "";
        public string TipoComprobante = "";
        public string NroComprobanteDesde = "";
        public string NroComprobanteHasta = "";
        public string FechaVencimientoPago = "";

        public string Importe = "";
        public string ImporteComprobanteB = "";
        public string ImporteNoGravado = "";
        public string ImporteGravado = "";
        public string ImporteImpuestoLiquidado = "";
        public string ImporteRNI_Percepcion = "";
        public string ImporteExento = "";
        public string ImportePercepciones_PagosCuentaImpuestosNacionales = "";
        public string ImportePercepcionIIBB = "";
        public string ImportePercepcionImpuestosMunicipales = "";
        public string ImporteImpuestosInternos = "";
        public string CantidadAlicuotasIVA = "";
        public string CodigoOperacion = "";
        public string LetraComprobante = "";
        public string NroInternoERP = "";        
        public string CondicionPago = "";

        public string OficinaVentas = "";
        public string PuntoVenta = "";

        public string EmisorRazonSocial = "";
        public string EmisorDireccion = "";
        public string EmisorCalle = "";
        public string EmisorCP = "";
        public string EmisorLocalidad = "";
        public string EmisorProvincia = "";
        public string EmisorPais = "";
        public string EmisorTelefonos = "";
        public string EmisorEMail = "";

        public string CompradorCodigoDocumento = "";
        public string CompradorNroDocumento = "";
        public string CompradorRazonSocial = "";
        public string CompradorTipoResponsable = "";
        public string CompradorTipoResponsableDescripcion = "";
        public string CompradorDireccion = "";
        public string CompradorLocalidad = "";
        public string CompradorProvincia = "";
        public string CompradorPais = "";
        public string CompradorCodigoPostal = "";
        public string CompradorNroIIBB = "";
        public string CompradorCodigoCliente = "";
        public string CompradorNroReferencia = "";
        public string CompradorEmail = "";

        public string NroRemito = "";
        public string AlicuotaIVA = "";
        public string TasaIIBB = "";
        public string CodigoJurisdiccionIIBB = "";
        public string JurisdiccionImpuestosMunicipales = "";

        public string ImporteMonedaFacturacion = "";
        public string ImporteMonedaFacturacionComprobanteB = "";
        public string ImporteNoGravadoMonedaFacturacion = "";
        public string ImporteGravadoMonedaFacturacion = "";
        public string ImporteImpuestoLiquidadoMonedaFacturacion = "";
        public string ImporteRNI_PercepcionMonedaFacturacion = "";
        public string ImporteExentoMonedaFacturacion = "";
        public string ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = "";
        public string ImportePercepcionIIBBMonedaFacturacion = "";
        public string ImportePercepcionImpuestosMunicipalesMonedaFacturacion = "";
        public string ImporteImpuestosInternosMonedaFacturacion = "";
        public string ImporteEscrito = "";

        public string TasaCambio = "";
        public string CodigoMoneda = "";        
        public string CantidadRegistrosDetalle = "";
        public string CodigoMecanismoDistribucion = "";
        public string TipoExportacion = "";
        public string PermisoExistente = "";
        public string FormaPagoDescripcion = "";
        public string IncoTerms = "";
        public string Idioma = "";

        public string Observaciones1 = "";
        public string Observaciones2 = "";
        public string Observaciones3 = "";

        public string RapiPago = "";
        public string ObservacionRapiPago = "";
        public string PagoFacil = "";

        public string OPER = "";
        public string NOPER = "";
        public string DAGRUF = "";
        public string FACTORI = "";
        public string FACTORI_FORMATEADO = "";
        public string USUARIO = "";
        public string FECPG1_FORMATEADO = "";
        public string FECPG2_FORMATEADO = "";
        public string CUOTAIVA105 = "";
        public string CUOTAIVA21 = "";

        public List<RequestLine> RequestLines = new List<RequestLine>();

        //TODO: Para Sprayette poner en todas las versiones
        public List<RequestAlicuota> RequestAlicuotas = new List<RequestAlicuota>();
        public List<RequestTributo> RequestTributos = new List<RequestTributo>();
    }
}
