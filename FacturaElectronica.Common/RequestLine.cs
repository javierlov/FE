using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacturaElectronica.Common
{
    public class RequestLine
    {
        public string SQLID = "";

        public string CodigoProductoEmpresa = "";
        public string CodigoProductoNCM = "";
        public string CodigoProductoSecretaria = "";
        public string Descripcion = "";
        public string Cantidad = "";
        public string UnidadMedida = "";
        public string ImportePrecioUnitario = "";
        public string ImporteBonificacion = "";
        public string ImporteAjuste = "";
        public string ImporteSubtotal = "";
        public string ImportePrecioUnitarioMonedaFacturacion = "";
        public string ImporteBonificacionMonedaFacturacion = "";
        public string ImporteAjusteMonedaFacturacion = "";
        public string ImporteSubtotalMonedaFacturacion = "";
        public string ImporteSubtotalMonedaFacturacionConIVA = "";
        public string AlicuotaIVA = "";
        public string IndicadorExentoGravadoNoGravado = "";
        public string Observaciones = "";
        public string MesPrestacion = ""; 
    }
}
