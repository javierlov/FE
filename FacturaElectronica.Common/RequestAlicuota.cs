using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacturaElectronica.Common
{
    //TODO: Para Sprayette poner en todas las versiones
    public class RequestAlicuota
    {
        public string ImpuestoID;
        public string CbteID;
        public string Id;
        public string Tipo = "";
        public string BaseImp;
        public string Importe;
        public string ImporteMonedaFacturacion;
        public string Descripcion = "";
        public string Codigo;
    }
}
