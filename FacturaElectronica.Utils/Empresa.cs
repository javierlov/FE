using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FacturaElectronica.Utils
{
    public class Empresa
    {
        private Hashtable parametros = new Hashtable();

        public Empresa(string EmpresaID)
        {
           DBEngine.SQLEngine sqlEngine = new DBEngine.SQLEngine();
           parametros = sqlEngine.GetEmpresa(EmpresaID);
        }

        public string EmpresaID { get { return parametros["EmpresaID"].ToString(); } }
        public string TipoDocumento { get { return parametros["TipoDocumento"].ToString(); } }
        public string NroDocumento { get { return parametros["NroDocumento"].ToString(); } }
        public string RazonSocial { get { return parametros["RazonSocial"].ToString(); } }
        public string InicioActividades { get { return parametros["InicioActividades"].ToString(); } }
        public string Direccion { get { return parametros["Direccion"].ToString(); } }
        public string Localidad { get { return parametros["Localidad"].ToString(); } }
        public string Provincia { get { return parametros["Provincia"].ToString(); } }
        public string Pais { get { return parametros["Pais"].ToString(); } }
        public string CodigoPostal { get { return parametros["CodigoPostal"].ToString(); } }
        public string Telefono { get { return parametros["Telefono"].ToString(); } }
        public string Fax { get { return parametros["Fax"].ToString(); } }
        public string Email { get { return parametros["Email"].ToString(); } }
        public string Contacto { get { return parametros["Contacto"].ToString(); } }

        public string CodigoTipoResponsableAnteAFIP { get { return parametros["CodigoTipoResponsableAnteAFIP"].ToString(); } }
        public string NroIIBB { get { return parametros["NroIIBB"].ToString(); } }
        public string AgRecaudacionIIBB { get { return parametros["AgRecaudacionIIBB"].ToString(); } }
        public string ImpuestosInternos { get { return parametros["ImpuestosInternos"].ToString(); } }
    }
}
